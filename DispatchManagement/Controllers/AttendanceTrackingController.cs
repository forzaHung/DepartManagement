using Dispatch;
using DispatchManagement.Helper;
using Framework.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DispatchManagement.Controllers
{
    public class AttendanceTrackingController : BaseController
    {
        private IAttendanceTracking _iplAttendanceTracking;
        public AttendanceTrackingController()
        {
            _iplAttendanceTracking = SingletonIpl.GetInstance<IplAttendanceTracking>();
        }
        // GET: AttendanceTracking
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Import(HttpPostedFileBase file)
        {
            if (file == null)
            {
                ViewBag.Msg = "Yêu cầu đẩy file lên trước.";
                return View();
            }
            if (!Utility.CheckfileUpload(file.FileName))
            {
                ViewBag.Msg = "File không hợp lệ.";
                return View();
            }
            Stream stream;
            try
            {
                stream = file.InputStream;
                List<string> invalidData = new List<string>();
                var lstProduct = UpdateListAttendanceTracking(file.InputStream, ref invalidData);
                if (lstProduct != null && invalidData.Count > 0)
                {
                    string errors = String.Join("</br>", invalidData);
                    ViewBag.Msg = errors;
                }
                return View("Import", lstProduct);
            }
            catch (Exception ex)
            {
                ViewBag.Msg = ex.Message;
                return View("Import");
            }
        }
        public List<AttendanceTrackingEntity> UpdateListAttendanceTracking(Stream stream, ref List<string> invalidData)
        {
            List<AttendanceTrackingEntity> data = new List<AttendanceTrackingEntity>();
            var _dictMapColumns = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                    {
                        {"Ngày Chấm Công", "Ngày Chấm Công" },
                        {"Giờ Vào", "Giờ Vào" },
                        {"Giờ Ra", "Giờ Ra" },
                        {"Mã nhân viên","Mã nhân viên"},
                    };
            var attendanceTrackingExcelParser = new AttendanceTrackingExcelParse(stream, _dictMapColumns);
            invalidData = new List<string>();
            data = attendanceTrackingExcelParser.Parse(ref invalidData);
            return data;
        }
        public ActionResult SaveImport(List<AttendanceTrackingEntity> lstInserted)
        {
            var modelXml = new ImportModel();
            modelXml.listAttendanceTracking = lstInserted;
            var xmlFile = XMLHelper.SerializeXML<ImportModel>(modelXml);
            var retVal = _iplAttendanceTracking.Insert(xmlFile);
            return Json(new
            {
                status = retVal
            });
        }
        public FileResult Download()
        {
            var download = Server.MapPath("~");
            byte[] fileBytes = System.IO.File.ReadAllBytes(download + "Template\\AttendanceTrackingTemplate.xlsx");
            string fileName = "AttendanceTrackingTemplate.xlsx";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}