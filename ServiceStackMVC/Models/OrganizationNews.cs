using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.DataAnnotations;

namespace ServiceStackMVC.Models
{
    public class OrganizationNews
    {
        [AutoIncrement]
        public int Id { get; set; }
        [References(typeof(Organization))]
        public int OrganizationId { get; set; }
        public string NewsText { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}