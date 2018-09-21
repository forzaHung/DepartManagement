using System;
using System.ComponentModel.DataAnnotations;

namespace Dispatch
{
    //[MetadataType(typeof(EmployeeMetadata))]
    public partial class EmployeeEntity
    {
        #region Properties
        /// <summary>
        /// MinhDuongVan generator
        /// Gets or sets the Id value.
        /// 2017-02-16 15:28:25Z
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// MinhDuongVan generator
        /// Gets or sets the DepartmentId value.
        /// 2017-02-16 15:28:25Z
        /// </summary>
        ///   [Display(Name = "Phòng ban")]
        [Required(ErrorMessage = "Chọn phòng ban cho nhân viên")]
        [Range(1, int.MaxValue, ErrorMessage = "Chọn 1 phòng ban cho nhân viên")]
        public int DepartmentId { get; set; }

        /// <summary>
        /// MinhDuongVan generator
        /// Gets or sets the FirstName value.
        /// 2017-02-16 15:28:25Z
        /// </summary>
        [Display(Name = "Họ và tên đệm")]
        [Required(ErrorMessage = "Nhập Họ và tên đệm")]
        [MaxLength(150, ErrorMessage = "Họ và tên đệm nhân viên quá dài, không được vượt quá 150 kí tự")]
        public string FirstName { get; set; }

        /// <summary>
        /// MinhDuongVan generator
        /// Gets or sets the LastName value.
        /// 2017-02-16 15:28:25Z
        /// </summary>
        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Nhập tên nhân viên")]
        [MaxLength(150, ErrorMessage = "Tên nhân viên quá dài, không được vượt quá 150 kí tự")]
        public string LastName { get; set; }
        [Display(Name = "Địa chỉ")]
        /// <summary>
        /// MinhDuongVan generator
        /// Gets or sets the Address value.
        /// 2017-02-16 15:28:25Z
        /// </summary>
        public string Address { get; set; }
        [Display(Name = "Điện thoại")]
        /// <summary>
        /// MinhDuongVan generator
        /// Gets or sets the Phone value.
        /// 2017-02-16 15:28:25Z
        /// </summary>
        [Required(ErrorMessage = "Cần nhập số điện thoại")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Không phải định dạng số điện thoại")]
        //[Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; }

		/// <summary>
		/// MinhDuongVan generator
		/// Gets or sets the PictureId value.
		/// 2017-02-16 15:28:25Z
		/// </summary>
		public string PicturePath { get; set; }
        [Display(Name = "Trình độ học vấn")]
        /// <summary>
        /// Gets or sets the academic level.
        /// </summary>
        /// <value>The academic level.</value>
        public string AcademicLevel { get; set; }
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>The position.</value>
        [Display(Name ="Chức vụ")]
        [Required(ErrorMessage = "Nhập chức vụ cho nhân viên")]
        public int Position { get; set; }
        /// <summary>
        /// MinhDuongVan generator
        /// Gets or sets the IsActive value.
        /// 2017-02-16 15:28:25Z
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="EmployeeEntity"/> is gender.
        /// </summary>
        /// <value><c>true</c> if gender; otherwise, <c>false</c>.</value>
        [Display(Name ="Giới tính")]
        public bool Gender { get; set; }
        /// <summary>
        /// Gets or sets the nation.
        /// </summary>
        /// <value>The nation.</value>
        [Display(Name ="Dân tộc")]
        public string nation { get; set; }
        /// <summary>
        /// MinhDuongVan generator
        /// Gets or sets the IsDel value.
        /// 2017-02-16 15:28:25Z
        /// </summary>
        public bool IsDel { get; set; }

		/// <summary>
		/// MinhDuongVan generator
		/// Gets or sets the CreatedDate value.
		/// 2017-02-16 15:28:25Z
		/// </summary>
		public DateTime CreatedDate { get; set; }

		/// <summary>
		/// MinhDuongVan generator
		/// Gets or sets the ModifiedDate value.
		/// 2017-02-16 15:28:25Z
		/// </summary>
		public DateTime ModifiedDate { get; set; }
        /// <summary>
        /// Gets or sets the birthday.
        /// </summary>
        /// <value>The birthday.</value>
        [Display(Name ="Ngày sinh")]
        public DateTime Birthday { get; set; }

        public int LastDepartment { get; set; }
        #endregion
    }
}
