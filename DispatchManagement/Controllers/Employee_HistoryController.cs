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
    public class Employee_HistoryController : BaseController
    {
        private IDepartment _iplDepartment;
        private IEmployee _iplEmployee;
        private IEmployee_History _iplEmployee_history;
        public Employee_HistoryController()
        {
            _iplDepartment = SingletonIpl.GetInstance<IplDepartment>();
            _iplEmployee = SingletonIpl.GetInstance<IplEmployee>();
            _iplEmployee_history = SingletonIpl.GetInstance<IplEmployee_History>();
        }
        // GET: Employee_History
        public ActionResult Index(string searchString = "", int page = 1, int pageSize = 10)
        {
            int totalRow = 0;
            //phan nay chua commit len

            var employee_history = _iplEmployee_history.ListAllPaging(searchString, page, pageSize, ref totalRow);
            return View(employee_history.ToPagedList(page, pageSize, totalRow));
        }
        public PartialViewResult GetEmployee_History(string searchString = "", int page = 1, int pageSize = 10)
        {
            int totalRow = 0;

            var employee_history = this._iplEmployee_history.ListAllPaging(searchString, page, pageSize, ref totalRow).ToPagedList(page, pageSize, totalRow);

            return PartialView("_Employee_HistoryList", employee_history);
        }


        public ActionResult EditEmployee_History(int? id)
        {
            var iplPosition = SingletonIpl.GetInstance<IplPosition>();
            var allPosition = iplPosition.ListAll();
            ViewData["Position"] = allPosition;

            var iplDepartment = SingletonIpl.GetInstance<IplDepartment>();
            var allDepartment = iplDepartment.ListAll();
            ViewData["Department"] = allDepartment;

            Employee_HistoryEntity entity = new Employee_HistoryEntity();
            if (id != null)
            {
                entity = _iplEmployee_history.ViewDetail((int)id);
            }
            return View(entity);
        }

        [ValidateInput(false)]
        public ActionResult Save(Employee_HistoryEntity model)
        {
            var entity = new Employee_HistoryEntity();

            var iplPosition = SingletonIpl.GetInstance<IplPosition>();
            var allPosition = iplPosition.ListAll();
            ViewData["Position"] = allPosition;

            var iplDepartment = SingletonIpl.GetInstance<IplDepartment>();
            var allDepartment = iplDepartment.ListAll();
            ViewData["Department"] = allDepartment;


            if (model.Id > 0)
            {
                var sess = SessionSystem.GetUser();
                Logs.logs("Sửa lịch sử nhân sự", "Truy cập vào trang Employee_History", "/Employee_History/EditEmployee_History", sess.UserId);
                entity = _iplEmployee_history.ViewDetail(model.Id);
                if (entity != null && entity.Id > 0)
                {
                    entity.EmployeeId = model.EmployeeId;
                    entity.DepartmentId = model.DepartmentId;
                    entity.Position = model.Position;
                    entity.TimeIn = model.TimeIn;
                    entity.TimeOut = model.TimeOut;
                    entity.Note = model.Note;
                    var retVal = _iplEmployee_history.Update(entity);
                    if (retVal)
                        return RedirectToAction("Index", "Employee_History");
                }
            }

            ViewBag.Msg = ConstantMsg.ErrorProgress;
            return View("EditEmployee_History", model);
        }

    }
}