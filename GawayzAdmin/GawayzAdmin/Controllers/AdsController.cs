using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Entities;
using DataAccess.UnitOfWork;
using GawayzAdmin.ViewModels;
using PagedList;

namespace GawayzAdmin.Controllers
{
    public class AdsController : Controller
    {
        private IUnitOfWork _uow;

        public AdsController()
        {
            _uow=new UnitOfWork();
        }
        // GET: Ads
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdsList(int? page)
        {
            var adsList = _uow.Ads.List().OrderBy(x => x.ID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var onePageOfAds = adsList.ToPagedList(pageNumber, pageSize);
            ViewBag.OnePageOfAds = onePageOfAds;
            return PartialView("_AdsList");
        }

        public ActionResult Create()
        {
            return View("_Create");
        }
        [HttpPost]
        public ActionResult Create(AdsViewModel model)
        {
          
            if (!ModelState.IsValid)
            {
                return View("_Create", model);
            }
            if (model.File.ContentLength > (2 * 1024 * 1024))
            {
                ModelState.AddModelError("File", "File size must be less than 2 MB");
                return View("_Create", model);
            }
            if (!(model.File.ContentType == "image/jpeg" || model.File.ContentType == "image/png" || model.File.ContentType == "image/gif"))
            {
                ModelState.AddModelError("File", "File type allowed : jpeg and gif and png");
                return View("_Create", model);
            }
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                string targetFolder = HttpContext.Server.MapPath("~/Content/Ads");
                string targetPath = Path.Combine(targetFolder,model.File.FileName);
                file.SaveAs(targetPath);
            }
            var ads = new Ads
            {
                NavigateUrl = model.NavigateUrl,
                AlternateText = model.AlternateText,
                Height = model.Height,
                Width = model.Width,
                Keyword = model.Keyword,
                SideAd = model.SideAd,
                Impressions = model.Impressions
            };
            byte[] data = new byte[model.File.ContentLength];
            model.File.InputStream.Read(data, 0, model.File.ContentLength);
            ads.Image = data;
            try
            {
                _uow.Ads.Add(ads);
                _uow.Save();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex });
            }
      
        }
    }
}