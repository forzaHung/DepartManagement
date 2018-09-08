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
using Newtonsoft.Json;

namespace DispatchManagement.Controllers
{
    public class EmployeeController : BaseController
    {
        private IDepartment _iplDepartment;
        private IDispatchWork _iplDispatchWork;
        private IEmployee _iplEmployee;
        private IEmployee_History _iplEmployee_History;
        private IPosition _iplPosition;
        private IUser _iplUser;

        private string _imagePath = Config.GetConfigByKey("AvatarPath");
        public EmployeeController()
        {
            _iplDepartment = SingletonIpl.GetInstance<IplDepartment>();
            _iplDispatchWork = SingletonIpl.GetInstance<IplDispatchWork>();
            _iplEmployee = SingletonIpl.GetInstance<IplEmployee>();
            _iplEmployee_History = SingletonIpl.GetInstance<IplEmployee_History>();
            _iplPosition = SingletonIpl.GetInstance<IplPosition>();
            _iplUser = SingletonIpl.GetInstance<IplUser>();
        }
        // GET: Backend/Users
        //[AuthorizeUser(ModuleName = "Employee", AccessLevel = Constants.View)]
        public ActionResult Index(string searchString = "", int page = 1, int pageSize = 10)
        {
            int totalRow = 0;
            var employee = _iplEmployee.ListAllPaging(searchString, page, pageSize, ref totalRow);
            return View(employee.ToPagedList(page, pageSize, totalRow));
        }
        [AuthorizeUser(ModuleName = "Employee", AccessLevel = Constants.View)]
        public PartialViewResult GetEmployee(string searchString = "", int page = 1, int pageSize = 10)
        {
            int totalRow = 0;

            var employee = this._iplEmployee.ListAllPaging(searchString, page, pageSize, ref totalRow).ToPagedList(page, pageSize, totalRow);

            return PartialView("_UserList", employee);
        }
        //[AuthorizeUser(ModuleName = "Employee", AccessLevel = Constants.Add)]
        public ActionResult CreateEmployee(EmployeeEntity entity)
        {
            ViewData["Department"] = _iplDepartment.ListAllByTreeView();
            ViewData["DispatchWork"] = _iplDispatchWork.ListAllByTreeView();
            ViewData["Position"] = _iplPosition.ListAll();
            return View(entity);
        }
        //[AuthorizeUser(ModuleName = "Employee", AccessLevel = Constants.Edit)]
        public ActionResult EditEmployee(int? id)
        {
            EmployeeEntity entity = new EmployeeEntity();
            if (id != null)
            {
                ReturnFalse((int)id);
                entity = _iplEmployee.ViewDetail((int)id);
            }
            else
            {
                ViewData["Department"] = _iplDepartment.ListAllByTreeView();
                ViewData["DispatchWork"] = _iplDispatchWork.ListAllByTreeView();
                ViewData["Position"] = _iplPosition.ListAll();
            }
            return View("CreateEmployee", entity);
        }
        //[AuthorizeUser(ModuleName = "Employee", AccessLevel = Constants.Save)]
        public ActionResult Save(EmployeeEntity model, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                EmployeeEntity entity = _iplEmployee.ViewDetail(model.Id);
                var DepartmentId = form["DepartmentId"];
                var PositionId = form["Position"];
                string[] arrayDepartment;
                string[] arrayPosition;
                arrayDepartment = DepartmentId.Split(',');
                arrayPosition = PositionId.Split(',');
                var DispatchWorkId = form["DispatchWork"];
                string[] arrayDispatchWork;
                arrayDispatchWork = DispatchWorkId.Split(',');

                #region Sửa Employee
                if (model.Id > 0)
                {
                    var sess = SessionSystem.GetUser();
                    Logs.logs("Sửa nhân sự", "Truy cập vào trang Employee", "/Employee/CreateEmployee", sess.UserId);
                    #region Sửa lịch sử Chức Vụ, Phòng Ban
                    if (DepartmentId != null && PositionId != null && arrayDepartment.Count() > 0 && arrayPosition.Count() == arrayDepartment.Count())
                    {
                        //ListHistory là list thêm mới, sửa phòng, chức
                        List<Employee_HistoryEntity> listHistory = ListHistory(model.Id, arrayDepartment, arrayPosition);
                        //ListHistoryOld lấy ra thằng TimeOut = null và IdDepartment và IdPosition không có trong param hoặc bị xóa
                        List<Employee_HistoryEntity> EmpHisOld = ListHistoryOld(model.Id, arrayDepartment, arrayPosition);
                        if (listHistory != null && listHistory.Count() == 0 && EmpHisOld != null && EmpHisOld.Count() > 0)
                        {
                            foreach (var item in EmpHisOld)
                            {
                                var listHistoryOld = _iplEmployee_History.SelectByPositionDepartment(item.EmployeeId, item.Position, item.DepartmentId);
                                if (listHistoryOld != null)
                                {
                                    listHistoryOld.TimeOut = DateTime.Now;
                                    var updateEmpHis = _iplEmployee_History.Update(listHistoryOld);
                                }
                            }
                        }
                        if (listHistory != null && listHistory.Count() > 0)
                        {
                            //Lưu thời gian nghỉ
                            foreach (var item in EmpHisOld)
                            {
                                var listHistoryOld = _iplEmployee_History.SelectByPositionDepartment(item.EmployeeId, item.Position, item.DepartmentId);
                                if (listHistoryOld != null)
                                {
                                    listHistoryOld.TimeOut = DateTime.Now;
                                    var updateEmpHis = _iplEmployee_History.Update(listHistoryOld);
                                }
                            }
                            foreach (var item in listHistory)
                            {
                                var EmpHis = new Employee_HistoryEntity();
                                EmpHis.Position = item.Position;
                                EmpHis.DepartmentId = item.DepartmentId;
                                EmpHis.EmployeeId = model.Id;
                                EmpHis.TimeIn = DateTime.Now;
                                EmpHis.Note = item.Note != null ? item.Note : form["Department" + item.DepartmentId];
                                _iplEmployee_History.Insert(EmpHis);
                            }
                        }
                        //Xóa liên kết cũ
                        _iplEmployee.DeleteDepartment(model.Id);
                        //Lưu liên kết mới
                        for (int i = 0; i < arrayDepartment.Count(); i++)
                        {
                            Employee_Department a = new Employee_Department();
                            a.IdDepartment = int.Parse(arrayDepartment[i]);
                            a.IdEmployee = model.Id;
                            a.IdPosition = int.Parse(arrayPosition[i]);
                            _iplEmployee.InsertDepartment(a);
                        }
                    }
                    else
                    {
                        ReturnFalse(model.Id);
                        ViewBag.ErrPosition = "Chức vụ và phòng ban phải tương đồng.";
                        return View("CreateEmployee", model);
                    }
                    #endregion
                    #region Sửa Công Việc
                    if (DispatchWorkId != null)
                    {
                        //Xóa liên kết cũ
                        _iplEmployee.DeleteWork(model.Id);
                        //Lưu liên kết mới
                        foreach (var item in arrayDispatchWork)
                        {
                            Employee_DispatchWork a = new Employee_DispatchWork();
                            a.IdDispatchWork = int.Parse(item);
                            a.IdEmployee = model.Id;
                            _iplEmployee.InsertDispatchWork(a);
                        }
                    }
                    else
                    {
                        ReturnFalse(model.Id);
                        ViewBag.ErrDispatchWork = "Công việc không được để trống.";
                        return View("CreateEmployee", model);
                    }
                    #endregion
                    if (entity != null && entity.Id > 0)
                    {
                        entity.FirstName = model.FirstName;
                        entity.LastName = model.LastName;
                        entity.Phone = model.Phone;
                        entity.Address = model.Address;
                        entity.AcademicLevel = model.AcademicLevel;
                        entity.Birthday = model.Birthday;
                        entity.Gender = model.Gender;
                        entity.IsActive = model.IsActive;
                        entity.Position = model.Position;
                        entity.DepartmentId = model.DepartmentId;
                        if (model.file != null && model.file.ContentLength > 0)
                        {
                            if (!Utility.CheckImageFormat(model.file.FileName))
                            {
                                ViewBag.msgUpload = ConstantMsg.ErrorImageFormat;
                                return View(model);
                            }
                            var retUpload = Upload.upload(_imagePath, model.file, 250, 250);
                            entity.PicturePath = retUpload.pathThumb;
                        }
                        var retVal = _iplEmployee.Update(entity);

                        if (retVal)
                        {
                            return RedirectToAction("Index", "Employee");
                        }
                    }
                }
                #endregion
                #region Thêm mới Employee
                else
                {
                    var sess = SessionSystem.GetUser();
                    Logs.logs("Thêm nhân sự", "Truy cập vào trang Employee", "/Employee/CreateEmployee", sess.UserId);
                    if (model.file != null && model.file.ContentLength > 0)
                    {
                        if (!Utility.CheckImageFormat(model.file.FileName))
                        {
                            ViewBag.msgUpload = ConstantMsg.ErrorImageFormat;
                            return View(model);
                        }
                        var retUpload = Upload.upload(_imagePath, model.file, 250, 250);
                        model.PicturePath = retUpload.pathThumb;
                    }
                    var empId = _iplEmployee.Insert(model);

                    if (empId > 0)
                    {
                        for (int i = 0; i < arrayDepartment.Count(); i++)
                        {
                            var EmpHis = new Employee_HistoryEntity();
                            EmpHis.Position = int.Parse(arrayPosition[i]);
                            EmpHis.DepartmentId = int.Parse(arrayDepartment[i]);
                            EmpHis.EmployeeId = empId;
                            EmpHis.TimeIn = DateTime.Now;
                            EmpHis.Note = "Bắt đầu làm việc";
                            _iplEmployee_History.Insert(EmpHis);

                            var employee_Department = new Employee_Department();
                            employee_Department.IdDepartment = int.Parse(arrayDepartment[i]);
                            employee_Department.IdEmployee = empId;
                            employee_Department.IdPosition = int.Parse(arrayPosition[i]);
                            _iplEmployee.InsertDepartment(employee_Department);
                        }
                        //Lưu liên kết mới
                        foreach (var item in arrayDispatchWork)
                        {
                            Employee_DispatchWork a = new Employee_DispatchWork();
                            a.IdDispatchWork = int.Parse(item);
                            a.IdEmployee = empId;
                            _iplEmployee.InsertDispatchWork(a);
                        }
                        return RedirectToAction("Index", "User", new { id = empId });
                    }
                }
                #endregion
            }
            ViewBag.Msg = ConstantMsg.ErrorProgress;
            ReturnFalse(model.Id);
            return View("CreateEmployee", model);
        }
        public JsonResult NoteDepartment_Position(int IdEmployee, string[] arrDepartment, string[] arrPosition)
        {
            if (IdEmployee == 0 || arrDepartment.Count() != arrPosition.Count())
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);
            List<Employee_Department> ListHistory = _iplEmployee.ListDetail(IdEmployee);
            List<Employee_History> ListNote = new List<Employee_History>();
            for (int i = 0; i < arrDepartment.Count(); i++)
            {
                var countDepartment = ListHistory.Where(x => x.IdDepartment == int.Parse(arrDepartment[i])).FirstOrDefault();
                DepartmentEntity nameDepartmentNew = _iplDepartment.ViewDetail(int.Parse(arrDepartment[i]));
                if (ListHistory.Count() == arrDepartment.Count())
                {
                    if (countDepartment == null)
                    {
                        var a = new Employee_History
                        {
                            Note = "Chuyển sang phòng ban " + nameDepartmentNew.Name,
                            IdDepartment = int.Parse(arrDepartment[i]),
                            NameDepartment = "chuyển phòng " + nameDepartmentNew.Name
                        };
                        ListNote.Add(a);
                    }
                }
                else
                {
                    if (countDepartment == null)
                    {
                        var a = new Employee_History
                        {
                            Note = "Được phân công vào phòng ban mới.",
                            IdDepartment = int.Parse(arrDepartment[i]),
                            NameDepartment = "chuyển phòng " + nameDepartmentNew.Name
                        };
                        ListNote.Add(a);
                    }
                }
                if (countDepartment != null && countDepartment.IdPosition != int.Parse(arrPosition[i]))
                {
                    PositionEntity namePosition = _iplPosition.ViewDetail(int.Parse(arrPosition[i]));
                    if (countDepartment.IdPosition > namePosition.Id)
                    {
                        var a = new Employee_History
                        {
                            Note = "Chuyển chức vụ từ " + countDepartment.NamePosition + " phòng " + nameDepartmentNew.Name + " lên chức vụ " + namePosition.Name + " phòng " + nameDepartmentNew.Name,
                            IdDepartment = int.Parse(arrDepartment[i]),
                            NameDepartment = "chuyển chức " + nameDepartmentNew.Name
                        };
                        ListNote.Add(a);
                    }
                    else
                    {
                        var a = new Employee_History
                        {
                            Note = "Chuyển chức vụ từ " + countDepartment.NamePosition + " phòng " + nameDepartmentNew.Name + " xuống chức vụ " + namePosition.Name + " phòng " + nameDepartmentNew.Name,
                            IdDepartment = int.Parse(arrDepartment[i]),
                            NameDepartment = "chuyển chức " + nameDepartmentNew.Name
                        };
                        ListNote.Add(a);
                    }

                }
            }
            if (ListNote.Count() > 0)
                return Json(new { status = true, data = ListNote }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }
        public List<Employee_HistoryEntity> ListHistory(int IdEmployee, string[] arrDepartment, string[] arrPosition)
        {
            //List all employee_department theo IdEmployee
            List<Employee_Department> ListHistory = _iplEmployee.ListDetail(IdEmployee);
            List<Employee_HistoryEntity> listHistory = new List<Employee_HistoryEntity>();
            for (int i = 0; i < arrDepartment.Count(); i++)
            {
                var countDepartment = ListHistory.Where(x => x.IdDepartment == int.Parse(arrDepartment[i])).FirstOrDefault();
                //Thêm mới phòng ban hoặc chuyển phòng
                if (countDepartment == null)
                {
                    var a = new Employee_HistoryEntity
                    {
                        EmployeeId = IdEmployee,
                        TimeIn = DateTime.Now,
                        DepartmentId = int.Parse(arrDepartment[i]),
                        Position = int.Parse(arrPosition[i]),
                    };
                    listHistory.Add(a);
                }
                else
                {
                    //Chuyển chức
                    if (countDepartment.IdPosition != int.Parse(arrPosition[i]))
                    {
                        PositionEntity namePosition = _iplPosition.ViewDetail(int.Parse(arrPosition[i]));
                        var a = new Employee_HistoryEntity
                        {
                            EmployeeId = IdEmployee,
                            TimeIn = DateTime.Now,
                            DepartmentId = int.Parse(arrDepartment[i]),
                            Position = int.Parse(arrPosition[i]),
                        };
                        listHistory.Add(a);
                    }
                    else
                    {
                        //ko có thay đổi
                    }
                }
            }
            return listHistory;
        }
        public List<Employee_HistoryEntity> ListHistoryOld(int IdEmployee, string[] arrDepartment, string[] arrPosition)
        {
            //Lấy list Timeout = null
            List<Employee_HistoryEntity> ListAllTimeOutNull = _iplEmployee_History.ListAllTimeOutNull(IdEmployee);
            List<Employee_HistoryEntity> listHistoryOld = new List<Employee_HistoryEntity>();
            foreach (var item in ListAllTimeOutNull)
            {
                if (item.DepartmentId == 0 || item.Position == 0)
                {
                    listHistoryOld.Add(item);
                }
                else
                {
                    var check = true;
                    for (int i = 0; i < arrDepartment.Count(); i++)
                    {
                        if (item.DepartmentId.ToString() == arrDepartment[i] && item.Position.ToString() == arrPosition[i])
                        {
                            check = false;
                        }
                    }
                    if (check == true)
                    {
                        listHistoryOld.Add(item);
                    }
                }
            }
            return listHistoryOld;
        }
        public void ReturnFalse(int Id)
        {
            ViewData["Department"] = _iplDepartment.ListAllByTreeView();
            ViewData["DispatchWork"] = _iplDispatchWork.ListAllByTreeView();
            ViewData["Position"] = _iplPosition.ListAll();
            var listDepart = _iplEmployee.ListDetail(Id);
            var list = new List<int>();
            list.ToArray();
            var list3 = new List<int>();
            list3.ToArray();
            foreach (var item in listDepart)
            {
                list.Add(item.IdDepartment);
                list3.Add(item.IdPosition);
            }
            ViewData["arrDepartment"] = list;
            ViewData["arrPosition"] = listDepart;

            var listWork = _iplDispatchWork.ListDetail(Id);
            var list2 = new List<int>();
            list2.ToArray();

            foreach (var item in listWork)
            {
                list2.Add(item.IdDispatchWork);
            }
            ViewData["arrDispatchWork"] = list2;
        }
        public ActionResult AddRole(int employeeId)
        {
            return RedirectToAction("Index", "Roles", new { id = employeeId });
        }
        [AuthorizeUser(ModuleName = "Employee", AccessLevel = Constants.Delete)]
        public ActionResult Delete(int id)
        {
            var sess = SessionSystem.GetUser();
            Logs.logs("Xóa nhân sự", "Truy cập vào trang Employee", "/Employee/", sess.UserId);
            var detail = _iplEmployee.ViewDetail(id);
            if (detail != null && detail.Id > 0)
            {
                var ret = _iplEmployee.Delete(id);
            }
            int totalRow = 0;

            var employee = _iplEmployee.ListAllPaging("", 1, 10, ref totalRow);
            return View("Index", employee.ToPagedList(1, 10, totalRow));
        }
        public JsonResult EditActive(int userId, bool access)
        {
            var ret = _iplUser.UpdateActive(userId, access);
            return Json(new { status = ret });
        }
    }
}