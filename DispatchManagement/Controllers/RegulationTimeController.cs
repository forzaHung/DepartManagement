using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dispatch;
using Framework.EF;

namespace DispatchManagement.Controllers
{
    public class RegulationTimeController : BaseController
    {
        private IPosition _IplPosition;
        private IDepartment _IplDepartment;
        private IRegulationTime _IplRegulationTime;
        public RegulationTimeController() {
            _IplPosition = SingletonIpl.GetInstance<IplPosition>();
            _IplDepartment = SingletonIpl.GetInstance<IplDepartment>();
            _IplRegulationTime = SingletonIpl.GetInstance<IplRegulationTime>();
        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetRegulationTimeList()
        {
            int totalRow = 0;
            string searchString = Request.QueryString["keyword"];
            var page = GetPagingMessage(Request.QueryString);
            var regulationTime = _IplRegulationTime.ListAllPaging(searchString, page.PageIndex, page.PageSize, ref totalRow);
            if (regulationTime != null && regulationTime.Count() > 0)
                return Json(new
                {
                    status = true,
                    rows = regulationTime,
                    total = totalRow
                }, JsonRequestBehavior.AllowGet);
            else
                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ChangeAllowedLate(int Id, int allowedLate)
        {
            var save = _IplRegulationTime.ChangeAllowedLate(Id, allowedLate);
            if (save)
                return Json(new { alertContent = "Thay đổi thành công" });
            else
                return Json(new { alertContent = "Có lỗi trong quá trình chỉnh sửa" });
        }
        public JsonResult ChangeAllowedEarly(int Id, int allowedEarly)
        {
            var save = _IplRegulationTime.ChangeAllowEarly(Id, allowedEarly);
            if (save)
                return Json(new { alertContent = "Thay đổi thành công" });
            else
                return Json(new { alertContent = "Có lỗi trong quá trình chỉnh sửa" });
        }
        public JsonResult ChangeTimesLate(int Id, int timesLate)
        {
            var save = _IplRegulationTime.ChangeTimesLate(Id, timesLate);
            if (save)
                return Json(new { alertContent = "Thay đổi thành công" });
            else
                return Json(new { alertContent = "Có lỗi trong quá trình chỉnh sửa" });
        }
        public JsonResult Delete(int Id) {
            var delete = _IplRegulationTime.Delete(Id);
            return Json(new { status = true });
        }
        public ActionResult CreateRegulationTime(RegulationTimeEntity entity)
        {
            ViewData["Position"] = _IplPosition.ListAll();
            ViewData["Department"] = _IplDepartment.ListAllByTreeView();
            try
            {
                ViewBag.Err = TempData["Err"].ToString();
            }
            catch (Exception)
            {
            }
            return View(entity);
        }
        public ActionResult Save(FormCollection colection){
            ViewData["Position"] = _IplPosition.ListAll();
            ViewData["Department"] = _IplDepartment.ListAllByTreeView();
            if (!string.IsNullOrEmpty(colection["Id"]) && int.Parse(colection["Id"]) == 0)
            {
                var entity = new RegulationTimeEntity();
                try
                {
                    entity.IdDepartment = int.Parse(colection["DepartmentId"]);
                    entity.IdPosition = int.Parse(colection["PositionId"]);
                    entity.AllowedLate = int.Parse(colection["AllowedLate"]);
                    entity.AllowedEarly = int.Parse(colection["AllowedEarly"]);
                    entity.TimesLate = int.Parse(colection["TimesLate"]);
                    if (entity.IdDepartment < 0 || entity.IdPosition < 0 || entity.AllowedLate < 0 || entity.AllowedEarly < 0 || entity.TimesLate < 0)
                    {
                        TempData["Err"] = "Chỉ được nhập số lớn hơn hoặc bằng 0";
                        return RedirectToAction("CreateRegulationTime");
                    }
                    var listAll = _IplRegulationTime.ListAll();
                    if (listAll != null && listAll.Count() > 0)
                    {
                        foreach (var item in listAll)
                        {
                            if (item.IdDepartment == entity.IdDepartment && item.IdPosition == entity.IdPosition)
                            {
                                TempData["Err"] = "Đã quy định ra vào công ty cho chức vụ "+ _IplPosition.ViewDetail(item.IdPosition).Name + " phòng " + _IplDepartment.ViewDetail(item.IdDepartment).Name;
                                return RedirectToAction("CreateRegulationTime");
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    TempData["Err"] = "Chỉ được nhập số";
                    return RedirectToAction("CreateRegulationTime");
                }
                if (_IplRegulationTime.Insert(entity))
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("CreateRegulationTime");
            }
            else
                return RedirectToAction("CreateRegulationTime");
        }
        public JsonResult CheckPositionDepartment(int IdDepartment, int IdPosition)
        {
            var listAll = _IplRegulationTime.ListAll();
            if (listAll != null && listAll.Count() > 0)
            {
                foreach (var item in listAll)
                {
                    if (item.IdDepartment == IdDepartment && item.IdPosition == IdPosition)
                    {
                        return Json(new { stt = false, mess = "Đã quy định ra vào công ty cho chức vụ " + _IplPosition.ViewDetail(item.IdPosition).Name + " phòng " + _IplDepartment.ViewDetail(item.IdDepartment).Name });
                    }
                }
            }
            return Json(new { stt = true });
        }
    }
}