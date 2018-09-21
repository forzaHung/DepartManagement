using Framework.Configuration;
using Framework.EF;
using Dispatch;
using MvcPaging;
using Prototype.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExtensionMethods;
using Newtonsoft.Json;
using DispatchManagement.Controllers;
using DispatchManagement;

namespace ProjectManagement.Controllers
{
    public class ProjectController : BaseController
    {
        private IProject _iplProject;
        private IProject_Status _iplProject_Status;
        private IProject_Role _iplProject_Role;
        private IRole _iplRole;
        private IModule _iplModule;
        private ICustomer_Type _iplCustomer_Type;
        private ICustomer _iplCustomer;
        private IPosition _iplPosition;
        private IEmployee _iplEmployee;
        private IProject_Comment _iplProjectComment;

        // GET: Project
        public ProjectController()
        {
            _iplProject = SingletonIpl.GetInstance<IplProject>();
            _iplProject_Status = SingletonIpl.GetInstance<IplProject_Status>();
            _iplRole = SingletonIpl.GetInstance<IplRole>();
            _iplModule = SingletonIpl.GetInstance<IplModule>();
            _iplCustomer_Type = SingletonIpl.GetInstance<IplCustomer_Type>();
            _iplCustomer = SingletonIpl.GetInstance<IplCustomer>();
            _iplPosition = SingletonIpl.GetInstance<IplPosition>();
            _iplEmployee = SingletonIpl.GetInstance<IplEmployee>();
            _iplProject_Role = SingletonIpl.GetInstance<IplProject_Role>();
            _iplProjectComment = SingletonIpl.GetInstance<IplProject_Comment>();
        }
        //[AuthorizeUser(ModuleName = "Project", AccessLevel = Constants.View)]
        public ActionResult Index(string searchString = "", int page = 1, int pageSize = 10)
        {
            int totalRow = 0;
            //Roles();
            var project = _iplProject.ListAllPaging(searchString, page, pageSize, ref totalRow);
            return View(project.ToPagedList(page, pageSize, totalRow));
        }
        //[AuthorizeUser(ModuleName = "Project", AccessLevel = Constants.Add)]
        public ActionResult CreateOrEdit(int? id)
        {
            var sessUser = SessionSystem.GetUser();
            LoadData();
            ProjectEntity entity = new ProjectEntity();
            if (id.HasValue)
            {
                var role = _iplProject_Role.ViewDetail(sessUser.UserId, id.Value);
                if (role != null)
                {
                    if (role.Edit)
                    {
                        ViewBag.Title = "Sửa thông tin dự án";
                        //Edit
                        entity = _iplProject.ViewDetail(id.Value);
                        if (entity == null)
                            return HttpNotFound("Không tìm thấy tin");
                        return View(entity);
                    }
                    else
                    {
                        return RedirectToAction("Denied", "NotFound");
                    }
                }
                else
                {
                    return RedirectToAction("Denied", "NotFound");
                }
            }


            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateOrEdit(ProjectEntity entity, int? id)
        {
            var sessUser = SessionSystem.GetUser();
            LoadData();
            if (ModelState.IsValid)
            {
                entity.IsDel = false;

                foreach (var item in entity.SelectedIDListManager)
                {
                    entity.ListManager += item.ToString() + ";";
                }

                //foreach (var item in entity.SelectedIDListTechnical)
                //{
                //    entity.ListTechnical += item.ToString() + ";";
                //}
                //foreach (var item in entity.SelectedIDListAssistant)
                //{
                //    entity.ListAssistant += item.ToString() + ";";
                //}
                //foreach (var item in entity.SelectedIDListBuyer)
                //{
                //    entity.ListBuyer += item.ToString() + ";";
                //}

                int projectId = 0;

                if (id.HasValue && id.Value > 0) //update
                {
                    var role = _iplProject_Role.ViewDetail(sessUser.UserId, id.Value);
                    if (role != null)
                    {
                        if (role.Edit)
                        {
                            var entityExist = _iplProject.ViewDetail(id.Value);
                            if (entityExist == null)
                            {
                                return HttpNotFound("Không có tin tức");
                            }
                            entityExist.Name = entity.Name;
                            entityExist.Price = entity.Price;
                            entityExist.CustomerId = entity.CustomerId;
                            entityExist.SalesId = entity.SalesId;
                            entityExist.ModifiedDate = DateTime.Now;
                            entityExist.ListAssistant = entity.ListAssistant;
                            entityExist.ListManager = entity.ListManager;
                            entityExist.ListTechnical = entity.ListTechnical;
                            entityExist.ListBuyer = entity.ListBuyer;
                            entityExist.TypeId = entity.TypeId;
                            entityExist.StatusId = entity.StatusId;
                            entityExist.Description = entity.Description;
                            projectId = _iplProject.Update(entityExist);
                            ActionProject_Roles(projectId, entity, "edit");
                        }
                        else
                        {
                            return RedirectToAction("Denied", "NotFound");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Denied", "NotFound");
                    }

                }
                else //insert
                {

                    entity.CreateDate = DateTime.Now;
                    projectId = _iplProject.Insert(entity);

                    //ActionProject_Roles(projectId, entity,"add");
                }
                if (projectId < 1)
                {
                    ModelState.AddModelError("Name", "Xảy ra lỗi, vui lòng liên hệ với coder");
                    return View(entity);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(entity);
            }

        }
        public ActionResult _ProjectComment(long Id)
        {
            var obj = _iplProjectComment.ListViewByProjectTop1(Id);
            return View(obj);
        }
        //public ActionResult Delete(int id)
        //{
        //    var trave = _iplProject.Delete(id);
        //    if (!trave)
        //        ViewBag.msg = "Occur Error. Contact to Administrator";
        //    return RedirectToAction("Index");
        //}
        public ActionResult Delete(int id)
        {
            var sessUser = SessionSystem.GetUser();
            var role = _iplProject_Role.ViewDetail(sessUser.UserId, id);
            if (role != null)
            {
                if (role.Delete)
                {
                    var detail = _iplProject.ViewDetail(id);
                    if (detail != null && detail.Id > 0)
                    {
                        var ret = _iplProject.Delete(id);
                    }
                    int totalRow = 0;

                    var project = _iplProject.ListAllPaging("", 1, 10, ref totalRow);
                    return View("Index", project.ToPagedList(1, 10, totalRow));
                }
                else
                {
                    return RedirectToAction("Denied", "NotFound");
                }
            }
            else
            {
                return RedirectToAction("Denied", "NotFound");
            }
        }
        public PartialViewResult GetProject(string searchString = "", int page = 1, int pageSize = 10)
        {
            int totalRow = 0;

            var lst = this._iplProject.ListAllPaging(searchString, page, pageSize, ref totalRow).ToPagedList(page, pageSize, totalRow);

            return PartialView("_ProjectList", lst);
        }
        #region các hàm cần để gọi
        public void ActionProject_Roles(int projectId, ProjectEntity entity, string action)
        {
            Project_RoleEntity project_role = new Project_RoleEntity();
            if (entity.SelectedIDListManager != null)
            {
                foreach (var item in entity.SelectedIDListManager)
                {
                    project_role.UserId = item;
                    project_role.ProjectId = projectId;
                    project_role.Add = true;
                    project_role.Edit = true;
                    project_role.Delete = true;
                    project_role.View = true;
                    project_role.Print = true;
                    if (action == "add")
                    {
                        _iplProject_Role.Insert(project_role);
                    }
                    if (action == "edit")
                    {
                        var check = _iplProject_Role.ListAllByUserProject(item, projectId);
                        if (check != null && check.Count > 0)
                        {
                            _iplProject_Role.Update(project_role);
                        }
                        else
                        {
                            //chưa xong phần check xoá nếu có Id 
                            _iplProject_Role.Insert(project_role);
                        }
                    }
                }
            }
        }
        public void Roles()
        {
            var sessUser = SessionSystem.GetUser();
            var module = _iplModule.ViewDetailByName("ProjectPrice");


            var role = _iplRole.ViewDetail(sessUser.UserId, module.ID);
            ViewData["AuthorizePriceView"] = role.View;

        }
        //phân quyền theo từng project
        public void RolesProject(int projectId)
        {

            var sessUser = SessionSystem.GetUser();
            var role = _iplProject_Role.ViewDetail(sessUser.UserId, projectId);
            ViewData["AuthorizeProjectPriceView"] = role.View;

        }
        public void LoadData()
        {
            int totalrow = 0;
            var AllProject_Status = _iplProject_Status.ListAllPaging(1, 1000, ref totalrow);
            ViewData["AllProject_Status"] = AllProject_Status;

            var treeview = _iplCustomer_Type.BindTreeview();
            ViewData["AllCustomer_Type"] = treeview;

            var Employee = _iplEmployee.ListAllPaging(1, 1000, ref totalrow);
            ViewData["AllEmployee"] = Employee;

            var LanhDao = _iplEmployee.ListAllByPostion(1, 0);
            ViewData["AllLanhDao"] = LanhDao;

            var khachhang = _iplCustomer.ListAll();
            ViewData["AllCustomer"] = khachhang;

            //var KyThuat = _iplEmployee.ListAllByPostion(2);
            //ViewData["AllKyThuat"] = KyThuat;

            //var TroLy = _iplEmployee.ListAllByPostion(3);
            //ViewData["AllTroLy"] = TroLy;

            //var MuaHang = _iplEmployee.ListAllByPostion(4);
            //ViewData["AllMuaHang"] = MuaHang;
        }
        [HttpGet]
        public JsonResult GetCustomer(int Customer_TypeId)
        {
            string listType = Customer_TypeId + ";";
            var listvalue = _iplCustomer.ListByType(listType);
            List<CustomerEntity> customer = new List<CustomerEntity>();
            foreach (var item in listvalue)
            {
                customer.Add(new CustomerEntity
                {
                    Id = Convert.ToInt32(item.Id.ToString()),
                    Name = item.Name.ToString()

                });
            }

            string jsonString = customer.ToJSON();




            if (customer != null)
            {
                return Json(new
                {
                    status = true,
                    data = jsonString
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion
    }
}