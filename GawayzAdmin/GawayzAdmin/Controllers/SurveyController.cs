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
    public class SurveyController : Controller
    {
        IUnitOfWork _uow;

        public SurveyController()
        {
            _uow = new UnitOfWork();
        }
        // GET: Survey
        public ActionResult Index(int productId)
        {
            ViewBag.productId = productId;
            return View();
        }

        public ActionResult SurveyList(int? page, int productId)
        {
            var surveyList = _uow.ProductsSurveyQuestions.List().Where(x => x.ProductID == productId ).OrderBy(x => x.SurveyQuestionID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var onePageOfSurveys = surveyList.ToPagedList(pageNumber, pageSize);
            ViewBag.OnePageOfSurvey = onePageOfSurveys;
            ViewBag.ProductId = productId;
            return PartialView("_SurveyList");
        }
        [HttpGet]
        public ActionResult Create(int productId)
        {
            var model = new SurveyViewModel() { ProductId = productId };
            return PartialView("_CreateSurvey", model);
        }
        [HttpPost]
        public ActionResult Create(SurveyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_CreateSurvey", model);
            }
            try
            {
               
                _uow.ProductsSurveyQuestions.Add(Helpers.CreateSurveyFromSurveyViewModel(model));
                _uow.Save();
                string url = Url.Action("SurveyList", "Survey", new { ProductId = model.ProductId });
                return Json(new { success = true, url = url , tableId = "surveyTable" });
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

            var survey = _uow.ProductsSurveyQuestions.Find(id);

            return PartialView("_EditSurvey", Helpers.CreateSurveyViewModelFromSurvey(survey));
        }
        [HttpPost]
        public ActionResult Edit(SurveyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_EditSurvey", model);
            }
            try
            {
                _uow.ProductsSurveyQuestions.Edit(model.SurveyQuestionId, Helpers.CreateSurveyFromSurveyViewModel(model));
                _uow.Save();
                string url = Url.Action("SurveyList", "Survey", new { ProductId = model.ProductId });
                return Json(new { success = true, url = url, tableId = "surveyTable" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex });
            }
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var surveyToDelete = _uow.ProductsSurveyQuestions.Find(id.Value);
            if (surveyToDelete == null)
            {
                return HttpNotFound();
            }
            var model = Helpers.CreateSurveyViewModelFromSurvey(surveyToDelete);
            return PartialView("_DeleteSurvey", model);
        }

        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int QuestionId, int ProductId)
        {

            _uow.ProductsSurveyQuestions.Delete(QuestionId);
            _uow.Save();
            string url = Url.Action("SurveyList", "Survey", new { ProductId = ProductId });
            return Json(new { success = true, url = url, tableId = "surveyTable" });

        }

    }
}