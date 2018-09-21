using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatch.BookMeeting
{
    public class BookMeetingMetadata
    {
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        //public string EmployeeName { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        //public string DepartmentName { get; set; }
        [Required]
        public int RoomID { get; set; }
        //public string RoomName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifierDate { get; set; }
        [Required]
        public DateTime TimeStart { get; set; }
        [Required]
        public DateTime TimeEnd { get; set; }
        public bool isActive { get; set; }
        public bool isDel { get; set; }
    }
}
