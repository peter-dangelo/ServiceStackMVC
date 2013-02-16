using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack;
using ServiceStackMVC.Helpers;
using ServiceStackMVC.Models;
using ServiceStackMVC.Services;
using ServiceStack.ServiceClient.Web;
using ServiceStack.CacheAccess;
using ServiceStack.ServiceInterface;
using ServiceStack.WebHost.Endpoints;
using ServiceStack.ServiceInterface.Auth;

namespace ServiceStackMVC.Controllers
{
    public class OrganizationCategoriesController : ControllerBase
    {
        public ActionResult Index()
        {
            //var client = GlobalHelper.GetClient(Session);

            //// Can see u user session in here
            //var cacheClient = Cache;
            //try
            //{
            //    // Cannot get user session here
            //    if (!base.UserSession.IsAuthenticated)
            //    {
            //        var myCustomFoo = base.UserSession.CustomFoo;
            //        var isAuth = base.UserSession.IsAuthenticated;
            //        return RedirectToAction("Login", "Account");
            //    }

            //    var response = client.Get(new OrganizationCategories());
            //    return View(response);
                //var sessionKey = SessionFeature.GetSessionKey();
                //var userSession = SessionFeature.GetOrCreateSession<CustomUserSession>(CacheClient);
                //var organizationCategoryService = AppHostBase.Resolve<OrganizationCategoriesService>();
                ////var response = client.Get(new OrganizationCategories());
                //var response = organizationCategoryService.Get(new OrganizationCategories());
                //return View(response);

            //}
            //catch (WebServiceException e)
            //{
            //    throw;
            //}

            try
            {
                var orgCategoriesService = AppHostBase.Resolve<OrganizationCategoriesService>();
                orgCategoriesService.RequestContext = System.Web.HttpContext.Current.ToRequestContext();
                var response = orgCategoriesService.Get(new OrganizationCategories());
                return View(response);

            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public ActionResult IndexFromFacebook()
        {
            // Here use the Aspnet client
            var serviceStackClient = GlobalHelper.ClientFromAspNetSession;
            var response = serviceStackClient.Get(new OrganizationCategories());
            return View("Index", response);
        }

        public ActionResult Add()
        {
            var orgCategory = new OrganizationCategory();
            return View(orgCategory);
        }

        [HttpPost]
        public ActionResult Add(OrganizationCategory orgCategory)
        {
            throw new NotImplementedException();
            //var client = GlobalHelper.GetClient(base.Session);

            //try
            //{
            //    client.Post(orgCategory);
            //    return RedirectToAction("Index", "OrganizationCategories");
            //}
            //catch (WebServiceException exception)
            //{
            //    throw;
            //}
        }

        public ActionResult Edit(int id)
        {
            throw new NotImplementedException();
            //var client = GlobalHelper.GetClient(base.Session);

            //var orgCategory = client.Get(new OrganizationCategory { Id = id });
            //return View(orgCategory.OrganizationCategory);
        }

        [HttpPost]
        public ActionResult Edit(OrganizationCategory orgCategory)
        {
            throw new NotImplementedException();
            //var client = GlobalHelper.GetClient(base.Session);

            //try
            //{
            //    client.Put(orgCategory);
            //    return RedirectToAction("Index", "OrganizationCategories");
            //}
            //catch (WebServiceException)
            //{
            //    throw;
            //}
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            throw new NotImplementedException();
            //var client = GlobalHelper.GetClient(base.Session);

            //try
            //{
            //    client.Delete(new OrganizationCategory { Id = id });
            //    return RedirectToAction("Index", "OrganizationCategories");
            //}
            //catch (WebServiceException)
            //{
            //    throw;
            //}
        }
    }
}