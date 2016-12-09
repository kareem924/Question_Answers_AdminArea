using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccess.UnitOfWork;
using PagedList;
using GawayzAdmin.ViewModels;
using DataAccess.Entities;

namespace GawayzAdmin.Controllers
{
    public class ProductController : Controller
    {
        IUnitOfWork _uow;

        public ProductController()
        {
            _uow=new UnitOfWork();
        }
        // GET: Product
        public ActionResult Index(int companyId)
        {
            ViewBag.companyId = companyId;
            var business = _uow.BusinessRules.List().Select(c => new {
                BusinessRuleID = c.BusinessRuleID,
                BusinessRuleName = c.BusinessRuleName
            }).ToList();
            ViewBag.BusinessRoles = new MultiSelectList(business, "BusinessRuleID", "BusinessRuleName");
            return View();
        }

        public ActionResult ProductList(int? page, int companyId)
        {
            var productList = _uow.Products.List().Where(x=>x.CompanyID== companyId).OrderBy(x => x.CompanyID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var onePageOfproducts = productList.ToPagedList(pageNumber, pageSize);
            ViewBag.OnePageOfProducts = onePageOfproducts;
            ViewBag.companyId = companyId;
            return PartialView("_ProductList");
        }
        [HttpGet]
        public ActionResult Create(int companyId)
        {
          
            var model = new ProductViewModel() {CompanyIdKey = companyId};
            var business = _uow.BusinessRules.List().Select(c => new {
                BusinessRuleID = c.BusinessRuleID,
                BusinessRuleName = c.BusinessRuleName
            }).ToList();
            ViewBag.BusinessRoles = new MultiSelectList(business, "BusinessRuleID", "BusinessRuleName");
            return PartialView("_CreateProduct",model);
        }
        [HttpPost]
        public ActionResult Create(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var business = _uow.BusinessRules.List().Select(c => new {
                    BusinessRuleID = c.BusinessRuleID,
                    BusinessRuleName = c.BusinessRuleName
                }).ToList();
                ViewBag.BusinessRoles = new MultiSelectList(business, "BusinessRuleID", "BusinessRuleName");
                return PartialView("_CreateProduct", model);
            }
            try
            {
                _uow.Products.Add(Helpers.CreateProductFromProductViewModel(model));
                _uow.Save();
                string url = Url.Action("ProductList", "Product",new { companyId =model.CompanyIdKey});
                return Json(new { success = true, url = url, tableId = "productTable" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex });
            }
        }

        [HttpGet]
        public ActionResult Edit(int id,int companyId)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            var product = _uow.Products.Find(id);
            var business = _uow.BusinessRules.List().Select(c => new {
                BusinessRuleID = c.BusinessRuleID,
                BusinessRuleName = c.BusinessRuleName
            }).ToList();
            ViewBag.BusinessRoles = new MultiSelectList(business, "BusinessRuleID", "BusinessRuleName");
            return PartialView("_EditProduct", Helpers.CreateProductViewModelFromProducts(product));
        }
        [HttpPost]
        public ActionResult Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var business = _uow.BusinessRules.List().Select(c => new {
                    BusinessRuleID = c.BusinessRuleID,
                    BusinessRuleName = c.BusinessRuleName
                }).ToList();
                ViewBag.BusinessRoles = new MultiSelectList(business, "BusinessRuleID", "BusinessRuleName");
                return PartialView("_EditProduct", model);
            }
            try
            {
               var ClearBusinessRole= _uow.Products.Find(model.ProductId);
                
                foreach (var item in ClearBusinessRole.ProductsBusinessRules.ToList())
                {
                    _uow.ProductsBusinessRules.Delete(item.ProductBID);
                }
                ClearBusinessRole.Active = model.Active;
                ClearBusinessRole.ProductID = model.ProductId;
                ClearBusinessRole.CompanyID = model.CompanyIdKey;
                ClearBusinessRole.ProductName = model.ProductName;
                ClearBusinessRole.ProductImage = model.ProductImage;
                ClearBusinessRole.ProductOrder = model.ProductOrder;
                ClearBusinessRole.ProductQPerPage = model.ProductQPerPage;
                ClearBusinessRole.ProductSurveyQPerPage = model.ProductSurveyQPerPage;
                ClearBusinessRole.enProductDescription = model.EnProductDescription;
                ClearBusinessRole.arProductDescription = model.ArProductDescription;
                int order = 0;
                foreach (var item in model.BusinessOrder)
                {
                    order++;
                    ClearBusinessRole.ProductsBusinessRules.Add(new ProductsBusinessRules() { ProductID = model.ProductId, BusinessRuleID = item, BusinessRuleOrder = order });
                }
                _uow.Products.Edit(model.ProductId, ClearBusinessRole);
                _uow.Save();
                string url = Url.Action("ProductList", "Product", new { companyId = model.CompanyIdKey });
                return Json(new { success = true, url = url, tableId = "productTable" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex });
            }
        }

     
    }
}