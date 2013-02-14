using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStackMVC.Helpers;
using ServiceStackMVC.Mappers;
using ServiceStackMVC.Models;
using ServiceStackMVC.Models.DTOs;
using ServiceStack.Common;
using ServiceStack.ServiceClient.Web;
using ServiceStack.WebHost.Endpoints;
using ServiceStackMVC.Services;
using ServiceStackMVC.Models.ViewModels;

namespace ServiceStackMVC.Controllers
{
    public class OrganizationsController : ControllerBase
    {
        public ActionResult Index()
        {
            var client = GlobalHelper.GetClient(base.Session);

            var userSession = base.UserSession;
            var orgs = client.Get(new OrganizationsDto()).Organizations;
            var model = orgs.Select(o => o.ToModel()).ToList();
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var client = GlobalHelper.GetClient(base.Session);

            var org = client.Get(new OrganizationDto { Id = id }).Organization;
            var model = org.ToDetailModel();
            return View(model);
        }

        public ActionResult Add()
        {
            var client = GlobalHelper.GetClient(base.Session);

            var model = new OrganizationDetailModel();
            var allStateProvinces = client.Get(new StateProvinces()).StateProvinces;
            model.AllStateProvinces = new SelectList(allStateProvinces, "Id", "Name");
            var allCategories = client.Get(new OrganizationCategories()).OrganizationCategories;
            model.AllOrganizationCategories = new SelectList(allCategories, "Id", "Name");
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(OrganizationDetailModel model)
        {
            var client = GlobalHelper.GetClient(base.Session);

            try
            {
                var org = model.ToEntity();
                client.Post(org);
                return RedirectToAction("Index", "Organizations");
            }
            catch (WebServiceException exception)
            {
                throw;
            }
        }

        public ActionResult Edit(int id)
        {
            var client = GlobalHelper.GetClient(base.Session);

            var org = client.Get(new OrganizationDto { Id = id }).Organization;
            var model = org.ToDetailModel();
            var allStateProvinces = client.Get(new StateProvinces()).StateProvinces;
            model.AllStateProvinces = new SelectList(allStateProvinces, "Id", "Name", model.StateProvinceId);
            var allCategories = client.Get(new OrganizationCategories()).OrganizationCategories;
            model.AllOrganizationCategories = new SelectList(allCategories, "Id", "Name", model.OrganizationCategoryId);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(OrganizationDetailModel model)
        {
            var client = GlobalHelper.GetClient(base.Session);

            try
            {
                var org = model.ToEntity();
                client.Put(org);
                return RedirectToAction("Index", "Organizations");
            }
            catch (WebServiceException)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var client = GlobalHelper.GetClient(base.Session);

            try
            {
                client.Delete(new OrganizationDto { Id = id });
                return RedirectToAction("Index", "Organizations");
            }
            catch (WebServiceException)
            {
                throw;
            }
        }

        public ActionResult Users(int organizationId)
        {
            var client = GlobalHelper.GetClient(base.Session);

            var organizationUsers = client.Get(new OrganizationUsers { OrganizationId = organizationId }).Users;
            var organization = client.Get(new OrganizationDto { Id = organizationId }).Organization;

            var model = new OrganizationAlliesModel();
            model.OrganizationId = organizationId;
            model.OrganizationName = organization.Name;
            model.Users = organizationUsers.Select(o => o.ToDetailModel()).ToList();

            return View(model);
        }
    }
}
