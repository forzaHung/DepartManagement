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
    public class DispatchInController : BaseController
    {
        private IDispatchIn _ipldispatchin;
        private IDepartment _ipldepartment;
        private IDispatchType _ipldispatchtype;
        private IDispatchStatus _ipldispatchstatus;
        private IDispatchPriority _ipldispatchpriority;
        private IEmployee _iplemployee;
        private IFile _iplfile;
        private string _FilePath = Config.GetConfigByKey("FileDispatchIn");
        // GET: DispatchIn
        public DispatchInController()
        {
            _ipldispatchin = SingletonIpl.GetInstance<IplDispatchIn>();
            _ipldepartment = SingletonIpl.GetInstance<IplDepartment>();
            _ipldispatchtype = SingletonIpl.GetInstance<IplDispatchType>();
            _ipldispatchstatus = SingletonIpl.GetInstance<IplDispatchStatus>();
            _ipldispatchpriority = SingletonIpl.GetInstance<IplDispatchPriority>();
            _iplemployee = SingletonIpl.GetInstance<IplEmployee>();
            _iplfile = SingletonIpl.GetInstance<IplFile>();
        }
        public ActionResult Index(string Code = "", string Name = "", int AddressToId = 0,
            int Type = 0, int Department = 0, int Status = 0, int Priority = 0, string DateFrom = "", int page = 1, int pageSize = 10)
        {
            #region lưu lại thông tin tìm
            ViewBag.AddressToId = AddressToId;
            ViewBag.Type = Type;
            ViewBag.Status = Status;
            ViewBag.Priority = Priority;
            #endregion
            ViewBag.FilePatch = _FilePath;
            int totalRow = 0;
            if (AddressToId > 0)
            {
                var department = _ipldepartment.ViewDetail(AddressToId);
                if(department != null) ViewBag.departmentname = department.Name;
            }
            #region load dropdownlist các lựa chọn tìm kiếm
            ViewData["dispatchtypeAll"] = _ipldispatchtype.ListAll();
            ViewData["dispatchstatusAll"] = _ipldispatchstatus.ListAll();
            ViewData["departmentAll"] = _ipldepartment.ListAllByTreeView();
            #endregion
            var dispatchIn = _ipldispatchin.ListAllPaging(Code, Name, Type, Department, Status, Priority,
                DateFrom, AddressToId, page, pageSize, ref totalRow);
            return View(dispatchIn.ToPagedList(page, pageSize, totalRow));
        }
        public PartialViewResult GetDispatchIn(string Code = "", string Name = "", int AddressToId = 0,
            int Type = 0, int Department = 0, int Status = 0, int Priority = 0, string DateFrom = "", int page = 1, int pageSize = 10)
        {
            int totalRow = 0;
            var dispatchIn = _ipldispatchin.ListAllPaging(Code, Name, Type, Department, Status, Priority,
               DateFrom, AddressToId, page, pageSize, ref totalRow).ToPagedList(page, pageSize, totalRow);
            return PartialView("_DispatchInList", dispatchIn);
        }
        public ActionResult CreateDispatchIn()
        {
            var entity = new DispatchInEntity();
            ListDropdown();
            return View(entity);
        }
        public ActionResult EditDispatchIn(int? id)
        {
            ListDropdown();

            var File = _iplfile.ViewDetailByDispatch(long.Parse(id.ToString()), false);
            if (File != null)
            {
                ViewBag.FileName = File.Name;
                ViewBag.FileId = File.Id;
            }
            DispatchInEntity entity = new DispatchInEntity();
            if (id != null)
            {
                entity = _ipldispatchin.ViewDetail((int)id);
            }
            return View("CreateDispatchIn", entity);
        }
        [ValidateInput(false)]
        public ActionResult Save(DispatchInEntity model)
        {
            var entity = new DispatchInEntity();
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
                var sess = SessionSystem.GetUser();
                //model.FileId = 0;
                if (model.Id > 0) //update
                {
                    Logs.logs("Sửa Công văn đến", "Truy cập vào trang DispatchIn", "/DispatchIn/CreateDispatchIn", sess.UserId);
                    entity = _ipldispatchin.ViewDetail(model.Id);
                    if (entity != null && entity.Id > 0)
                    {
                        entity.DispatchNo = model.DispatchNo;
                        entity.DispatchName = model.DispatchName;
                        entity.DispatchType = model.DispatchType;
                        entity.Priority = model.Priority;
                        entity.DateFrom = model.DateFrom;
                        entity.DatePublish = model.DatePublish;
                        entity.Signer = model.Signer;
                        entity.DispatchStatus = model.DispatchStatus;
                        entity.AddressFromId = model.AddressFromId;
                        entity.AddressToId = model.AddressToId;
                        entity.ReceiverId = model.ReceiverId;
                        entity.ChiefOfStaffId = model.ChiefOfStaffId;
                        entity.Note = model.Note;
                        entity.Description = model.Description;
                        entity.ModifiedBy = sessUser.UserId;
                        entity.ModifiedDate = DateTime.Now;

                        var retVal = _ipldispatchin.Update(entity);

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
                            return RedirectToAction("Index", "DispatchIn", new { AddressToId = model.AddressToId });
                        }
                    }
                }
                else //insert
                {
                    Logs.logs("Thêm Công văn đến", "Truy cập vào trang DispatchIn", "/DispatchIn/CreateDispatchIn", sess.UserId);
                    model.CreatedBy = sessUser.UserId;
                    model.CreatedDate = DateTime.Now;
                    var Id = _ipldispatchin.Insert(model);
                    if (Id > 0)
                    {
                        SaveFile(Id, sessUser.UserId, model);
                        return RedirectToAction("Index", "DispatchIn", new { AddressToId = model.AddressToId });
                    }
                }
            }else
            {
                ListDropdown();
            }
            ViewBag.Msg = ConstantMsg.ErrorProgress;
            return View("CreateDispatchIn", model);
        }
        public PartialViewResult Delete(int id, string Code = "", string Name = "", int AddressToId = 0,
            int Type = 0, int Department = 0, int Status = 0, int Priority = 0, string DateFrom = "", int page = 1, int pageSize = 10)
        {
            var sess = SessionSystem.GetUser();
            Logs.logs("Xóa công văn đến", "Truy cập vào trang DispatchIn", "/DispatchIn/", sess.UserId);
            var detail = _ipldispatchin.ViewDetail(id);
            if (detail != null && detail.Id > 0)
            {
                var ret = _ipldispatchin.Delete(id);
            }
            int totalRow = 0;
            var dispatchIn = _ipldispatchin.ListAllPaging(Code, Name, Type, Department, Status, Priority,
               DateFrom, AddressToId, page, pageSize, ref totalRow).ToPagedList(page, pageSize, totalRow);

            return PartialView("_DispatchInList", dispatchIn);
        }
        public void SaveFile(int DispatchId, int UserId, DispatchInEntity model)
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

                    string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "DispatchIn");

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
                    modelfiles.Type = false;//false là công văn đến true là công văn đi
                    var Id = _iplfile.Insert(modelfiles);

                    model.Id = DispatchId;
                    model.FileId =int.Parse(Id.ToString());
                    _ipldispatchin.UpdateFileId(model);
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
            ViewData["dispatchtypeAll"] = _ipldispatchtype.ListAll();
            ViewData["departmentAll"] = _ipldepartment.ListAllByTreeView();
            ViewData["employeeAll"] = _iplemployee.ListAll();
            ViewData["employeeChief"] = _iplemployee.ListAllByPostion(5, 0);
            ViewData["dispatchstatusAll"] = _ipldispatchstatus.ListAll();
            ViewData["dispatchpriorityAll"] = _ipldispatchpriority.ListAll();
            #endregion
        }
    }
}