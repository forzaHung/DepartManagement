using Framework;
using Framework.Data;
using Framework.EF;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatch
{
    public class IplAttendanceTracking : BaseIpl<ADOProvider>, IAttendanceTracking
    {
        private IHoliday _iplHoliday;
        private IEmployee_Payroll _iplEmployeePayroll;
        public IplAttendanceTracking() {
            _iplHoliday = SingletonIpl.GetInstance<IplHoliday>();
            _iplEmployeePayroll = SingletonIpl.GetInstance<IplEmployee_Payroll>();
        }
        public bool Insert(string xml)
        {
            bool flag = false;
            try
            {
                var p = new DynamicParameters();
                p.Add("@xml", xml);
                flag = (bool)unitOfWork.ProcedureExecute("ptgroup_AttendanceTracking_Import", p);
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
            }
            return flag;
        }
        public List<AttendanceTrackingEntity> ListAttendanceLastMonth(DateTime fromDate, DateTime toDate,string employeeId, int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@fromDate", fromDate);
                p.Add("@toDate", toDate);
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@employeeId", employeeId);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<AttendanceTrackingEntity>("ptgroupAttendanceTracking_ListLastMonth", p);
                totalRow = p.Get<int>("@totalRow");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public List<AttendanceTrackingEntity> ListAttendanceByMonth(DateTime fromDate, DateTime toDate, string employeeId, int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@fromMonth", fromDate.Month);
                p.Add("@toMonth", toDate.Month);
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@employeeId", employeeId);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<AttendanceTrackingEntity>("ptgroupAttendanceTracking_ListByMonth", p);
                totalRow = p.Get<int>("@totalRow");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public List<AttendanceTrackingEntity> ListAll(DateTime fromDate, DateTime toDate, int employeeId)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@fromDate", fromDate);
                p.Add("@toDate", toDate);
                p.Add("@employeeId", employeeId);
                var data = unitOfWork.Procedure<AttendanceTrackingEntity>("ptgroupAttendanceTracking_ListAll", p);
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public List<AttendanceTrackingEntity> AttendanceTracking(List<AttendanceTrackingEntity> regulationTime, ref double totalAbsent, ref int totalTimeLate,
            ref int totalTimesLate, ref int totalTimeEarly, ref int totalTimesEarly, ref int totalMinutesOverTime, ref int forgetTracking, ref double work, 
            ref double NumberDaysOfficialSalary, ref double ProbationaryDate, ref double OfficialDate)
        {
            var employee =  _iplEmployeePayroll.CheckDetail(regulationTime.FirstOrDefault().IdEmployee);
            foreach (var item in regulationTime)
            {
                var CheckHolidayDate = _iplHoliday.Detail(item.DateCheck);
                item.DayOfWeek = item.DateCheck.DayOfWeek.ToString();
                //Đếm ngày công trong tháng
                NumberDaysOfficialSalary++;
                if (DateTime.Compare(item.DateCheck, employee.ProbationaryFromDate) >= 0 && DateTime.Compare(employee.ProbationaryToDate,item.DateCheck) >= 0)
                    ProbationaryDate = ProbationaryDate + 1;
                else
                    OfficialDate = OfficialDate + 1;
                if (item.DayOfWeek == "Sunday")
                {
                    NumberDaysOfficialSalary = NumberDaysOfficialSalary - 1;
                    if (DateTime.Compare(item.DateCheck, employee.ProbationaryFromDate) >= 0 && DateTime.Compare(employee.ProbationaryToDate, item.DateCheck) >= 0)
                        ProbationaryDate = ProbationaryDate - 1;
                    else
                        OfficialDate = OfficialDate - 1;
                }
                if (item.DayOfWeek == "Saturday")
                {
                    NumberDaysOfficialSalary = NumberDaysOfficialSalary - 0.5;
                    if (DateTime.Compare(item.DateCheck, employee.ProbationaryFromDate) >= 0 && DateTime.Compare(employee.ProbationaryToDate, item.DateCheck) >= 0)
                        ProbationaryDate = ProbationaryDate - 0.5;
                    else
                        OfficialDate = OfficialDate - 0.5;
                }
                if (CheckHolidayDate != null)
                {
                    NumberDaysOfficialSalary = NumberDaysOfficialSalary - 1;
                    if (DateTime.Compare(item.DateCheck, employee.ProbationaryFromDate) >= 0 && DateTime.Compare(employee.ProbationaryToDate, item.DateCheck) >= 0)
                        ProbationaryDate = ProbationaryDate - 1;
                    else
                        OfficialDate = OfficialDate - 1;
                }
                //không chấm công ngày chủ nhật
                if (string.IsNullOrWhiteSpace(item.CheckIn) && item.DayOfWeek != "Sunday")
                {
                    if (CheckHolidayDate == null)
                    {
                        item.Absent = "Vắng mặt";
                        if (item.DayOfWeek == "Saturday")
                        {
                            totalAbsent += 0.5;
                            if (DateTime.Compare(item.DateCheck, employee.ProbationaryFromDate) >= 0 && DateTime.Compare(employee.ProbationaryToDate, item.DateCheck) >= 0)
                                ProbationaryDate = ProbationaryDate - 0.5;
                            else
                                OfficialDate = OfficialDate - 0.5;
                        }
                        else
                        {
                            totalAbsent++;
                            if (DateTime.Compare(item.DateCheck, employee.ProbationaryFromDate) >= 0 && DateTime.Compare(employee.ProbationaryToDate, item.DateCheck) >= 0)
                                ProbationaryDate = ProbationaryDate - 1;
                            else
                                OfficialDate = OfficialDate - 1;
                        }
                    }
                    else
                    {
                        item.Absent = CheckHolidayDate.Description;
                    }
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
                        if (Convert.ToInt32(checkOut) > 810)
                        {
                            item.TimeEarly = 1050 - Convert.ToInt32(checkOut);
                        }
                        else
                        {
                            item.TimeEarly = 1050 - 90 - Convert.ToInt32(checkOut);
                        }
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
    }
}
