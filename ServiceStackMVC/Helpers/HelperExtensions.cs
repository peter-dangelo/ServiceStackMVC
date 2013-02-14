using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStackMVC.App_Start;

namespace ServiceStackMVC.Helpers
{
    public static class HelperExtensions
    {
        public static bool In(this Env env, params Env[] inAnyEnvs)
        {
            return inAnyEnvs.Any(x => x == env);
        }
    }
}