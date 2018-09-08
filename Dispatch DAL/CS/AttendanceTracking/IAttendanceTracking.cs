using System;
using System.Collections.Generic;

namespace Dispatch
{
    public interface IAttendanceTracking
    {
        bool Insert(string xml);
        List<AttendanceTrackingEntity> ListAttendanceLastMonth(DateTime fromDate, DateTime toDate, string employeeId, int pageIndex, int pageSize, ref int totalRow);
        List<AttendanceTrackingEntity> ListAll(DateTime fromDate, DateTime toDate, int employeeId);
    }
}
