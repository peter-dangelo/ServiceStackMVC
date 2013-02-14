using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ServiceStackMVC.Mappers;

namespace ServiceStackMVC.App_Start
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(x => x.AddProfile<MappingProfile>());
        }
    }
}