using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using ServiceStack.CacheAccess;
using ServiceStack.ServiceClient.Web;

namespace ServiceStackMVC.Helpers
{
    public static class GlobalHelper
    {
        public static string GetServiceUrl()
        {
            return ConfigurationManager.AppSettings["baseServiceUrl"];
        }

        /// <summary>
        /// Get a client from a servic stack session otherwise new up
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static JsonServiceClient GetClient(ISession session)
        {
            try
            {
                var client = session.Get<JsonServiceClient>("Client");
                if (client != null)
                {
                    return client;
                }

                client = new JsonServiceClient(GlobalHelper.GetServiceUrl());
                session.Set("Client", client);
                return client;
            }
            catch (Exception)
            {
                return new JsonServiceClient(GlobalHelper.GetServiceUrl());
            }        
        }

        public static JsonServiceClient ClientFromAspNetSession
        {
            get
            {
                var client = (JsonServiceClient)HttpContext.Current.Session["SsClient"];
                if (client != null)
                {
                    return client;
                }

                client = new JsonServiceClient(GlobalHelper.GetServiceUrl());
                HttpContext.Current.Session["SsClient"] = client;
                return client;     
            }

            set { HttpContext.Current.Session["SsClient"] = value; }

        }

        private static JsonServiceClient GetSessionFromCookie()
        {
            throw new NotImplementedException();
        }
    }
}