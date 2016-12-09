using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using DataAccess.UnitOfWork;
using GawayzAdmin.ViewModels;
using PagedList;


namespace GawayzAdmin.Controllers
{
    [ValidateInput(false)]
    public class CodeController : Controller
    {
        private IUnitOfWork _uow;

        public CodeController()
        {
            _uow = new UnitOfWork();
        }
        // GET: Code
        public ActionResult Index(int productId)
        {
            ViewBag.productId = productId;
            return View();
        }
        public ActionResult CodeList(int? page, int productId)
        {
            var codesList = _uow.ProductsCodes.List().ToList().Where(x => x.ProductID == productId).GroupBy(x => x.CreatedDate.Date);
            int pageSize = 10;

            int pageNumber = (page ?? 1);
            var onePageOfCodes = codesList.ToPagedList(pageNumber, pageSize).Select(x => new GroupByData() { CreatedAt = x.Key });
            ViewBag.OnePageOfCodes = onePageOfCodes;
            ViewBag.ProductId = productId;
            return PartialView("_CodeList");
        }

        public ActionResult Create(int productId)
        {
            var model = new CodeViewModel() { ProductId = productId };
            ViewBag.Codes = new List<string>();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(CodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var codes = RandomString(model.Size);
                ViewBag.Codes = codes;
                Session["Codes"] = codes;
                foreach (var code in codes)
                {
                    model.Code = code;
                    _uow.ProductsCodes.Add(Helpers.CreateProductCodesFromCodesViewModel(model));
                }

                _uow.Save();
                return View(model);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex });
            }
            return View();
        }
        public ActionResult Delete(int productId)
        {
            return View();
        }
        [ValidateInput(false)]
        public ActionResult ExportData(string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                var codes = _uow.ProductsCodes.List().ToList().Where(x => x.CreatedDate.Date == Convert.ToDateTime(date));
                DataTable dt = new DataTable();
                dt.TableName = "Code";
                dt.Columns.Add("CODE");
                foreach (var element in codes)
                {
                    var row = dt.NewRow();
                    row["CODE"] = element.Code;
                    dt.Rows.Add(row);
                }
                using (XLWorkbook wb = new XLWorkbook())
                {

                    wb.Worksheets.Add(dt);
                    wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wb.Style.Font.Bold = true;
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename= EmployeeReport.xlsx");
                    using (var ms = new MemoryStream())
                    {
                        wb.SaveAs(ms);
                        ms.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }

                return Content("");
            }
            var codesSession = Session["Codes"] as List<string>;
            DataTable dts = new DataTable();
            dts.TableName = "Code";
            dts.Columns.Add("CODE");
            if (codesSession != null)
                foreach (var element in codesSession)
                {
                    var row = dts.NewRow();
                    row["CODE"] = element;
                    dts.Rows.Add(row);
                }
            using (XLWorkbook wb = new XLWorkbook())
            {

                wb.Worksheets.Add(dts);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= EmployeeReport.xlsx");
                using (var ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    ms.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            Session["Codes"] = null;
            return Content("");
        }
        private List<string> RandomString(int size)
        {
            var codes = new List<string>();
            Random rand = new Random();
            for (int x = 0; x < size; x++)
            {

                string input = "abcdefghijklmnopqrstuvwxyz0123456789";
                StringBuilder builder = new StringBuilder();
                char ch;
                for (int i = 0; i < 14; i++)
                {
                    ch = input[rand.Next(0, input.Length)];
                    builder.Append(ch);
                }
                codes.Add(builder.ToString());
            }

            return codes;
        }
    }
}