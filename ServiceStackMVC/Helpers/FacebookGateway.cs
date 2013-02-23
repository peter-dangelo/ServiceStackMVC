using ServiceStack.ServiceInterface.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceStackMVC.Helpers
{
    public interface IFacebookGateway
    {
        IFacebookGateway CreateAuthorizedGateway(OAuthProvider authProvider, string accessToken,
                                                 string accessTokenSecret);
    }

    public class FacebookAuth
    {
        public OAuthProvider OAuthProvider { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
    }

    public class FacebookGateway : IFacebookGateway
    {
        public FacebookAuth Auth { get; set; }

        public IFacebookGateway CreateAuthorizedGateway(OAuthProvider authProvider, string accessToken, string accessTokenSecret)
        {
            return new FacebookGateway
                       {
                           Auth = new FacebookAuth
                                      {
                                          OAuthProvider = authProvider,
                                          AccessToken = accessToken,
                                          AccessTokenSecret = accessTokenSecret
                                      }
                       };
        }
    }
}