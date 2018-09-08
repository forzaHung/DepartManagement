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
    public class DispatchTypeController : BaseController
    {
        private IDispatchType _ipldispatchtype;
        // GET: DispatchType
        public DispatchTypeController()
        {
            _ipldispatchtype = SingletonIpl.GetInstance<IplDispatchType>();
        }
        public ActionResult Index(string searchString = "", int page = 1, int pageSize = 10)
        {
            int totalRow = 0;

            var dispatchtype = _ipldispatchtype.ListAllPaging(searchString, page, pageSize, ref totalRow);
            return View(dispatchtype.ToPagedList(page, pageSize, totalRow));
        }
        public PartialViewResult GetDispatchType(string searchString = "", int page = 1, int pageSize = 10)
        {
            int totalRow = 0;

            var dispatchtype = this._ipldispatchtype.ListAllPaging(searchString, page, pageSize, ref totalRow).ToPagedList(page, pageSize, totalRow);

            return PartialView("_DispatchTypeList", dispatchtype);
        }

        //[AuthorizeUser(ModuleName = "Department", AccessLevel = Constants.Add)]
        public ActionResult CreateDispatchType(DispatchTypeEntity entity)
        {

            return View(entity);
        }

        //[AuthorizeUser(ModuleName = "Employee", AccessLevel = Constants.Edit)]
        public ActionResult EditDispatchType(int? id)
        {

            DispatchTypeEntity entity = new DispatchTypeEntity();
            if (id != null)
            {
                entity = _ipldispatchtype.ViewDetail((int)id);
            }
            return View("CreateDispatchType", entity);
        }

        public ActionResult Save(DispatchTypeEntity model)
        {
            var entity = new DispatchTypeEntity();
            //if (ModelState.IsValid)
            //{
            if (model.Id > 0)
            {
                var sess = SessionSystem.GetUser();
                Logs.logs("Sửa Loại công văn", "Truy cập vào trang DispatchType", "/DispatchType/CreateDispatchType", sess.UserId);
                entity = _ipldispatchtype.ViewDetail(model.Id);

                if (entity != null && entity.Id > 0)
                {
                    entity.Name = model.Name;

                    var retVal = _ipldispatchtype.Update(entity);
                    if (retVal)
                    {
                        return RedirectToAction("Index", "DispatchType");
                    }
                }
            }
            else
            {
                var sess = SessionSystem.GetUser();
                Logs.logs("Thêm mới Loại công văn", "Truy cập vào trang DispatchType", "/DispatchType/CreateDispatchType", sess.UserId);
                model.Createdate = DateTime.Now;
                var empId = _ipldispatchtype.Insert(model);
                if (empId > 0)
                {
                    return RedirectToAction("Index", "DispatchType", new { id = empId });
                }

            }
            //}
            ViewBag.Msg = ConstantMsg.ErrorProgress;
            return View("DispatchType", model);
        }
    }
}