using Framework.Configuration;
using Framework.EF;
using Dispatch;
using System.Web.Mvc;
using DispatchManagement.Models;

namespace DispatchManagement.Controllers
{
    public class ProfileController : BaseController
    {
        private IEmployee _iplEmployee;
        private string _imagePath = Config.GetConfigByKey("AvatarPath");
        public ProfileController()
        {
            _iplEmployee = SingletonIpl.GetInstance<IplEmployee>();
        }
        // GET: Backend/Profile
        public ActionResult Index(int? id)
        {
            var iplDep = SingletonIpl.GetInstance<IplDepartment>();
            var allDep = iplDep.ListAll();
            ViewData["Department"] = allDep;
            var fullName = string.Empty;
            var address = string.Empty;
            var phone = string.Empty;
            var avatarPath = string.Empty;
            var sess = SessionSystem.GetUser();
            var emp = new EmployeeEntity();
            id = id ?? sess.EmployeeId;
            if (sess != null)
            {
                emp = _iplEmployee.ViewDetail((int)id);

                fullName = emp != null ? emp.FirstName + " " + emp.LastName : sess.Email;
                address = emp != null ? emp.Address : string.Empty;
                phone = emp != null ? emp.Phone : sess.Phone;
                avatarPath = !string.IsNullOrEmpty(emp.PicturePath) ? _imagePath + emp.PicturePath : "";
            }
            ViewBag.FullName = fullName;
            ViewBag.Address = address;
            ViewBag.Phone = phone;
            ViewBag.Avatar = avatarPath;
            return View("Index", emp);
        }

        public ActionResult Save(EmployeeEntity model)
        {
            var entity = new EmployeeEntity();
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    var sess = SessionSystem.GetUser();
                    Logs.logs("Sửa Profile", "Truy cập vào trang Profile", "/Profile/", sess.UserId);
                    entity = _iplEmployee.ViewDetail(model.Id);
                    if (entity != null && entity.Id > 0)
                    {
                        entity.DepartmentId = model.DepartmentId;
                        entity.FirstName = model.FirstName;
                        entity.LastName = model.LastName;
                        entity.Phone = model.Phone;
                        entity.Address = model.Address;
                        entity.AcademicLevel = model.AcademicLevel;
                        
                        if (model.file != null && model.file.ContentLength > 0)
                        {
                            if (!Utility.CheckfileUpload(model.file.FileName))
                            {
                                ViewBag.msgUpload = ConstantMsg.ErrorImageFormat;
                                return View(model);
                            }
                            var retUpload = Upload.upload(_imagePath, model.file, 250, 250);
                            entity.PicturePath = retUpload.pathThumb;

                        }
                        var retVal = _iplEmployee.Update(entity);
                    }
                }
            }
            var iplDep = SingletonIpl.GetInstance<IplDepartment>();
            var allDep = iplDep.ListAll();
            ViewData["Department"] = allDep;
            return View("Index", entity);
        }
    }
}