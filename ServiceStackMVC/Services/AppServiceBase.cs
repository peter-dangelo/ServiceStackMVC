using ServiceStackMVC.Models;
using ServiceStack.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceStackMVC.Services
{
    public abstract class AppServiceBase : Service
    {
        public CustomUserSession UserSession
        {
            get { return SessionAs<CustomUserSession>(); }
        }
    }
}