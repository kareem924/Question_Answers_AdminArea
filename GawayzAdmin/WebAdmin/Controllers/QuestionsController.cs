using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.UnitOfWork;
using PagedList;
using WebAdmin.ViewModels;

namespace WebAdmin.Controllers
{
    public class QuestionsController : Controller
    {
        IUnitOfWork _uow;
  
        public QuestionsController()
        {
            _uow = new UnitOfWork();
        }
        // GET: Questions
        public ActionResult Index(int? page)
        {
            var questuinList = _uow.Questions.List().OrderBy(x => x.QuestionID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var onePageOfQuestion = questuinList.ToPagedList(pageNumber, pageSize);
            ViewBag.OnePageOfQuestion = onePageOfQuestion;
            return View();
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var question = _uow.Questions.Find(id.Value);
            if (question == null)
            {
                return HttpNotFound();
            }
            var questionViewModel = ViewModels.Helpers.CreateQuestionsViewModelFromQuestions(question);
            return View(questionViewModel);
        }


        public ActionResult Create()
        {
            var salesOrderViewModel = new QuestionsViewModel();
            salesOrderViewModel.ObjectState = ObjectState.Added;
            return View(salesOrderViewModel);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var question = _uow.Questions.Find(id.Value);
            if (question == null)
            {
                return HttpNotFound();
            }
            var questionViewModel = ViewModels.Helpers.CreateQuestionsViewModelFromQuestions(question);
            return View(questionViewModel);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var question = _uow.Questions.Find(id.Value);
            if (question == null)
            {
                return HttpNotFound();
            }

            var questionViewModel = ViewModels.Helpers.CreateQuestionsViewModelFromQuestions(question);
            questionViewModel.ObjectState = ObjectState.Deleted;

            return View(questionViewModel);
        }





        [HandleModelStateException]
        public JsonResult Save(QuestionsViewModel questionsViewModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelStateException(ModelState);
            }

            var question = ViewModels.Helpers.CreateQuestionsFromQuestionsViewModel(questionsViewModel);
            if (question.ObjectState == ObjectState.Added)
            { _uow.Questions.Add(question); }
            else
            {
                _uow.Questions.Edit(question.QuestionID, question);
            }
            if (question.ObjectState == ObjectState.Deleted)
            {
                foreach (var choiceItem in questionsViewModel.ChoicesItems)
                {
                    var choice = _uow.Choices.Find(choiceItem.ChoiceId);
                    if (choice != null)
                        choice.ObjectState = ObjectState.Deleted;
                }
            }
            else
            {
                foreach (int choiceId in questionsViewModel.ChoicesToDelete)
                {
                    var choice = _uow.Choices.Find(choiceId);
                    if (choice != null)
                        choice.ObjectState = ObjectState.Deleted;
                }
            }


            string messageToClient = string.Empty;

            try
            {
                _uow.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                messageToClient = "Someone else have modified this sales order since you retrieved it.  Your changes have not been applied.  What you see now are the current values in the database.";
            }
            catch (Exception ex)
            {
                throw new ModelStateException(ex);
            }

            return Json(new { newLocation = "/Questions/Index/" });


        }
    }
}