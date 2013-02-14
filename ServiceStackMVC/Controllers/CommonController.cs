using ServiceStackMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceStackMVC.Controllers
{
    public class CommonController : ControllerBase
    {
        [ChildActionOnly]
        public ActionResult Header()
        {
            var model = new HeaderModel();
            bool isAuthenticated = base.UserSession.IsAuthenticated;
            model.IsLoggedIn = isAuthenticated;
            model.DisplayName = isAuthenticated ? base.UserSession.DisplayName : "";
            return PartialView(model);
        }

    }
}
