using Framework.EF;
using Dispatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagement.Controllers
{
    public class Project_RolesController : Controller
    {
        private IProject_Role _iplProject_Roles;
        private IPosition _iplPosition;
        private IEmployee _iplEmployee;
        // GET: Project_Roles
        public Project_RolesController()
        {
            _iplPosition = SingletonIpl.GetInstance<IplPosition>();
            _iplEmployee = SingletonIpl.GetInstance<IplEmployee>();
            _iplProject_Roles = SingletonIpl.GetInstance<IplProject_Role>();
        }

        //[AuthorizeUser(ModuleName = "Roles", AccessLevel = Constants.View)]
        public ActionResult Index(int id)
        {
            var iplProject = SingletonIpl.GetInstance<IplProject>();
            var ProjectInfo = iplProject.ViewDetailWithCustomer(id);
            ViewData["ListRole"] = _iplProject_Roles.ListAllByProject(id);
            return View(ProjectInfo);
        }
        [HttpPost]
        //[AuthorizeUser(ModuleName = "Roles", AccessLevel = Constants.Edit)]
        public JsonResult EditRoles(int userId, int ProjectId, string action, bool access)
        {
            var iplProject_Role = SingletonIpl.GetInstance<IplProject_Role>();
            var ret = iplProject_Role.Update(userId, ProjectId, action, access);
            return Json(new { status = ret });
        }

        public ActionResult InsertUser()
        {
            LoadData();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        //[AuthorizeUser(ModuleName = "Roles", AccessLevel = Constants.Add)]
        public ActionResult InsertUser(Project_RoleEntity entity,int id)
        {
            LoadData();
            if(ModelState.IsValid)
            {
                
                    int project_RolesId = 0;
                    Project_RoleEntity exitEntity = new Project_RoleEntity();

                    exitEntity = entity;
                    exitEntity.Position = entity.SelectPosition;
                    exitEntity.UserId = entity.SelectFullName;
                    exitEntity.ProjectId = id;
                    project_RolesId = _iplProject_Roles.Insert(exitEntity);
                if (project_RolesId < 1)
                {
                    ModelState.AddModelError("SelectFullName", "Xảy ra lỗi, vui lòng liên hệ với coder");
                    return View(entity);
                }
            }
            return RedirectToAction("Index",new { id = id });
        }
        
        public ActionResult Delete(int UserId,int ProjectId)
        {
            _iplProject_Roles.Delete(UserId, ProjectId);
            return RedirectToAction("Index", new { id = ProjectId });
        }

        public void LoadData()
        {
            int totalrow = 0;
           

            var Employee = _iplEmployee.ListAllPaging(1, 1000, ref totalrow);
            ViewData["AllEmployee"] = Employee;

            var position = _iplPosition.ListAllPaging(1, 100, ref totalrow);
            ViewData["Vitri"] = position;
        }

    }
}