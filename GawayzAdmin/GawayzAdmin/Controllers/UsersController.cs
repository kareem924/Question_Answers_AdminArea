using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using DataAccess.Entities;
using DataAccess.UnitOfWork;
using Newtonsoft.Json.Linq;
using PagedList;

using GawayzAdmin.SecurityClasses;
using GawayzAdmin.ViewModels;

namespace GawayzAdmin.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        IUnitOfWork _uow;
        LoginRepository _objRepository;
        public UsersController()
        {
            _uow = new UnitOfWork();
            _objRepository = new LoginRepository();
        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserList(int? page)
        {
            var userList = _uow.Users.List().OrderBy(x => x.UserId);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var onePageOfusers = userList.ToPagedList(pageNumber, pageSize);
            ViewBag.OnePageOfusers = onePageOfusers;
            return PartialView("_UserList");
        }
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView("_CreateUser");
        }
        [HttpPost]
        public ActionResult Create(UsersViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_CreateUser", model);
            }
            try
            {
                _uow.Users.Add(Helpers.CreateUsersFromUserViewModel(model));
                _uow.Save();
                string url = Url.Action("UserList", "Users");
                return Json(new { success = true, url = url, tableId = "usersTable" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex });
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Users user = null;
            if (id == 0)
            {

                user = _uow.Users.Find(SessionManager.CurrentUser.UserId);
            }


            if (id != null) user = _uow.Users.Find(id.Value);
            return PartialView("_EditUser", Helpers.CreatEditUserViewModelFromUser(user));
        }
        [HttpPost]
        public ActionResult Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_EditUser", model);
            }
            try
            {
                _uow.Users.Edit(model.UserId, Helpers.CreateUserFromEditUserViewModel(model));
                _uow.Save();
                string url = Url.Action("UserList", "Users");
                return Json(new { success = true, url = url, tableId = "usersTable" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex });
            }
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return PartialView("_ChangePassword");
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePassword model)
        {
            var existing = _uow.Users.Find(SessionManager.CurrentUser.UserId);
            if (existing == null)
            {
                return HttpNotFound();
            }


            if (ModelState.IsValid)
            {
                string oldPassword = StringCipher.Decrypt(existing.Password, WebConfigurationManager.AppSettings["EncDecKey"]);
                if (oldPassword != model.OldPassword)
                {
                    ModelState.AddModelError("OldPassword", "it's not your old password");
                    return PartialView("_ChangePassword", model);
                }
                existing.Password = StringCipher.Encrypt(model.ConfirmPassword,
                    WebConfigurationManager.AppSettings["EncDecKey"]);

                _uow.Users.Edit(existing.UserId, existing);
                _uow.Save();

                return Json(new { success = true });
            }
            return PartialView("_ChangePassword", model);
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult LoginUser(UserLoginViewModel login)
        {
            try
            {
                string jsonResult = _objRepository.LoginUser(login);
                dynamic data = JObject.Parse(jsonResult);
                var message = data.message.ToString();
                if (data.success == "True")
                {
                    return Json(new { success = true, url = Url.Action("Index", "Company"), message = message });

                }
                else
                {
                    return Json(new { success = false, message = message });
                }

            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }


        }

        public ActionResult Logout()
        {
            SessionManager.LogoutUser();
            return RedirectToAction("Login");
        }

    }
}