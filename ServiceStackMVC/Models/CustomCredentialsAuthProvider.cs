using ServiceStack.Configuration;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceStackMVC.Models
{
    public class CustomCredentialsAuthProvider : CredentialsAuthProvider
    {
        public CustomCredentialsAuthProvider(AppSettings appSettings) : base(appSettings)
        {
        }

        public override bool TryAuthenticate(ServiceStack.ServiceInterface.IServiceBase authService, string userName, string password)
        {
            var authenticated = base.TryAuthenticate(authService, userName, password);
            //var session = authService.GetSession(false);
            //authService.SaveSession(session);
            //if (authenticated)
            //{
            //    var session = (CustomUserSession) authService.GetSession(false);
            //    session.DisplayName = userName;
            //    session.IsAuthenticated = true;
            //}

            return authenticated;
        }
    }
}