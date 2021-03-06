﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.DataAnnotations;
using ServiceStack.ServiceHost;

namespace ServiceStackMVC.Models
{
    public class StateProvince
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }

    [Route("/state-provinces", "GET")]
    public class StateProvinces : IReturn<StateProvincesResponse>
    {

    }

    public class StateProvincesResponse
    {
        public List<StateProvince> StateProvinces { get; set; }
    }
}