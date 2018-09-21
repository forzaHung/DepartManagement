using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Framework.EF;
using MvcPaging;
using ProjectManagement.Controllers;
using Newtonsoft.Json;
using Dispatch;

namespace DispatchManagement.Controllers
{
    public class ProjectStatusController : BaseController
    {
        private IplProject_Status _iplProjectStatus;
        public ProjectStatusController()
        {
            _iplProjectStatus = SingletonIpl.GetInstance<IplProject_Status>();
        }
        // GET: Project_Status
        public ActionResult Index(int pageIndex = 1)
        {
            int pageSize = 10;
            int totalRow = 0;
            var projectStatus = _iplProjectStatus.ListAllPaging(pageIndex, pageSize, ref totalRow);
            return View(projectStatus.ToPagedList(pageIndex, pageSize, totalRow));
        }
        public ActionResult CreateOrEdit(int? id)
        {
            Project_StatusEntity entity = new Project_StatusEntity();
            if (id.HasValue)
            {
                ViewBag.Title = "Sửa trạng thái dự án";
                ViewBag.SubTitle = "Sửa thông tin trạng thái dự án";
                entity = _iplProjectStatus.ViewDetail(id.Value);
                if (entity == null)
                    return HttpNotFound("Không có thông tin trạng thái dự án");
                return View(entity);
            }
            ViewBag.Title = "Thêm trạng thái dự án";
            ViewBag.SubTitle = "Thêm thông tin trạng thái dự án";
            return View(entity);
        }
        [HttpPost]
        public ActionResult CreateOrEdit(Project_StatusEntity entity, int? id)
        {
            if (!ModelState.IsValid)
                return View(entity);
            int projecStatusId = 0;
            if (id.HasValue && id.Value > 0)
            {
                var entityTemp = _iplProjectStatus.ViewDetail(id.Value);
                if (entityTemp == null)
                {
                    ViewBag.MgsNotFound = "Không tìm thấy trạng thái dự án";
                    return View(entity);
                }
                projecStatusId = id.Value;
                entityTemp.Name = entity.Name;
                entityTemp.IsActive = entity.IsActive;
                _iplProjectStatus.Update(entityTemp);
            }
            else
            {
                projecStatusId = _iplProjectStatus.Insert(entity);

                return RedirectToAction("Index");
            }
            if (projecStatusId == -1)
            {
                ModelState.AddModelError("Name", "Trùng tên. Có thể danh mục này đã bị xóa. Bạn có thể kiểm tra trong thùng rác");
                return View(entity);
            }
            return RedirectToAction("Index");
        }     
        //Delete Status
        public ActionResult Delete(int id)
        {
            try
            {
                _iplProjectStatus.Delete(id);
            }
            catch
            {
                return HttpNotFound("Không tìm thấy trạng thái");
            }
            return RedirectToAction("Index");
        }
        //Edit Active
        [HttpPost]
        public JsonResult Active(int moduleId, string action, bool access)
        {
            var detail = _iplProjectStatus.ViewDetail(moduleId);
            switch (action)
            {
                case "Active":
                    if (access)
                    {
                        detail.IsActive = true;
                    }
                    else
                    {
                        detail.IsActive = false;
                    }
                    break;      
            }

            var ret = _iplProjectStatus.Update(detail);
            return Json(new { status = ret });
        }
        //Get GetProjectStatus
        public PartialViewResult GetProjectStatus(string searchString = "", int page = 1, int pageSize = 10)
        {
            int totalRow = 0;
            IEnumerable<Project_StatusEntity> lstStatus;
            if (!string.IsNullOrEmpty(searchString))
            {
                dynamic deserializeResult = JsonConvert.DeserializeObject(searchString);
                string searchName = deserializeResult.searchName;
                lstStatus = this._iplProjectStatus.ListAllPaging(searchName.Trim(), page, pageSize, ref totalRow).ToPagedList(page, pageSize, totalRow);
            }
            else
            {
                lstStatus = _iplProjectStatus.ListAllPaging(page, pageSize, ref totalRow).ToPagedList(page, pageSize, totalRow);

            }
            return PartialView("_ProjectStatusList",lstStatus);
        }
    }
}