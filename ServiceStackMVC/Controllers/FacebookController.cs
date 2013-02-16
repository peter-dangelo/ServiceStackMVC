using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook;
using ServiceStack.ServiceInterface.Auth;
using ServiceStackMVC.Helpers;
using ServiceStackMVC.Models.DTOs;

namespace ServiceStackMVC.Controllers
{
    public class FacebookController : Controller
    {
        [HttpPost]
        public ActionResult Login(string accessToken, string uid)
        {
            if (accessToken == null || uid == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var facebookClient = new FacebookClient(accessToken);
            dynamic me = facebookClient.Get("me");
            string email = me.email;

            if (me != null)
            {
                var serviceStackClient = GlobalHelper.ClientFromAspNetSession;

                var user = serviceStackClient.Get(new UserDto { Email = me.email }).User;

                if (user != null)
                {
                    // Doesn't work
                    var response = serviceStackClient.Send<AuthResponse>(new Auth
                    {
                        provider = FacebookAuthProvider.Name,
                        oauth_token = accessToken,
                        RememberMe = true
                    });

                    GlobalHelper.ClientFromAspNetSession = serviceStackClient;

                    return RedirectToAction("Index", "OrganizationCategories");
                }
            }

            return RedirectToAction("Register", "Account");
        }

    }
}
