using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack;
using ServiceStackMVC.Helpers;
using ServiceStackMVC.Models.ViewModels;
using ServiceStack.CacheAccess;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface.Auth;
using ServiceStackMVC.Models.ViewModels;
using ServiceStack.WebHost.Endpoints;

namespace ServiceStackMVC.Controllers
{
    public class AccountController : ControllerBase
    {
        public ActionResult Login()
        {
            ViewBag.ReturnUrl = "/OrganizationCategories";
            var model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            // This method resulted in incompatible cookies
            //var client = GlobalHelper.GetClient(base.Session);

            //var response = client.Send<AuthResponse>(new Auth
            //{
            //    provider = CredentialsAuthProvider.Name,
            //    UserName = model.Username,
            //    Password = model.Password,
            //    RememberMe = true
            //});

            var authService = AppHostBase.Resolve<AuthService>();
            authService.RequestContext = System.Web.HttpContext.Current.ToRequestContext();
            var response = authService.Authenticate(new Auth
                                                        {
                                                            UserName = model.Username,
                                                            Password = model.Password,
                                                            RememberMe = model.RememberMe
                                                        });

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
