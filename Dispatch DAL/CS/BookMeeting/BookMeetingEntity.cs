using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatch
{
    public class BookMeetingEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nhân viên không được trống")]
        [Display(Name = "Mã Nhân Viên")]
        public int EmployeeId { get; set; }
        public string NameEmployee { get; set; }
        [Required(ErrorMessage = "Phòng ban không được trống")]
        [Display(Name = "Mã Phòng Ban")]
        public int DepartmentId { get; set; }
        public string NameDepartment { get; set; }
        [Required(ErrorMessage = "Phòng họp không được trống")]
        [Display(Name = "Mã Phòng Họp")]
        public int RoomID { get; set; }
        public string NameRoom { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ngày đặt phòng")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "Ngày Sửa")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ModifierDate { get; set; }
        [Required(ErrorMessage = "Chọn thời gian bắt đầu")]
        [Display(Name = "Thời gian Bắt Đầu")]
        public DateTime TimeStart { get; set; }

        [Required(ErrorMessage = "Chọn thời gian kết thúc")]
        [Display(Name = "Thời gian Kết Thúc")]
        public DateTime TimeEnd { get; set; }

        public bool isActive { get; set; }
        public bool isDel { get; set; }

        public string ConvertDate(DateTime dt)
        {
            return String.Format("{0:t}", dt);
        }

    }
}
