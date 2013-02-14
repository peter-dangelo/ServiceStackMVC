using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStackMVC.Helpers;
using ServiceStackMVC.Models.ViewModels;
using ServiceStack.CacheAccess;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface.Auth;
using ServiceStackMVC.Models.ViewModels;

namespace ServiceStackMVC.Controllers
{
    public class AccountController : ControllerBase
    {
        public ICacheClient Cache { get; set; }
        public ActionResult Login()
        {
            ViewBag.ReturnUrl = "/OrganizationCategories";
            var model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            //var client = GlobalHelper.GetClient(base.Session);
            var client = new JsonServiceClient(GlobalHelper.GetServiceUrl());

            var response = client.Send<AuthResponse>(new Auth
            {
                provider = CredentialsAuthProvider.Name,
                UserName = model.Username,
                Password = model.Password,
                RememberMe = true
            });

            var cache = Cache;
            //base.UserSession = cache.
            return RedirectToAction("Index", "OrganizationCategories");
        }

        public ActionResult Register()
        {
            var model = new RegisterModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var client = GlobalHelper.GetClient(base.Session);

                // Attempt to register the user
                try
                {
                    //WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                    //WebSecurity.Login(model.UserName, model.Password);
                    client.Post<RegistrationResponse>(new Registration
                                                          {
                                                              AutoLogin = true,
                                                              DisplayName = model.DisplayName,
                                                              Email = model.Email,
                                                              FirstName = model.FirstName,
                                                              LastName = model.LastName,
                                                              Password = model.Password,
                                                              UserName = model.DisplayName
                                                          });

                    return RedirectToAction("Index", "OrganizationCategories");
                }
                    //catch (MembershipCreateUserException e)
                    //{
                    //    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                    //}
                catch (Exception ex)
                {
                    throw;
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult Logout()
        {
            throw new NotImplementedException();

            return RedirectToAction("Index", "Home");
        }

    }
}
