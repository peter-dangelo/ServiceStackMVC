using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.DataAnnotations;

namespace ServiceStackMVC.Models
{
    public class OrganizationAlly
    {
        [AutoIncrement]
        public int Id { get; set; }
        [References(typeof(Organization))]
        public int OrganizationId { get; set; }
        [References(typeof(User))]
        public int UserId { get; set; }
    }
}
