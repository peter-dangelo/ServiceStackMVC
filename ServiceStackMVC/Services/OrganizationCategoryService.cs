using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStackMVC.Models;
using ServiceStack.OrmLite;
using ServiceStack.ServiceInterface;

namespace ServiceStackMVC.Services
{
    public class OrganizationCategoryService : AppServiceBase//ServiceStack.ServiceInterface.Service
    {
        /// <summary>
        /// GET /organizationcategories/{Id} 
        /// </summary>
        //[Authenticate]
        public object Get(OrganizationCategory organizationCategory)
        {
            return new OrganizationCategoryResponse
            {
                OrganizationCategory = Db.Id<OrganizationCategory>(organizationCategory.Id)
            };
        }

        public object Post(OrganizationCategory organizationCategory)
        {
            Db.Insert(organizationCategory);
            //return new HttpResult(Db.GetLastInsertId(), HttpStatusCode.Created);
            return new OrganizationCategoryResponse { OrganizationCategory = new OrganizationCategory() };
        }

        public object Put(OrganizationCategory organizationCategory)
        {
            Db.Update(organizationCategory);
            //return new HttpResult {StatusCode = HttpStatusCode.NoContent};
            return new OrganizationCategoryResponse { OrganizationCategory = new OrganizationCategory() };
        }

        public object Delete(OrganizationCategory organizationCategory)
        {
            Db.DeleteById<OrganizationCategory>(organizationCategory.Id);
            //return new HttpResult { StatusCode = HttpStatusCode.NoContent };
            return new OrganizationCategoryResponse { OrganizationCategory = new OrganizationCategory() };
        }
    }

    /// <summary>
    /// GET /organizationcategories
    /// Returns a list of organization categories
    /// </summary>
    //[Authenticate]
    public class OrganizationCategoriesService : AppServiceBase//ServiceStack.ServiceInterface.Service
    {
        public object Get(OrganizationCategories request)
        {
            return new OrganizationCategoriesResponse
            {
                OrganizationCategories = Db.Select<OrganizationCategory>()
            };
        }
    }
}