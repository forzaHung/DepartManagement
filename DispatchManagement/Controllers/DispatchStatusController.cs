using Framework.Configuration;
using Framework.EF;
using Dispatch;
using MvcPaging;
using DispatchManagement.Models;
using Prototype.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DispatchManagement.Controllers
{
    public class DispatchStatusController : BaseController
    {
        private IDispatchStatus _ipldispatchstatus;
        // GET: DispatchStatus
        public DispatchStatusController()
        {
            _ipldispatchstatus = SingletonIpl.GetInstance<IplDispatchStatus>();
        }
        public ActionResult Index(string searchString = "", int page = 1, int pageSize = 10)
        {
            int totalRow = 0;

            var dispatchstatus = _ipldispatchstatus.ListAllPaging(searchString, page, pageSize, ref totalRow);
            return View(dispatchstatus.ToPagedList(page, pageSize, totalRow));
        }
        public PartialViewResult GetDispatchStatus(string searchString = "", int page = 1, int pageSize = 10)
        {
            int totalRow = 0;

            var dispatchstatus = this._ipldispatchstatus.ListAllPaging(searchString, page, pageSize, ref totalRow).ToPagedList(page, pageSize, totalRow);

            return PartialView("_DispatchStatusList", dispatchstatus);
        }

        //[AuthorizeUser(ModuleName = "Department", AccessLevel = Constants.Add)]
        public ActionResult CreateDispatchStatus(DispatchStatusEntity entity)
        {

            return View(entity);
        }

        //[AuthorizeUser(ModuleName = "Employee", AccessLevel = Constants.Edit)]
        public ActionResult EditDispatchStatus(int? id)
        {

            DispatchStatusEntity entity = new DispatchStatusEntity();
            if (id != null)
            {
                entity = _ipldispatchstatus.ViewDetail((int)id);
            }
            return View("CreateDispatchStatus", entity);
        }

        public ActionResult Save(DispatchStatusEntity model)
        {
            var entity = new DispatchStatusEntity();
            //if (ModelState.IsValid)
            //{
            if (model.Id > 0)
            {
                var sess = SessionSystem.GetUser();
                Logs.logs("Sửa Tình trạng công văn", "Truy cập vào trang DispatchStatus", "/DispatchStatus/CreateDispatchStatus", sess.UserId);
                entity = _ipldispatchstatus.ViewDetail(model.Id);

                if (entity != null && entity.Id > 0)
                {
                    entity.Name = model.Name;

                    var retVal = _ipldispatchstatus.Update(entity);
                    if (retVal)
                    {
                        return RedirectToAction("Index", "DispatchStatus");
                    }
                }
            }
            else
            {
                var sess = SessionSystem.GetUser();
                Logs.logs("Thêm Tình trạng công văn", "Truy cập vào trang DispatchStatus", "/DispatchStatus/CreateDispatchStatus", sess.UserId);
                model.CreateDate = DateTime.Now;
                var empId = _ipldispatchstatus.Insert(model);
                if (empId > 0)
                {
                    return RedirectToAction("Index", "DispatchStatus", new { id = empId });
                }

            }
            //}
            ViewBag.Msg = ConstantMsg.ErrorProgress;
            return View("DispatchStatus", model);
        }
    }
}