using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceStackMVC.Models.ViewModels
{
    public class OrganizationAlliesModel
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public List<UserDetailModel> Users { get; set; }
    }
}