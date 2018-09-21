using Framework.Configuration;
using Framework.EF;
using Dispatch;
using MvcPaging;
using DispatchManagement.Models;
using Dispatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DispatchManagement.Controllers
{
    public class DepartmentController : BaseController
    {
        private IDepartment _iplDepartment;
        private IEmployee _iplEmployee;
        private IEmployee_History _iplEmployee_History;

        public DepartmentController()
        {
            _iplDepartment = SingletonIpl.GetInstance<IplDepartment>();
            _iplEmployee = SingletonIpl.GetInstance<IplEmployee>();
            _iplEmployee_History = SingletonIpl.GetInstance<IplEmployee_History>();

        }
        // GET: Department
        public ActionResult Index()
        {
            //var department = _iplDepartment.ListAllPaging(searchString, page, pageSize, ref totalRow);
            var department = _iplDepartment.ListAllByTreeView();
            return View(department);
        }

        public PartialViewResult GetDepartment(string searchString = "", int page = 1, int pageSize = 10)
        {
            int totalRow = 0;

            var department = this._iplDepartment.ListAllPaging(searchString, page, pageSize, ref totalRow).ToPagedList(page, pageSize, totalRow);

            return PartialView("_DepartmentList", department);
        }

        //[AuthorizeUser(ModuleName = "Department", AccessLevel = Constants.Add)]
        public ActionResult CreateDepartment()
        {
            var entity = new DepartmentEntity();
            ReturnViewData(0);
            return View(entity);
        }

        //[AuthorizeUser(ModuleName = "Employee", AccessLevel = Constants.Edit)]
        public ActionResult EditDepartment(int? id)
        {
            List<DepartmentEntity> depart = _iplDepartment.ListAllByTreeView();
            foreach (var item in depart)
            {
                if (item.Id == id)
                {
                    List<DepartmentEntity> b = DepartmentChild(item.Id, depart);
                    foreach (var item2 in b)
                        depart = depart.Where(x => x.Id != item2.Id).ToList();
                    depart = depart.Where(x => x.Id != item.Id).ToList();
                }
            }
            ReturnViewData(id);
            DepartmentEntity entity = new DepartmentEntity();
            if (id != null)
            {
                entity = _iplDepartment.ViewDetail((int)id);
            }
            return View("CreateDepartment", entity);
        }

        public void ReturnViewData(int? id)
        {
            ViewData["ParentID"] = _iplDepartment.ListAllByTreeView();
            var iplEmp = SingletonIpl.GetInstance<IplEmployee>();
            var allEmpManager = iplEmp.ListAllByPostion(3, (int)id);//id=3 is Manager
            ViewData["EmployeeManager"] = allEmpManager;
            var allEmpDeputyManager = iplEmp.ListAllByPostion(4, (int)id);//id=4 is Deputy Manager
            ViewData["EmployeeDeputyManager"] = allEmpDeputyManager;
            var allChiefOfTheOffice = iplEmp.ListAllByPostion(5, (int)id);//id=5 is ChiefOfTheOffice (sửa lại sau vì list danh sách chưa có Chánh văn phòng)
            ViewData["EmployeeChiefOfTheOffice"] = allChiefOfTheOffice;
        }
        public ActionResult Save(DepartmentEntity model)
        {
            var entity = new DepartmentEntity();
            var employeetity = new EmployeeEntity();

            ReturnViewData(0);
            if (ModelState.IsValid)
            {
                if (model.Id > 0) //update
                {
                    var sess = SessionSystem.GetUser();
                    Logs.logs("Sửa Department", "Truy cập vào trang CreateDepartment", "/Department/CreateDepartment", sess.UserId);
                    entity = _iplDepartment.ViewDetail(model.Id);
                    if (entity != null && entity.Id > 0)
                    {
                        //get thông tin của Phòng
                        entity.ParentId = model.ParentId;
                        entity.Name = model.Name;
                        entity.Location = model.Location;
                        //entity.CreatedDate = model.CreatedDate;
                        entity.ModifiedDate = DateTime.Now;
                        entity.EmployeeManagerId = model.EmployeeManagerId;
                        entity.EmployeeDebutyManagerId = model.EmployeeDebutyManagerId;
                        entity.EmployeeChiefOfTheOfficeId = model.EmployeeChiefOfTheOfficeId;
                        entity.SumEmployeesExpected = model.SumEmployeesExpected;
                        var retVal = _iplDepartment.Update(entity);

                        if (retVal)
                        {
                            int departid = entity.Id;
                            if (departid > 0)
                            {

                                employeetity = _iplEmployee.ViewDetail(model.EmployeeManagerId);//lấy thông tin user trưởng phòng
                                if (employeetity != null && employeetity.Id > 0)
                                {
                                    if (employeetity.DepartmentId != departid) //check xem nếu nằm trong phòng hiện tại thì bỏ qua.
                                    {
                                        employeetity.LastDepartment = employeetity.DepartmentId;
                                        //trường hợp đổi phòng thì update lại phòng cũ chưa có vị trí mà employee này từng làm
                                        entity = _iplDepartment.ViewDetail(employeetity.LastDepartment);
                                        entity.EmployeeManagerId = 0;
                                        var updatedepartment = _iplDepartment.Update(entity);

                                        SaveHistory(employeetity, departid, _iplEmployee_History);
                                    }
                                    employeetity.DepartmentId = departid; //update cho employee thông tin về bộ phận làm việc.
                                    employeetity.ModifiedDate = DateTime.Now;
                                    var revalmanager = _iplEmployee.Update(employeetity);
                                }

                                employeetity = _iplEmployee.ViewDetail(model.EmployeeDebutyManagerId);// thông tin phó phòng
                                if (employeetity != null && employeetity.Id > 0)
                                {
                                    if (employeetity.DepartmentId != departid)
                                    {
                                        employeetity.LastDepartment = employeetity.DepartmentId;
                                        //trường hợp đổi phòng thì update lại phòng cũ chưa có vị trí mà employee này từng làm
                                        entity = _iplDepartment.ViewDetail(employeetity.LastDepartment);
                                        entity.EmployeeDebutyManagerId = 0;
                                        var updatedepartment = _iplDepartment.Update(entity);

                                        SaveHistory(employeetity, departid, _iplEmployee_History);
                                    }
                                    employeetity.DepartmentId = departid; //update cho employee thông tin về bộ phận làm việc.
                                    employeetity.ModifiedDate = DateTime.Now;

                                    var revalmanager = _iplEmployee.Update(employeetity);
                                }

                                employeetity = _iplEmployee.ViewDetail(model.EmployeeChiefOfTheOfficeId);// thông tin Chánh văn phòng
                                if (employeetity != null && employeetity.Id > 0)
                                {
                                    if (employeetity.DepartmentId != departid)
                                    {
                                        employeetity.LastDepartment = employeetity.DepartmentId;
                                        //trường hợp đổi phòng thì update lại phòng cũ chưa có vị trí mà employee này từng làm
                                        entity = _iplDepartment.ViewDetail(employeetity.LastDepartment);
                                        entity.EmployeeChiefOfTheOfficeId = 0;
                                        var updatedepartment = _iplDepartment.Update(entity);

                                        SaveHistory(employeetity, departid, _iplEmployee_History);
                                    }
                                    employeetity.DepartmentId = departid; //update cho employee thông tin về bộ phận làm việc.
                                    employeetity.ModifiedDate = DateTime.Now;
                                    var revalmanager = _iplEmployee.Update(employeetity);
                                }
                            }
                            return RedirectToAction("Index", "Department");
                        }
                    }
                }
                else //insert
                {
                    var sess = SessionSystem.GetUser();
                    Logs.logs("Thêm mới Department", "Truy cập vào trang CreateDepartment", "/Department/CreateDepartment", sess.UserId);
                    model.CreatedDate = DateTime.Now;
                    var departid = _iplDepartment.Insert(model);
                    if (departid > 0)
                    {
                        employeetity = _iplEmployee.ViewDetail(model.EmployeeManagerId);//lấy thông tin user trưởng phòng
                        if (employeetity != null && employeetity.Id > 0)
                        {
                            if (employeetity.DepartmentId != departid)
                            {
                                employeetity.LastDepartment = employeetity.DepartmentId;
                            }
                            employeetity.DepartmentId = departid; //update cho employee thông tin về bộ phận làm việc.
                            employeetity.ModifiedDate = DateTime.Now;
                            var revalmanager = _iplEmployee.Update(employeetity);
                        }

                        employeetity = _iplEmployee.ViewDetail(model.EmployeeDebutyManagerId);// thông tin phó phòng
                        if (employeetity != null && employeetity.Id > 0)
                        {
                            if (employeetity.DepartmentId != departid)
                            {
                                employeetity.LastDepartment = employeetity.DepartmentId;
                            }
                            employeetity.DepartmentId = departid; //update cho employee thông tin về bộ phận làm việc.
                            employeetity.ModifiedDate = DateTime.Now;

                            var revalmanager = _iplEmployee.Update(employeetity);
                        }

                        employeetity = _iplEmployee.ViewDetail(model.EmployeeChiefOfTheOfficeId);// thông tin Chánh văn phòng
                        if (employeetity != null && employeetity.Id > 0)
                        {
                            if (employeetity.DepartmentId != departid)
                            {
                                employeetity.LastDepartment = employeetity.DepartmentId;
                            }
                            employeetity.DepartmentId = departid; //update cho employee thông tin về bộ phận làm việc.
                            employeetity.ModifiedDate = DateTime.Now;
                            var revalmanager = _iplEmployee.Update(employeetity);
                        }

                        return RedirectToAction("Index", "Department", new { id = departid });
                    }

                }
            }
            ViewBag.Msg = ConstantMsg.ErrorProgress;
            return View("CreateDepartment", model);
        }

        public static void SaveHistory(EmployeeEntity employeetity, int departid, IEmployee_History _iplEmployee_History)
        {
            var EmpHis = new Employee_HistoryEntity(); //lưu lại lịch sử đổi phòng

            //trường hợp department ko thay đổi htif ko cập nhập
            if (employeetity.DepartmentId != departid)
            {
                EmpHis.Position = employeetity.Position;
                EmpHis.DepartmentId = departid;
                EmpHis.EmployeeId = employeetity.Id;
                EmpHis.TimeIn = DateTime.Now;
                //EmpHis.TimeOut = DateTime.Now;
                EmpHis.Note = "Chuyển sang từ " + employeetity.DepartmentName;
                var emphId = _iplEmployee_History.Insert(EmpHis);

                IplDepartment depart = new IplDepartment();
                var tmpdepart = depart.ViewDetail(departid);

                string departmentTo = tmpdepart.Name;



                EmpHis = _iplEmployee_History.SelectByPositionDepartment(employeetity.Id, employeetity.Position, employeetity.DepartmentId); //sửa lịch sử cũ
                if (EmpHis != null)
                {
                    EmpHis.TimeOut = DateTime.Now;
                    EmpHis.Note = "Chuyển sang " + departmentTo;
                    var updateEmpHis = _iplEmployee_History.Update(EmpHis);

                }
            }
        }
        public ActionResult Delete(int id)
        {
            var sess = SessionSystem.GetUser();
            Logs.logs("Xóa Department", "Truy cập vào trang Department", "/Department/", sess.UserId);
            List<DepartmentEntity> depart = _iplDepartment.ListAllByTreeView();

            //if(depart.Count(x =  .))

            foreach (var item in depart)
            {
                if (item.Id == id)
                {
                    List<DepartmentEntity> b = DepartmentChild(item.Id, depart);
                    b.Add(item);
                    foreach (var item2 in b)
                    {
                        var detail = _iplDepartment.ViewDetail(item2.Id);
                        if (detail != null && detail.Id > 0)
                        {
                            var ret = _iplDepartment.Delete(item2.Id);
                        }
                    }
                }
            }
            return RedirectToAction("Index");
            //int totalRow = 0;
            //var department = _iplDepartment.ListAllPaging("", 1, 10, ref totalRow);
            //return View("Index", department.ToPagedList(1, 10, totalRow));
        }
        public List<DepartmentEntity> DepartmentChild(int id, List<DepartmentEntity> depart)
        {
            List<DepartmentEntity> con = new List<DepartmentEntity>();
            foreach (var item in depart)
            {
                if (item.ParentId == id)
                {
                    con.Add(item);
                    depart = depart.Where(x => x.Id != item.Id).ToList();
                    List<DepartmentEntity> b = DepartmentChild(item.Id, depart);
                    foreach (var item2 in b)
                        con.Add(item2);
                }
            }
            return con;
        }
    }
}