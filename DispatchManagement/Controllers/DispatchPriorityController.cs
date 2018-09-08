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
    public class DispatchPriorityController : BaseController
    {
        private IDispatchPriority _ipldispatchpriority;
        // GET: DispatchPriority
        public DispatchPriorityController()
        {
            _ipldispatchpriority = SingletonIpl.GetInstance<IplDispatchPriority>();
        }
        public ActionResult Index(string searchString = "", int page = 1, int pageSize = 10)
        {
            int totalRow = 0;

            var dispatchpriority = _ipldispatchpriority.ListAllPaging(searchString, page, pageSize, ref totalRow);
            return View(dispatchpriority.ToPagedList(page, pageSize, totalRow));
        }
        public PartialViewResult GetDispatchPriority(string searchString = "", int page = 1, int pageSize = 10)
        {
            int totalRow = 0;

            var dispatchpriority = this._ipldispatchpriority.ListAllPaging(searchString, page, pageSize, ref totalRow).ToPagedList(page, pageSize, totalRow);

            return PartialView("_DispatchPriorityList", dispatchpriority);
        }

        //[AuthorizeUser(ModuleName = "Department", AccessLevel = Constants.Add)]
        public ActionResult CreateDispatchPriority(DispatchPriorityEntity entity)
        {

            return View(entity);
        }

        //[AuthorizeUser(ModuleName = "Employee", AccessLevel = Constants.Edit)]
        public ActionResult EditDispatchPriority(int? id)
        {

            DispatchPriorityEntity entity = new DispatchPriorityEntity();
            if (id != null)
            {
                entity = _ipldispatchpriority.ViewDetail((int)id);
            }
            return View("CreateDispatchPriority", entity);
        }

        public ActionResult Save(DispatchPriorityEntity model)
        {
            var entity = new DispatchPriorityEntity();
            //if (ModelState.IsValid)
            //{
            if (model.Id > 0)
            {
                var sess = SessionSystem.GetUser();
                Logs.logs("Sửa Độ ưu tiên công văn", "Truy cập vào trang DispatchPriority", "/DispatchPriority/CreateDispatchPriority", sess.UserId);
                entity = _ipldispatchpriority.ViewDetail(model.Id);

                if (entity != null && entity.Id > 0)
                {
                    entity.Name = model.Name;

                    var retVal = _ipldispatchpriority.Update(entity);
                    if (retVal)
                    {
                        return RedirectToAction("Index", "DispatchPriority");
                    }
                }
            }
            else
            {
                var sess = SessionSystem.GetUser();
                Logs.logs("Thêm Độ ưu tiên công văn", "Truy cập vào trang DispatchPriority", "/DispatchPriority/CreateDispatchPriority", sess.UserId);
                model.CreateDate = DateTime.Now;
                var empId = _ipldispatchpriority.Insert(model);
                if (empId > 0)
                {
                    return RedirectToAction("Index", "DispatchPriority", new { id = empId });
                }

            }
            //}
            ViewBag.Msg = ConstantMsg.ErrorProgress;
            return View("DispatchPriority", model);
        }
    }
}