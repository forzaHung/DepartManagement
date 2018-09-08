using System.ComponentModel.DataAnnotations;

namespace Dispatch
{
    public class EmployeeMetadata
    {
        [Display(Name = "Phòng ban")]
        [Required(ErrorMessage = "Chọn phòng ban cho nhân viên")]
        [Range(1, int.MaxValue, ErrorMessage = "Chọn 1 phòng ban cho nhân viên")]
        public int DepartmentId { get; set; }
        [Display(Name = "Họ và tên đệm")]
        [Required(ErrorMessage = "Nhập Họ và tên đệm")]
        [MaxLength(150, ErrorMessage = "Họ và tên đệm nhân viên quá dài, không được vượt quá 150 kí tự")]
        public string FirstName { get; set; }

        /// <summary>
        /// MinhDuongVan generator
        /// Gets or sets the LastName value.
        /// 2016-12-14 17:22:14Z
        /// </summary>
        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Nhập tên nhân viên")]
        [MaxLength(150, ErrorMessage = "Tên nhân viên quá dài, không được vượt quá 150 kí tự")]
        public string LastName { get; set; }
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Nhập địa chỉ nhận viên")]
        /// <summary>
        /// MinhDuongVan generator
        /// Gets or sets the Address value.
        /// 2016-12-14 17:22:14Z
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// MinhDuongVan generator
        /// Gets or sets the Phone value.
        /// 2016-12-14 17:22:14Z
        /// </summary>
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; }
        [Display(Name ="Chức vụ")]
        [Required(ErrorMessage = "Nhập chức vụ cho nhân viên")]
        public string Position { get; set; }
    }
}
