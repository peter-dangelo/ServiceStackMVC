using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceStackMVC.Models.ViewModels
{
    public class HeaderModel
    {
        public bool IsLoggedIn { get; set; }
        public string DisplayName { get; set; }
    }
}