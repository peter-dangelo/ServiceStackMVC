using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Common.Utils;
using ServiceStack.Configuration;
using ServiceStack.FluentValidation;
using ServiceStack.Html;
using ServiceStack.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.WebHost.Endpoints;
using ServiceStackMVC.Controllers;
using ServiceStackMVC.Helpers;
using ServiceStackMVC.Models;
using ServiceStackMVC.Services;
using RouteValueDictionary = System.Web.Routing.RouteValueDictionary;
using TagBuilder = System.Web.Mvc.TagBuilder;
using TagRenderMode = System.Web.Mvc.TagRenderMode;
[assembly: WebActivator.PreApplicationStartMethod(typeof(ServiceStackMVC.App_Start.AppHost), "Start")]

//IMPORTANT: Add the line below to MvcApplication.RegisterRoutes(RouteCollection) in the Global.asax:
//routes.IgnoreRoute("api/{*pathInfo}"); 

/**
 * Entire ServiceStack Starter Template configured with a 'Hello' Web Service and a 'Todo' Rest Service.
 *
 * Auto-Generated Metadata API page at: /metadata
 * See other complete web service examples at: https://github.com/ServiceStack/ServiceStack.Examples
 */

namespace ServiceStackMVC.App_Start
{
    //Hold App wide configuration you want to accessible by your services
    public class AppConfig
    {
        public AppConfig(IResourceManager appSettings)
        {
            this.Env = appSettings.Get("Env", Env.Local);
            this.EnableCdn = appSettings.Get("EnableCdn", false);
            this.CdnPrefix = appSettings.Get("CdnPrefix", "");
            this.AdminUserNames = appSettings.Get("AdminUserNames", new List<string>());
        }

        public Env Env { get; set; }
        public bool EnableCdn { get; set; }
        public string CdnPrefix { get; set; }
        public List<string> AdminUserNames { get; set; }
        public BundleOptions BundleOptions
        {
            get { return Env.In(Env.Local, Env.Dev) ? BundleOptions.Normal : BundleOptions.MinifiedAndCombined; }
        }
    }

    public enum Env
    {
        Local,
        Dev,
        Test,
        Prod,
    }

    public class AppHost
        : AppHostBase
    {
        public AppHost() //Tell ServiceStack the name and where to find your web services
            : base("StarterTemplate ASP.NET Host", typeof(OrganizationService).Assembly) { }

        public static AppConfig AppConfig;

        public override void Configure(Funq.Container container)
        {
            //Set JSON web services to return idiomatic JSON camelCase properties
            ServiceStack.Text.JsConfig.EmitCamelCaseNames = true;

            //Configure User Defined REST Paths
            //Routes
            //  .Add<Hello>("/hello")
            //  .Add<Hello>("/hello/{Name*}");

            //Uncomment to change the default ServiceStack configuration
            //SetConfig(new EndpointHostConfig {
            //});

            //Register Typed Config some services might need to access
            var appSettings = new AppSettings();
            AppConfig = new AppConfig(appSettings);
            container.Register(AppConfig);

            //Register all your dependencies
            //container.Register(new TodoRepository());
            container.Register<ICacheClient>(new MemoryCacheClient());

            // Register DB
            container.Register<IDbConnectionFactory>(c =>
                                                     new OrmLiteConnectionFactory(
                                                         "~/App_Data/db.sqlite".MapHostAbsolutePath(),
                                                         SqliteOrmLiteDialectProvider.Instance));

            // Reset DB
            using (var resetDb = container.Resolve<ResetDbService>())
            {
                resetDb.Any(null);
            }

            //Enable Authentication
            ConfigureAuth(container);

            //Set MVC to use the same Funq IOC as ServiceStack
            ControllerBuilder.Current.SetControllerFactory(new FunqControllerFactory(container));
            ServiceStackController.CatchAllController = reqCtx => container.TryResolve<HomeController>();
        }

        // Uncomment to enable ServiceStack Authentication and CustomUserSession
        private void ConfigureAuth(Funq.Container container)
        {
            var appSettings = new AppSettings();

            //Default route: /auth/{provider}
            Plugins.Add(new AuthFeature(() => new CustomUserSession(), //Use your own typed Custom UserSession type
                new IAuthProvider[] {
					//new CredentialsAuthProvider(appSettings), 
                    new CustomCredentialsAuthProvider(appSettings),
					new FacebookAuthProvider(appSettings), 
					//new TwitterAuthProvider(appSettings), 
					new BasicAuthProvider(appSettings), 
				}));

            //Default route: /register
            Plugins.Add(new RegistrationFeature());

            //override the default registration validation with your own custom implementation
            container.RegisterAs<CustomRegistrationValidator, IValidator<Registration>>();

            ////Requires ConnectionString configured in Web.Config
            //var connectionString = ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString;
            //container.Register<IDbConnectionFactory>(c =>
            //    new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider));

            container.Register<IUserAuthRepository>(c =>
                new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>()));

            var authRepo = (OrmLiteAuthRepository)container.Resolve<IUserAuthRepository>();
            authRepo.ValidUserNameRegEx = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            authRepo.CreateMissingTables();
        }

        public static void Start()
        {
            new AppHost().Init();
        }
    }
}