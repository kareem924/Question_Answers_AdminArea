using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccess.UnitOfWork;
using PagedList;
using GawayzAdmin.ViewModels;

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
            return PartialView("_CreateProduct",model);
        }
        [HttpPost]
        public ActionResult Create(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
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
          
            return PartialView("_EditProduct", Helpers.CreateProductViewModelFromProducts(product));
        }
        [HttpPost]
        public ActionResult Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_EditProduct", model);
            }
            try
            {
                _uow.Products.Edit(model.ProductId, Helpers.CreateProductFromProductViewModel(model));
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