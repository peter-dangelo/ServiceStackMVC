using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStackMVC.Models;
using ServiceStack.OrmLite;

namespace ServiceStackMVC.Services
{
    /// <summary>
    /// GET /state-provinces
    /// Returns a list of state provinces
    /// </summary>
    public class StateProvincesService : ServiceStack.ServiceInterface.Service
    {
        public object Get(StateProvinces request)
        {
            return new StateProvincesResponse
            {
                StateProvinces = Db.Select<StateProvince>()
            };
        }
    }
}
