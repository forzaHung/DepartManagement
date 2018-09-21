using Dispatch;
using Framework.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DispatchManagement.Helper;

namespace DispatchManagement.Controllers
{
    public class HolidayController : BaseController
    {
        // GET: Holiday
        private IHoliday _iplHoliday;
        public HolidayController() {
            _iplHoliday = SingletonIpl.GetInstance<IplHoliday>();
        }
        public ActionResult Index()
        {
            ViewBag.FromDate = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            ViewBag.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }
        public ActionResult CreateAndEdit() {
            ViewBag.Err = TempData["Err"];
            return View();
        }
        public ActionResult Save(HolidayEntity entity)
        {
            if (_iplHoliday.Detail(entity.HolidayDate) != null)
            {
                TempData["Err"] = "Ngày lễ này đã tồn tại.";
                return RedirectToAction("CreateAndEdit");
            }
            if (entity.Id < 1)
            {
                _iplHoliday.Insert(entity);
            }
            return RedirectToAction("Index");
        }
        public JsonResult GetHolidayList()
        {
            int totalRow = 0;
            string fromDate = Request.QueryString["fromDate"];
            string toDate = Request.QueryString["toDate"];
            DateTime FromDate;
            DateTime ToDate;
            if (string.IsNullOrWhiteSpace(fromDate))
            {
                var a = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                FromDate = DateTime.ParseExact(a, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
                FromDate = DateTime.ParseExact(fromDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            if (string.IsNullOrWhiteSpace(toDate))
                ToDate = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd"), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            else
                ToDate = DateTime.ParseExact(toDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            var page = GetPagingMessage(Request.QueryString);
            int result = DateTime.Compare(ToDate, FromDate);
            if (result < 0)
            {
                return Json(new
                {
                    status = false,
                    mess = "Ngày bắt đầu không thể lớn hơn ngày kết thúc.",
                }, JsonRequestBehavior.AllowGet);
            }

            var regulationTime = _iplHoliday.ListHolidayAllPaging(FromDate, ToDate, page.PageIndex, page.PageSize, ref totalRow);

            ViewBag.FromDate = FromDate.ToString("yyyy-MM-dd");
            ViewBag.ToDate = ToDate.ToString("yyyy-MM-dd");
            if (regulationTime != null && regulationTime.Count() > 0)
                return Json(new
                {
                    status = true,
                    rows = regulationTime.ToList(),
                }, JsonRequestBehavior.AllowGet);
            else
                return Json(new
                {
                    status = false,
                    mess = "Không tìm thấy dữ liệu.",
                }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(long Id){
            if (_iplHoliday.Delete(Id))
            {
                return Json(new
                {
                    stt = true
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                stt = false
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ChangeDescription(long Id, string description)
        {
            var save = _iplHoliday.ChangeDescription(Id, description);
            if (save)
                return Json(new { alertContent = "Thay đổi thành công" });
            else
                return Json(new { alertContent = "Có lỗi trong quá trình chỉnh sửa" });
        }
    }
}