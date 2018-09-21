using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatch
{
    public class Employee_HistoryMetadata
    {
     

        [Display(Name = "Tên phòng")]
        [Required(ErrorMessage = "Nhập tên phòng")]
        [MaxLength(2000, ErrorMessage = "Tên phòng quá dài, không được vượt quá 2000 kí tự")]
        public string DepartmentName { get; set; }
        [Display(Name = "Tên của Nhân sự")]
        [Required(ErrorMessage = "Nhập tên")]
        [MaxLength(50, ErrorMessage = "Tên nhân sự quá dài, không được vượt quá 50 kí tự")]
        public string FirstName { get; set; }

        [Display(Name = "Họ của Nhân sự")]
        [Required(ErrorMessage = "Nhập Họ")]
        [MaxLength(50, ErrorMessage = "Họ nhân sự quá dài, không được vượt quá 50 kí tự")]
        public string LastName { get; set; }


        [Display(Name = "Ngày vào làm")]
        [Required(ErrorMessage = "Nhập ngày vào làm")]      
        public DateTime TimeIn { get; set; }

        [Display(Name = "Ngày chuyển")]
        [Required(ErrorMessage = "Nhập ngày chuyển")]
        public DateTime TimeOut { get; set; }
    }
}
