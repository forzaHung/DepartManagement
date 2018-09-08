using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dispatch;
using Framework.EF;
using System.IO;
using DispatchManagement.Helper;

namespace DispatchManagement.Controllers
{
    public class AttendanceTracking_HistoryController : BaseController
    {
        private IAttendanceTracking _iplAttendanceTracking;
        private IEmployee _iplEmployee;
        public AttendanceTracking_HistoryController() {
            _iplAttendanceTracking = SingletonIpl.GetInstance<IplAttendanceTracking>();
            _iplEmployee = SingletonIpl.GetInstance<IplEmployee>();
        }
        // GET: AttendanceTracking_History
        public ActionResult Index()
        {
            ViewBag.FromDate = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            ViewBag.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }
        public JsonResult GetAttendanceTrackingHistoryList()
        {
            int totalRow = 0;
            string fromDate = Request.QueryString["fromDate"];
            string toDate = Request.QueryString["toDate"];
            string employeeId = Request.QueryString["employee"];
            if (string.IsNullOrWhiteSpace(employeeId))
            {
                return Json(new
                {
                    status = false,
                    mess = "Phải điền tên nhân viên trước khi tìm kiếm.",
                }, JsonRequestBehavior.AllowGet);
            }
            DateTime FromDate;
            DateTime ToDate;
            if (string.IsNullOrWhiteSpace(fromDate))
            {
                var a = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                FromDate = DateTime.ParseExact(a, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
                FromDate = DateTime.ParseExact(fromDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            if (string.IsNullOrWhiteSpace(toDate))
                ToDate = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd"), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            else
                ToDate = DateTime.ParseExact(toDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            var page = GetPagingMessage(Request.QueryString);
            int result = DateTime.Compare(ToDate, FromDate);
            if (result < 0)
            {
                return Json(new
                {
                    status = false,
                    mess = "Ngày bắt đầu không thể lớn hơn ngày kết thúc.",
                }, JsonRequestBehavior.AllowGet);
            }

            var regulationTime = _iplAttendanceTracking.ListAttendanceLastMonth(FromDate, ToDate, employeeId, page.PageIndex, page.PageSize, ref totalRow);
            int totalAbsent = 0;
            int totalTimeLate = 0;
            int totalTimesLate = 0;
            int totalTimeEarly = 0;
            int totalTimesEarly = 0;
            int totalMinutesOverTime = 0;
            int forgetTracking = 0;
            double work = 0;
            var attendanceTracking = AttendanceTracking(regulationTime, ref totalAbsent, ref totalTimeLate, ref totalTimesLate, ref totalTimeEarly, ref totalTimesEarly, ref totalMinutesOverTime, ref forgetTracking, ref work);

            ViewBag.FromDate = FromDate.ToString("yyyy-MM-dd");
            ViewBag.ToDate = ToDate.ToString("yyyy-MM-dd");

            string TotalTimeLate = (totalTimeLate / 60).ToString() + "h:" + (totalTimeLate % 60).ToString() + "p";
            string TotalTimeEarly = (totalTimeEarly / 60).ToString() + "h:" + (totalTimeEarly % 60).ToString() + "p";
            string TotalMinutesOverTime = (totalMinutesOverTime / 60).ToString() + "h:" + (totalMinutesOverTime % 60).ToString() + "p";

            if (regulationTime != null && regulationTime.Count() > 0)
                return Json(new
                {
                    status = true,
                    rows = attendanceTracking.ToList(),
                    total = totalRow,
                    totalAbsent = totalAbsent,
                    totalTimeLate = TotalTimeLate,
                    totalTimeEarly = TotalTimeEarly,
                    totalTimesEarly = totalTimesEarly,
                    totalTimesLate = totalTimesLate,
                    totalMinutesOverTime = TotalMinutesOverTime,
                    forgetTracking = forgetTracking,
                    work = String.Format("{0:0.00}", work),
                }, JsonRequestBehavior.AllowGet);
            else
                return Json(new
                {
                    status = false,
                    mess = "Không tìm thấy dữ liệu.",
                }, JsonRequestBehavior.AllowGet);
        }
        public List<AttendanceTrackingEntity> AttendanceTracking(List<AttendanceTrackingEntity> regulationTime, ref int totalAbsent, ref int totalTimeLate,
            ref int totalTimesLate, ref int totalTimeEarly, ref int totalTimesEarly, ref int totalMinutesOverTime, ref int forgetTracking, ref double work)
        {
            foreach (var item in regulationTime)
            {
                item.DayOfWeek = item.DateCheck.DayOfWeek.ToString();
                //không chấm công ngày chủ nhật
                if (string.IsNullOrWhiteSpace(item.CheckIn) && item.DayOfWeek != "Sunday")
                {
                    item.Absent = "Vắng mặt";
                    totalAbsent++;
                }
                //có chấm công
                if (!string.IsNullOrWhiteSpace(item.CheckIn))
                {
                    double checkIn = TimeSpan.Parse(item.CheckIn).TotalMinutes;
                    if ((item.DayOfWeek == "Saturday" || item.DayOfWeek == "Sunday") && !string.IsNullOrWhiteSpace(item.CheckOut))
                    {
                        int minutes = Convert.ToInt32(TimeSpan.Parse(item.CheckOut).TotalMinutes) - Convert.ToInt32(checkIn);
                        if (minutes > 330)
                        {
                            item.OverTime = ((minutes - 90) / 60).ToString() + "h:" + ((minutes - 90) % 60).ToString() + "p";
                            totalMinutesOverTime += (minutes - 90);
                        }
                        else
                        {
                            item.OverTime = (minutes / 60).ToString() + "h:" + (minutes % 60).ToString() + "p";
                            totalMinutesOverTime += minutes;
                        }
                    }
                    if ((item.DayOfWeek != "Saturday" && item.DayOfWeek != "Sunday"))
                    {
                        if (!string.IsNullOrWhiteSpace(item.CheckOut))
                        {
                            var a = (TimeSpan.Parse(item.CheckOut).TotalMinutes - TimeSpan.Parse(item.CheckIn).TotalMinutes - 90) / 480;
                            work += a;
                            item.Work = String.Format("{0:0.00}", a);
                        }
                        else
                        {
                            work++;
                            item.Work = "1";
                        }
                    }
                    item.TimeLate = Convert.ToInt32(checkIn) - 480;
                    if (item.TimeLate > 0)
                    {
                        totalTimeLate += item.TimeLate;
                        totalTimesLate++;
                    }
                }
                //không chấm công ra về
                if (string.IsNullOrWhiteSpace(item.CheckOut))
                {
                    if (!string.IsNullOrWhiteSpace(item.CheckIn))
                    {
                        forgetTracking++;
                    }
                    item.TimeEarly = 0;
                }
                else
                {
                    double checkOut = TimeSpan.Parse(item.CheckOut).TotalMinutes;
                    if (item.DayOfWeek == "Saturday")
                    {
                        item.TimeEarly = 720 - Convert.ToInt32(checkOut);
                    }
                    else
                    {
                        item.TimeEarly = 1050 - Convert.ToInt32(checkOut);
                    }
                    if (item.TimeEarly > 0)
                    {
                        totalTimeEarly += item.TimeEarly;
                        totalTimesEarly++;
                    }
                }
            }
            return regulationTime.ToList();
        }
        public JsonResult EmployeeSearch(string keyword) {
            var data = _iplEmployee.ListByName(keyword);
            return Json(new { data },JsonRequestBehavior.AllowGet);
        }
        public JsonResult ExportAttendance(string fromDate, string toDate, int idEmployee)
        {
            DateTime FromDate;
            DateTime ToDate;
            if (string.IsNullOrWhiteSpace(fromDate))
            {
                var a = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                FromDate = DateTime.ParseExact(a, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
                FromDate = DateTime.ParseExact(fromDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            if (string.IsNullOrWhiteSpace(toDate))
                ToDate = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd"), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            else
                ToDate = DateTime.ParseExact(toDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            var lst1 = _iplAttendanceTracking.ListAll(FromDate, ToDate, idEmployee);
            int totalAbsent = 0;
            int totalTimeLate = 0;
            int totalTimesLate = 0;
            int totalTimeEarly = 0;
            int totalTimesEarly = 0;
            int totalMinutesOverTime = 0;
            int forgetTracking = 0;
            double work = 0;
            var lst2 = AttendanceTracking(lst1, ref totalAbsent, ref totalTimeLate, ref totalTimesLate, ref totalTimeEarly, ref totalTimesEarly, ref totalMinutesOverTime, ref forgetTracking, ref work);
            if (lst2 == null || lst2.Count < 1)
            {
                return Json(new
                {
                    status = false,
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var fileName = ExportExcel(lst2, totalAbsent, totalTimeLate, totalTimesLate, totalTimeEarly, totalTimesEarly, totalMinutesOverTime, forgetTracking, work, FromDate.ToString("dd/MM/yyyy"), ToDate.ToString("dd/MM/yyyy"));

                return Json(new
                {
                    status = true,
                    FileLink = @"/Download/" + fileName,
                    FileName = fileName,
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public string ExportExcel(List<AttendanceTrackingEntity> lst2,int totalAbsent, int totalTimeLate, int totalTimesLate, int totalTimeEarly, int totalTimesEarly, int totalMinutesOverTime, int forgetTracking, double work, string FromDate, string ToDate)
        {
            MemoryStream st = new MemoryStream();
            string UrlTemplate = ControllerContext.HttpContext.Server.MapPath(@"~/Template/AttendanceTrackingExportTemplate.xlsx");
            using (ExcelTemplateHelper helper = new ExcelTemplateHelper(UrlTemplate, st))
            {
                helper.DeleteSheetTemplate = false;
                helper.Direction = ExcelTemplateHelper.DirectionType.TOP_TO_DOWN;
                #region San luong-Doanh thu
                helper.CurrentSheetName = "Cham-Cong";
                helper.TempSheetName = "Sheet1";
                helper.CurrentPosition = new CellPosition("A1");

                var datas = new List<dynamic>();
                for (int i = 0; i < lst2.Count(); i++)
                {
                    var temp = new
                    {
                        Items = new List<dynamic>(),
                    };
                    temp.Items.Add(new
                    {
                        MaNhanVien = lst2[i].IdEmployee,
                        TenNhanVien = lst2[i].NameEmployee,
                        NgayChamCong = lst2[i].DateCheck.ToString("dd/MM/yyyy"),
                        Thu = lst2[i].DayOfWeek == "Sunday"? "Chủ Nhật":(lst2[i].DayOfWeek == "Monday" ? "Thứ Hai" : (lst2[i].DayOfWeek == "Tuesday" ? "Thứ Ba" : (lst2[i].DayOfWeek == "Wednesday" ? "Thứ Tư" : (lst2[i].DayOfWeek == "Thursday" ? "Thứ Năm" : (lst2[i].DayOfWeek == "Friday" ? "Thứ Sáu" : (lst2[i].DayOfWeek == "Saturday" ? "Thứ Bảy" : "")))))),
                        GioVao = lst2[i].CheckIn,
                        GioRa = lst2[i].CheckOut,
                        DenTre = lst2[i].TimeLate <= 0 ? 0 : lst2[i].TimeLate,
                        VeSom = lst2[i].TimeEarly <= 0 ? 0 : lst2[i].TimeEarly,
                        Cong = lst2[i].Work,
                        TangCa = lst2[i].OverTime,
                        VangMat = lst2[i].Absent
                    });
                    datas.Add(temp);
                }
                var param4 = new
                {
                    DenMuon = (totalTimeLate / 60).ToString() + "h:" + (totalTimeLate % 60).ToString() + "p",
                    SoLanDenMuon = totalTimesLate,
                    VeSom = (totalTimeEarly / 60).ToString() + "h:" + (totalTimeEarly % 60).ToString() + "p",
                    SoLanVeSom = totalTimesEarly,
                    TongThoiGianTangCa = (totalMinutesOverTime / 60).ToString() + "h:" + (totalMinutesOverTime % 60).ToString() + "p",
                    SolanKhongChamCongKhiVe = forgetTracking,
                    TongSoCong = work,
                    TuNgay = FromDate,
                    DenNgay = ToDate,
                    SoNgayVang = totalAbsent,
                };
                var temp1 = helper.CreateTemplate("header");
                var temp2 = helper.CreateTemplate("item");
                //var temp3 = helper.CreateTemplate("total");
                //var temp4 = helper.CreateTemplate("footer");

                helper.InsertData(temp1, param4);
                for (int i = 0; i < datas.Count; i++)
                {
                    helper.InsertDatas(temp2, datas[i].Items);
                    //helper.InsertData(temp3, datas[i].Total);
                }
                //helper.InsertData(temp4, param4.footer);
                helper.CopyWidth();
                #endregion
                helper.DeleteSheet("Sheet1");
            }
            string fileName = string.Format("Export_AttendanceTracking_{0}.xlsx", DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"));
            FileStream fileStream = new FileStream(Server.MapPath(@"~/Download/" + fileName), FileMode.Create, FileAccess.Write);
            st.WriteTo(fileStream);
            fileStream.Close();
            return fileName;
        }
    }
}