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
using System.IO;

namespace DispatchManagement.Controllers
{
    public class DispatchOutController : BaseController
    {
        private IDispatchOut _ipldispatchout;
        private IDepartment _ipldepartment;
        private IDispatchType _ipldispatchtype;
        private IDispatchStatus _ipldispatchstatus;
        private IDispatchPriority _ipldispatchpriority;
        private IEmployee _iplemployee;
        private IFile _iplfile;
        private string _FilePath = Config.GetConfigByKey("FileDispatchOut");
        // GET: DispatchOut
        public DispatchOutController()
        {
            _ipldispatchout = SingletonIpl.GetInstance<IplDispatchOut>();
            _ipldepartment = SingletonIpl.GetInstance<IplDepartment>();
            _ipldispatchtype = SingletonIpl.GetInstance<IplDispatchType>();
            _ipldispatchstatus = SingletonIpl.GetInstance<IplDispatchStatus>();
            _ipldispatchpriority = SingletonIpl.GetInstance<IplDispatchPriority>();
            _iplemployee = SingletonIpl.GetInstance<IplEmployee>();
            _iplfile = SingletonIpl.GetInstance<IplFile>();
        }

        public ActionResult Index(string Code = "", string Name = "", int DepartmentId = 0,
            int Type = 0, int Department = 0, int Status = 0, int Priority = 0, string DateWrite = "", int page = 1, int pageSize = 10)
        {
            #region lưu lại thông tin tìm

            ViewBag.DepartmentId = DepartmentId;
            ViewBag.Type = Type;
            ViewBag.Status = Status;
            ViewBag.Priority = Priority;
            #endregion
            ViewBag.FilePatch = _FilePath;
            int totalRow = 0;
            if (DepartmentId > 0)
            {
                var department = _ipldepartment.ViewDetail(DepartmentId);
                ViewBag.departmentname = department.Name;
            }
            #region load dropdownlist các lựa chọn tìm kiếm
            var dispatchtypeAll = _ipldispatchtype.ListAll();
            ViewData["dispatchtypeAll"] = dispatchtypeAll;
            var departmentAll = _ipldepartment.ListAll();
            ViewData["departmentAll"] = departmentAll;

            var dispatchstatusAll = _ipldispatchstatus.ListAll();
            ViewData["dispatchstatusAll"] = dispatchstatusAll;
            #endregion

            var dispatchOut = _ipldispatchout.ListAllPaging(Code, Name, Type, Department, Status, Priority,
                DateWrite, DepartmentId, page, pageSize, ref totalRow);
            return View(dispatchOut.ToPagedList(page, pageSize, totalRow));
        }
        public PartialViewResult GetDispatchOut(string Code = "", string Name = "", int DepartmentId = 0,
            int Type = 0, int Department = 0, int Status = 0, int Priority = 0, string DateWrite = "", int page = 1, int pageSize = 10)
        {
            int totalRow = 0;

            var dispatchOut = _ipldispatchout.ListAllPaging(Code, Name, Type, Department, Status, Priority,
               DateWrite, DepartmentId, page, pageSize, ref totalRow).ToPagedList(page, pageSize, totalRow);

            return PartialView("_DispatchOutList", dispatchOut);
        }

        public ActionResult CreateDispatchOut(DispatchOutEntity entity)
        {

            ListDropdown();
            return View(entity);
        }
        public ActionResult EditDispatchOut(int? id)
        {

            ListDropdown();

            var File = _iplfile.ViewDetailByDispatch(long.Parse(id.ToString()), true);
            if (File != null)
            {
                ViewBag.FileName = File.Name;
                ViewBag.FileId = File.Id;
            }
            DispatchOutEntity entity = new DispatchOutEntity();
            if (id != null)
            {
                entity = _ipldispatchout.ViewDetail((int)id);

            }
            return View("CreateDispatchOut", entity);
        }

        [ValidateInput(false)]
        public ActionResult Save(DispatchOutEntity model)
        {
            var entity = new DispatchOutEntity();
            var sessUser = SessionSystem.GetUser();
            if (model.File != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    model.File.InputStream.CopyTo(memoryStream);
                }
            }
            if (ModelState.IsValid)
            {
               
                //model.FileId = 0;
                if (model.Id > 0) //update
                {
                    var sess = SessionSystem.GetUser();
                    Logs.logs("Sửa công văn đi", "Truy cập vào trang DispatchOut", "/DispatchOut/CreateDispatchOut", sess.UserId);
                    entity = _ipldispatchout.ViewDetail(model.Id);
                    if (entity != null && entity.Id > 0)
                    {
                        entity.DispatchNo = model.DispatchNo;
                        entity.DispatchName = model.DispatchName;
                        entity.DispatchType = model.DispatchType;
                        entity.Priority = model.Priority;
                        entity.DateWrite = model.DateWrite;

                        entity.ApproverId = model.ApproverId;
                        entity.DispatchStatus = model.DispatchStatus;

                        entity.DepartmentId = model.DepartmentId;

                        entity.ChiefOfStaffId = model.ChiefOfStaffId;
                        entity.Note = model.Note;
                        entity.Description = model.Description;
                        entity.ModifiedBy = sessUser.UserId;
                        entity.ModifiedDate = DateTime.Now;



                        var retVal = _ipldispatchout.Update(entity);

                        if (retVal)
                        {
                            int departid = entity.Id;

                            var OldFile = _iplfile.ViewDetail(model.FileId);
                            if (model.File != null)
                            {
                                OldFile.IsDel = true;
                                _iplfile.Update(OldFile); //đổi file cũ sang trạng thái đã xóa
                            }
                            SaveFile(model.Id, sessUser.UserId, model);
                            return RedirectToAction("Index", "DispatchOut", new { DepartmentId = model.DepartmentId });
                        }

                    }

                }
                else //insert
                {
                    var sess = SessionSystem.GetUser();
                    Logs.logs("Thêm công văn đi", "Truy cập vào trang DispatchOut", "/DispatchOut/CreateDispatchOut", sess.UserId);
                    model.CreateBy = sessUser.UserId;
                    model.CreatedDate = DateTime.Now;
                    var Id = _ipldispatchout.Insert(model);
                    if (Id > 0)
                    {
                        SaveFile(Id, sessUser.UserId, model);

                        return RedirectToAction("Index", "DispatchOut", new { DepartmentId = model.DepartmentId });
                    }

                }
            }
            else
            {
                ListDropdown();
            }


            ViewBag.Msg = ConstantMsg.ErrorProgress;
            return View("CreateDispatchOut", model);
        }

        public PartialViewResult Delete(int id, string Code = "", string Name = "", int DepartmentId = 0,
            int Type = 0, int Department = 0, int Status = 0, int Priority = 0, string DateWrite = "", int page = 1, int pageSize = 10)
        {
            var sess = SessionSystem.GetUser();
            Logs.logs("Xóa công văn đi", "Truy cập vào trang DispatchOut", "/DispatchOut/", sess.UserId);
            var detail = _ipldispatchout.ViewDetail(id);
            if (detail != null && detail.Id > 0)
            {
                var ret = _ipldispatchout.Delete(id);
            }
            int totalRow = 0;
            var dispatchOut = _ipldispatchout.ListAllPaging(Code, Name, Type, Department, Status, Priority,
               DateWrite, DepartmentId, page, pageSize, ref totalRow).ToPagedList(page, pageSize, totalRow);

            return PartialView("_DispatchOutList", dispatchOut);

        }

        public void SaveFile(int DispatchId, int UserId, DispatchOutEntity model)
        {
            //bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                //foreach (string fileName in Request.Files)
                //{
                HttpPostedFileBase file = model.File;
                //Save file content goes here
                fName = file.FileName;
                if (file != null && file.ContentLength > 0)
                {

                    var originalDirectory = new DirectoryInfo(string.Format("{0}Upload\\File", Server.MapPath(@"\")));

                    string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "DispatchOut");

                    var fileName1 = Path.GetFileNameWithoutExtension(file.FileName) + "_u" + UserId + "_" + DateTime.Now.ToString("ddMMyyyyHHmm") + Path.GetExtension(fName);

                    bool isExists = System.IO.Directory.Exists(pathString);

                    if (!isExists)
                        System.IO.Directory.CreateDirectory(pathString);

                    var path = string.Format("{0}\\{1}", pathString, fileName1);
                    file.SaveAs(path);

                    var modelfiles = new FileEntity();
                    modelfiles.DispatchId = DispatchId;
                    modelfiles.Name = fileName1;
                    modelfiles.UserId = UserId;
                    string fileExtension = "";

                    if (!string.IsNullOrEmpty(fName))
                        fileExtension = Path.GetExtension(fName);
                    modelfiles.MimeType = fileExtension;

                    modelfiles.FileSize = Utils.ToPrettySize(file.ContentLength).ToString();
                    modelfiles.Private = false;
                    modelfiles.IsDel = false;
                    modelfiles.UploadDate = DateTime.Now;
                    modelfiles.Type = true;//false là công văn đến true là công văn đi
                    var Id = _iplfile.Insert(modelfiles);

                    model.Id = DispatchId;
                    model.FileId = int.Parse(Id.ToString());
                    _ipldispatchout.UpdateFileId(model);
                }

                //}

            }
            catch (Exception ex)
            {
                //isSavedSuccessfully = false;
            }


            //if (isSavedSuccessfully)
            //{
            //    return Json(new { Message = fName });
            //}
            //else
            //{
            //    return Json(new { Message = "Lỗi khi upload file" });
            //}

        }

        public void ListDropdown()
        {
            #region load dropdownlist 
            var dispatchtypeAll = _ipldispatchtype.ListAll();
            ViewData["dispatchtypeAll"] = dispatchtypeAll;

            var departmentAll = _ipldepartment.ListAll();
            ViewData["departmentAll"] = departmentAll;

            var employeeAll = _iplemployee.ListAll();
            ViewData["employeeAll"] = employeeAll;

            var employeeTP = _iplemployee.ListAllByPostion(3,0);
            ViewData["employeeTP"] = employeeTP;

            var employeeChief = _iplemployee.ListAllByPostion(5,0);
            ViewData["employeeChief"] = employeeChief;

            var dispatchstatusAll = _ipldispatchstatus.ListAll();
            ViewData["dispatchstatusAll"] = dispatchstatusAll;

            var dispatchpriorityAll = _ipldispatchpriority.ListAll();
            ViewData["dispatchpriorityAll"] = dispatchpriorityAll;
            #endregion
        }
        public ActionResult list()
        {
            return View();
        }

       
    }
}