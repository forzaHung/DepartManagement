using Dispatch;
using Framework.EF;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DispatchManagement.Controllers
{
    public class Employee_PayrollController : BaseController
    {
        private IEmployee_Payroll _iplEmployeePayroll;
        private IEmployee _iplEmployee;
        private IAttendanceTracking _iplAttendanceTracking;
        public Employee_PayrollController()
        {
            _iplEmployeePayroll = SingletonIpl.GetInstance<IplEmployee_Payroll>();
            _iplEmployee = SingletonIpl.GetInstance<IplEmployee>();
            _iplAttendanceTracking = SingletonIpl.GetInstance<IplAttendanceTracking>();
        }
        // GET: Employee_Payroll
        public ActionResult Index()
        {
            ViewBag.FromDate = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            ViewBag.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.FromMonth = DateTime.Now.AddMonths(-1).ToString("MM/yyyy");
            ViewBag.ToMonth = DateTime.Now.ToString("MM/yyyy");
            return View();
        }
        public ActionResult ConfigPayroll()
        {
            return View();
        }
        public JsonResult EmployeeSearch(string keyword)
        {
            var data = _iplEmployee.ListByName(keyword);
            return Json(new { data }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CreatePayroll(string fromDate, string toDate, int idEmployee)
        {

            DateTime FromDate = new DateTime();
            DateTime ToDate = new DateTime();
            int result = CompareDate(fromDate,toDate,ref ToDate,ref FromDate);
            if (result < 0)
                return Json(new { stt = false, mess = "Ngày bắt đầu không thể lớn hơn ngày kết thúc.", }, JsonRequestBehavior.AllowGet);
            if (ToDate.Subtract(FromDate).TotalDays > 31)
                return Json(new { stt = false, mess = "Ngày kết thúc và ngày bắt đầu cách nhau không quá 31 ngày.", }, JsonRequestBehavior.AllowGet);
            if (_iplEmployeePayroll.CheckPayroll(FromDate, ToDate, idEmployee) != null && _iplEmployeePayroll.CheckPayroll(FromDate, ToDate, idEmployee).Count() > 0)
                return Json(new { stt = false, mess = "Đã tạo bảng lương trong khoảng thời gian này." }, JsonRequestBehavior.AllowGet);

            var lst1 = _iplAttendanceTracking.ListAll(FromDate, ToDate, idEmployee);
            if (lst1 == null || lst1.Count == 0)
                return Json(new { stt = false, mess = "Không có dữ liệu tạo bảng lương." }, JsonRequestBehavior.AllowGet);
            var entity = ComputePayroll(idEmployee, lst1, FromDate, ToDate);
            _iplEmployeePayroll.Insert(entity);
            return Json(new { stt = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CreatePayrollMulti(string fromDate, string toDate)
        {
            DateTime FromDate = new DateTime();
            DateTime ToDate = new DateTime();

            int result = CompareDate(fromDate, toDate, ref FromDate,ref ToDate);
            if (result < 0)
                return Json(new { stt = false, mess = "Ngày bắt đầu không thể lớn hơn ngày kết thúc.", }, JsonRequestBehavior.AllowGet);
            if (ToDate.Subtract(FromDate).TotalDays > 31)
                return Json(new { stt = false, mess = "Ngày kết thúc và ngày bắt đầu cách nhau không quá 31 ngày.", }, JsonRequestBehavior.AllowGet);

            var listEmployee = _iplEmployee.ListAll();
            List<string> ListReason = new List<string>();
            foreach (var item in listEmployee)
            {
                var idEmployee = item.Id;
                if (_iplEmployeePayroll.CheckPayroll(FromDate, ToDate, idEmployee) != null && _iplEmployeePayroll.CheckPayroll(FromDate, ToDate, idEmployee).Count() > 0)
                {
                    string reason = item.FirstName + " " + item.LastName + ": Đã tạo bảng lương trong khoảng thời gian này.";
                    ListReason.Add(reason);
                    continue;
                }
                var lst1 = _iplAttendanceTracking.ListAll(FromDate, ToDate, idEmployee);
                if (lst1 == null || lst1.Count == 0)
                {
                    string reason = item.FirstName + " " + item.LastName + ": Không có dữ liệu tạo bảng lương.";
                    ListReason.Add(reason);
                    continue;
                }
                var entity = ComputePayroll(idEmployee, lst1, FromDate, ToDate);
                _iplEmployeePayroll.Insert(entity);

            }
            string ReasonError = "";
            foreach (var item in ListReason)
            {
                ReasonError += item + "<br>";
            }
            string Errormsg = ListReason.Count +"/"+listEmployee.Count + " nhân viên không thêm được bảng lương!";
            
            return Json(new { stt = ListReason.Count == 0, mess = Errormsg, displayReason = ReasonError }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdatePayroll(int Id, string fromDate, string toDate, int idEmployee)
        {
            DateTime FromDate = new DateTime();
            DateTime ToDate = new DateTime();
            int result = CompareDate(fromDate, toDate, ref FromDate, ref ToDate);
            if (result < 0)
                return Json(new { stt = false, mess = "Ngày bắt đầu không thể lớn hơn ngày kết thúc.", }, JsonRequestBehavior.AllowGet);
            if (ToDate.Subtract(FromDate).TotalDays > 31)
                return Json(new { stt = false, mess = "Ngày kết thúc và ngày bắt đầu cách nhau không quá 31 ngày.", }, JsonRequestBehavior.AllowGet);
            //if (_iplEmployeePayroll.CheckPayroll(FromDate, ToDate, idEmployee) != null && _iplEmployeePayroll.CheckPayroll(FromDate, ToDate, idEmployee).Count() > 0)
            //    return Json(new { stt = false, mess = "Đã tạo bảng lương trong khoảng thời gian này." }, JsonRequestBehavior.AllowGet);

            var lst1 = _iplAttendanceTracking.ListAll(FromDate, ToDate, idEmployee);
            if (lst1 == null || lst1.Count == 0)
                return Json(new { stt = false, mess = "Không có dữ liệu tạo bảng lương." }, JsonRequestBehavior.AllowGet);
            var entity = ComputePayroll(idEmployee, lst1, FromDate, ToDate);

            entity.Id = Id;
            _iplEmployeePayroll.Update(entity);
            return Json(new { stt = true }, JsonRequestBehavior.AllowGet);
        }
        public int CompareDate(string fromDate, string toDate, ref DateTime FromDate, ref DateTime ToDate)
        {
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

            return DateTime.Compare(ToDate, FromDate);
        }
        public Employee_PayrollEntity ComputePayroll(int idEmployee,List<AttendanceTrackingEntity> lst1, DateTime FromDate ,DateTime ToDate)
        {
            double totalAbsent = 0;
            int totalTimeLate = 0;
            int totalTimesLate = 0;
            int totalTimeEarly = 0;
            int totalTimesEarly = 0;
            int totalMinutesOverTime = 0;
            int forgetTracking = 0;
            double work = 0;
            double NumberDaysOfficialSalary = 0;
            double ProbationaryDate = 0;
            double OfficialDate = 0;
            var lst2 = _iplAttendanceTracking.AttendanceTracking(lst1, ref totalAbsent, ref totalTimeLate, ref totalTimesLate, ref totalTimeEarly, ref totalTimesEarly, ref totalMinutesOverTime, ref forgetTracking, ref work, ref NumberDaysOfficialSalary, ref ProbationaryDate, ref OfficialDate);

            var employee = _iplEmployee.ViewDetail(idEmployee);

            var entity = new Employee_PayrollEntity();
    
            entity.WageAgreement = employee.WageAgreement;
            entity.IdEmployee = idEmployee;
            entity.NamePayroll = "Lương tháng " + FromDate.ToString("MM") + " " + lst2.FirstOrDefault().NameEmployee;
            entity.StandardWorkDays = NumberDaysOfficialSalary;
            entity.NumberDaysOfficialSalary = OfficialDate;
            entity.DaysProbationarySalary = ProbationaryDate;
            entity.PremiumSalary = 4500000;
            entity.CompulsoryInsurance = entity.PremiumSalary / 200 * 21;
            entity.UnionFees = entity.WageAgreement / 100 * 1;
            entity.TotalMonthlySalary = entity.WageAgreement * entity.DaysProbationarySalary / entity.StandardWorkDays * 85 / 100 + entity.WageAgreement * entity.NumberDaysOfficialSalary / entity.StandardWorkDays - entity.CompulsoryInsurance - entity.UnionFees;
            entity.CreateDate = DateTime.Now;
            entity.ToDate = ToDate;
            entity.FromDate = FromDate;

            return entity;
        }
        public JsonResult GetEmployeePayrollList()
        {
            int totalRow = 0;
            string fromDate = Request.QueryString["fromMonth"];
            string toDate = Request.QueryString["toMonth"];
            int employeeId = Request.QueryString["employee"] == "" ? 0 : int.Parse(Request.QueryString["employee"]);
            DateTime FromDate;
            DateTime ToDate;
            if (string.IsNullOrWhiteSpace(fromDate))
            {
                var a = DateTime.Now.AddDays(-30).ToString("dd/MM/yyyy");
                FromDate = DateTime.ParseExact(a, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
                FromDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            if (string.IsNullOrWhiteSpace(toDate))
                ToDate = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            else
                ToDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var page = GetPagingMessage(Request.QueryString);
            int result = DateTime.Compare(ToDate, FromDate);
            //bool result = (ToDate.Month < FromDate.Month);
            if (result < 0)
            {
                return Json(new
                {
                    status = false,
                    mess = "Ngày bắt đầu không thể lớn hơn ngày ngày kết thúc.",
                }, JsonRequestBehavior.AllowGet);
            }

            var regulationTime = _iplEmployeePayroll.ListByMonthPaging(FromDate, ToDate, employeeId, page.PageIndex, page.PageSize, ref totalRow);

            if (regulationTime != null && regulationTime.Count() > 0)
                return Json(new
                {
                    status = true,
                    rows = regulationTime.ToList(),
                    total = totalRow,
                }, JsonRequestBehavior.AllowGet);
            else
                return Json(new
                {
                    status = false,
                    mess = "Không tìm thấy dữ liệu.",
                }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEmployeePayrollConfigList()
        {
            int totalRow = 0;
            var page = GetPagingMessage(Request.QueryString);
            int employeeId = Request.QueryString["employee"] == "" ? 0 : int.Parse(Request.QueryString["employee"]);

            var regulationTime = _iplEmployeePayroll.ListConfigByPaging(employeeId, page.PageIndex, page.PageSize, ref totalRow);

            if (regulationTime != null && regulationTime.Count() > 0)
                return Json(new
                {
                    status = true,
                    rows = regulationTime.ToList(),
                    total = totalRow,
                }, JsonRequestBehavior.AllowGet);
            else
                return Json(new
                {
                    status = false,
                    mess = "Không tìm thấy dữ liệu.",
                }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int Id)
        {
            if (_iplEmployeePayroll.Delete(Id))
            {
                return Json(new
                {
                    stt = true
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                stt = false
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ChangeWageAgreement(int Id, decimal wageAgreement)
        {
            if (_iplEmployeePayroll.UpdateWageAgreement(Id, wageAgreement))
            {
                return Json(new { stt = true, mess = "Cập nhật lương thoả thuận thành công!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { stt = false, mess = "Có lỗi trong quá trình cập nhật lương thoả thuận." }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ChangeProbationaryFromDate(int Id, string probationaryFromDate)
        {
            DateTime Date;
            if (string.IsNullOrWhiteSpace(probationaryFromDate))
            {
                var a = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                Date = DateTime.ParseExact(a, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
                Date = DateTime.ParseExact(probationaryFromDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            var xxx = _iplEmployee.ViewDetail(Id);
            DateTime Todate = xxx.ProbationaryToDate;
            if (Todate.ToString("dd/MM/yyyy") != "01/01/0001")
            {
                int result = DateTime.Compare(Todate, Date);
                if (result < 0)
                    return Json(new { stt = false, mess = "Ngày bắt đầu không thể lớn hơn ngày kết thúc." }, JsonRequestBehavior.AllowGet);
            }
            if (_iplEmployeePayroll.UpdateProbationaryFromDate(Id, Date))
                return Json(new { stt = true, mess = "Cập nhật cấu hình thành công!" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { stt = false, mess = "Có lỗi trong quá trình cập nhật cấu hình." }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ChangeProbationaryToDate(int Id, string probationaryToDate)
        {
            DateTime Date;
            if (string.IsNullOrWhiteSpace(probationaryToDate))
            {
                var a = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                Date = DateTime.ParseExact(a, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
                Date = DateTime.ParseExact(probationaryToDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            DateTime FromDate = _iplEmployee.ViewDetail(Id).ProbationaryFromDate;
            if (FromDate.ToString("dd/MM/yyyy") != "01/01/0001")
            {
                int result = DateTime.Compare(Date, FromDate);
                if (result < 0)
                    return Json(new { stt = false, mess = "Ngày bắt đầu không thể lớn hơn ngày kết thúc." }, JsonRequestBehavior.AllowGet);
            }
            if (_iplEmployeePayroll.UpdateProbationaryToDate(Id, Date))
                return Json(new { stt = true, mess = "Cập nhật cấu hình thành công!" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { stt = false, mess = "Có lỗi trong quá trình cập nhật cấu hình." }, JsonRequestBehavior.AllowGet);
        }
    }
}