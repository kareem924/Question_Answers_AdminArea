using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccess.UnitOfWork;
using PagedList;
using WebAdmin.ViewModels;

namespace WebAdmin.Controllers
{
    public class CompanyController : Controller
    {
        IUnitOfWork _uow;

        public CompanyController()
        {
            _uow = new UnitOfWork();
        }
        // GET: Company
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult CompanyList(int? page)
        {
            var companyList = _uow.Companies.List().OrderBy(x => x.CompanyID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var onePageOfcompany = companyList.ToPagedList(pageNumber, pageSize);
            ViewBag.OnePageOfcompany = onePageOfcompany;
            return PartialView("_CompanyList");
        }

        [HttpGet]
        public PartialViewResult Create()
        {
            return PartialView("_CreateCompany");
        }
        [HttpPost]
        public ActionResult Create(CompanyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_CreateCompany", model);
            }
            try
            {
                _uow.Companies.Add(Helpers.CreateCompanyFromCompanyViewModel(model));
                _uow.Save();
                string url = Url.Action("CompanyList", "Company");
                return Json(new { success = true, url = url });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex });
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var company = _uow.Companies.Find(id);
            return PartialView("_EditCompany", Helpers.CreateCompanyviewmodelFromCompanies(company));
        }
        [HttpPost]
        public ActionResult Edit(CompanyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_CreateCompany", model);
            }
            try
            {
                _uow.Companies.Edit(model.CompanyId,Helpers.CreateCompanyFromCompanyViewModel(model));
                _uow.Save();
                string url = Url.Action("CompanyList", "Company");
                return Json(new { success = true, url = url });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex });
            }
        }
      
    }
}