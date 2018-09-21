using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatch
{
    public interface IAttendanceTracking
    {
        bool Insert(string xml);
        List<AttendanceTrackingEntity> ListAttendanceLastMonth(DateTime fromDate, DateTime toDate, string employeeId, int pageIndex, int pageSize, ref int totalRow);
        List<AttendanceTrackingEntity> ListAll(DateTime fromDate, DateTime toDate, int employeeId);
        List<AttendanceTrackingEntity> AttendanceTracking(List<AttendanceTrackingEntity> regulationTime, ref double totalAbsent, ref int totalTimeLate,
            ref int totalTimesLate, ref int totalTimeEarly, ref int totalTimesEarly, ref int totalMinutesOverTime, ref int forgetTracking, ref double work, ref double NumberDaysOfficialSalary, ref double ProbationaryDate, ref double OfficialDate);
        List<AttendanceTrackingEntity> ListAttendanceByMonth(DateTime fromDate, DateTime toDate, string employeeId, int pageIndex, int pageSize, ref int totalRow);
    }
}
