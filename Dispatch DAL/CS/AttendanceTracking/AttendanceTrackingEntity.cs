using System;
using System.Collections.Generic;

namespace Dispatch
{
    public partial class AttendanceTrackingEntity
    {
        public long Id { get; set; }
        public DateTime DateCheck { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public int IdEmployee { get; set; }
        public string NameEmployee { get; set; }
        public string DayOfWeek { get; set; }
        public int TimeLate { get; set; }
        public int TimeEarly { get; set; }
        public string Absent { get; set; }
        public string OverTime { get; set; }
        public string Work { get; set; }
    }
    public class ImportModel
    {
        public List<AttendanceTrackingEntity> listAttendanceTracking {get; set;}
    }
}
