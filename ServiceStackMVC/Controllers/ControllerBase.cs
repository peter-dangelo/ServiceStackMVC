using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStackMVC.App_Start;
using ServiceStackMVC.Models;
using ServiceStack.CacheAccess;
using ServiceStack.Mvc;
using ServiceStack.Mvc.MiniProfiler;
using ServiceStack.WebHost.Endpoints;

namespace ServiceStackMVC.Controllers
{
    public class ControllerBase : ServiceStackController<CustomUserSession>
    {
    }

    public class TestController : Controller
    {
        private ISession _serviceStackSession;
        protected ISession ServiceStackSession
        {
            get
            {
                return _serviceStackSession ?? (_serviceStackSession = AppHostBase.Resolve<ISessionFactory>().GetOrCreateSession());
            }
        }
    }
}