using System.ComponentModel.DataAnnotations;

namespace Dispatch
{
    public class DepartmentMetadata
    {
        [Display(Name = "Trưởng phòng")]
        [Required(ErrorMessage = "Chọn trưởng phòng cho phòng ban")]
        [Range(1, int.MaxValue, ErrorMessage = "Chọn 1 trưởng phòng cho vị trí này")]
        public int EmployeeManagerId { get; set; }

        [Display(Name = "Phó trưởng phòng")]
        [Required(ErrorMessage = "Chọn phó trưởng phòng cho phòng ban")]
        [Range(1, int.MaxValue, ErrorMessage = "Chọn 1 phó trưởng phòng cho vị trí này")]
        public int EmployeeDebutyManagerId { get; set; }

        [Display(Name = "Chánh văn phòng")]
        [Required(ErrorMessage = "Chọn chánh văn phòng cho phòng ban")]
        [Range(1, int.MaxValue, ErrorMessage = "Chọn 1 chánh văn phòng cho vị trí này")]
        public int EmployeeChiefOfTheOfficeId { get; set; }

        [Display(Name = "Tên phòng")]
        [Required(ErrorMessage = "Nhập tên phòng")]
        [MaxLength(150, ErrorMessage = "tên phòng quá dài, không được vượt quá 150 kí tự")]
        public string Name { get; set; }

        [Display(Name = "Địa điểm")]
        [Required(ErrorMessage = "Nhập địa điểm làm việc")]
        [MaxLength(250, ErrorMessage = "tên địa điểm quá dài, không được vượt quá 250 kí tự")]
        public string Location { get; set; }

        [Display(Name = "Ngày thành lập")]
        [Required(ErrorMessage = "Nhập ngày thành lập")]
        //[MaxLength(250, ErrorMessage = "tên địa điểm quá dài, không được vượt quá 250 kí tự")]
        public string CreatedDate { get; set; }
      
        [Display(Name = "Số nhân viên dự kiến")]
        [Range(0,100, ErrorMessage = "Nhập số nhân viên trong khoảng từ 1 đến 100")]
        public int SumEmployeesExpected { get; set; }
    }
}
