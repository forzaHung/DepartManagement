using Framework.EF;
using MvcPaging;
using Dispatch;
using ProjectManagement;
using ProjectManagement.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DispatchManagement.Controllers
{
    public class ProjectCommentController : BaseController
    {
        private IProject _iplProject;
        private IProject_Comment _iplProjectComment;
        private IProject_Status _iplProject_Status;
        private ICustomer_Type _iplCustomer_Type;
        private ICustomer _iplCustomer;
        private IEmployee _iplEmployee;
        private IProject_Role _iplProjectRole;
        public ProjectCommentController()
        {
            _iplProject = SingletonIpl.GetInstance<IplProject>();
            _iplProjectComment = SingletonIpl.GetInstance<IplProject_Comment>();
            _iplProject_Status = SingletonIpl.GetInstance<IplProject_Status>();
            _iplCustomer_Type = SingletonIpl.GetInstance<IplCustomer_Type>();
            _iplCustomer = SingletonIpl.GetInstance<IplCustomer>();
            _iplEmployee = SingletonIpl.GetInstance<IplEmployee>();
            _iplProjectRole = SingletonIpl.GetInstance<IplProject_Role>();
        }
        // GET: ProjectComment
        /// <summary>
        /// Lấy các bình luận về project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(long? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                var sessUser = SessionSystem.GetUser();
                var role = _iplProjectRole.ViewDetail(sessUser.UserId, id.Value);
                if (role != null)
                {
                    if (role.View == true)
                    {
                        RolesProject(id.Value);
                        ViewBag.id = id.Value;
                        //Lấy thông tin người đăng nhập
                        var objProject = _iplProject.ViewDetail(id.Value);
                        try
                        {
                            ViewBag.ProjectName = objProject.Name;
                            ViewBag.Description = objProject.Description;
                            ViewBag.ProjectPrice = objProject.Price;

                            var statusproject = _iplProject_Status.ViewDetail(objProject.StatusId);
                            ViewBag.Status = statusproject.Name;

                            var sales = _iplEmployee.ViewDetail(objProject.SalesId);
                            ViewBag.sales = sales.FirstName + " " + sales.LastName;
                            var roles = _iplProjectRole.ListAllByProject(objProject.Id);


                            foreach (var item in roles)
                            {
                                ViewBag.Lanhdao = item.FullName + " ;";
                                //string positionname = item.PositionName;
                                //switch (positionname)
                                //{
                                //    case "Lãnh đạo":
                                //        ViewBag.Lanhdao = item.FullName + " ;";
                                //        break;
                                //    case "Kỹ thuật":
                                //        ViewBag.Technical = item.FullName + " ;";
                                //        break;
                                //    case "Trợ lý":
                                //        ViewBag.Troly = item.FullName + " ;";
                                //        break;
                                //    case "Mua hàng":
                                //        ViewBag.Muahang = item.FullName + " ;";
                                //        break;
                                //    default:
                                //        break;
                                //}
                            }
                            //ViewBag.Troly = "";


                            //ViewBag.Technical = "";
                            var customer = _iplCustomer.ViewDetail(objProject.CustomerId);
                            ViewBag.Customer = customer.Name;


                        }
                        catch { }
                        if (objProject != null)
                        {
                            //Lấy toàn bộ danh sách các bình luận theo dự án
                            List<Project_CommentEntity> glst = _iplProjectComment.GetByFKProject(objProject.Id);
                            return View(glst);
                        }
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
            else
                ViewData["ID"] = 0;
            return View();
        }
        [HttpPost]
        public JsonResult CreateComment(int id, string comment, int projectID)
        {
            if (id == 0)
            {
                var sessUser = SessionSystem.GetUser();
                long projectCmt = 0;
                Project_CommentEntity entity = new Project_CommentEntity();
                entity.Description = comment.ToString();
                entity.CreateDate = DateTime.Now;
                entity.UserId = sessUser.UserId;
                entity.ProjectId = projectID;
                entity.FullName = sessUser.FirstName + " " + sessUser.LastName;
                projectCmt = _iplProjectComment.Insert(entity);
                if (projectCmt == -1)
                    return Json(new { status = false });
                else
                    return Json(new { status = true, data = 0 });
            }
            else
            {
                var sessUser = SessionSystem.GetUser();
                long projectCmt = 0;
                Project_CommentEntity entity = new Project_CommentEntity();
                entity.Description = comment.ToString();
                entity.CreateDate = DateTime.Now;
                entity.UserId = sessUser.UserId;
                entity.ProjectId = projectID;
                entity.ParentId = id;
                entity.FullName = sessUser.FirstName + " " + sessUser.LastName;
                projectCmt = _iplProjectComment.Insert(entity);
                if (projectCmt == -1)
                    return Json(new { status = false });
                else
                    return Json(new { status = true, data = id });
            }
        }
        [HttpPost]
        public JsonResult loadComment(int id)
        {
            var glst = _iplProjectComment.GetBySK(id);
            if (glst.Count > 0)
                return Json(new { status = true, data = glst, count = glst.Count }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = false });
        }

        #region Các hàm cần dùng
        public void RolesProject(long projectId)
        {
            var sessUser = SessionSystem.GetUser();
            var role = _iplProjectRole.ViewDetail(sessUser.UserId, projectId);
            ViewData["AuthorizeProjectPriceView"] = role.ViewPrice;

        }
        #endregion

    }
}