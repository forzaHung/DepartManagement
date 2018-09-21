using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatch
{
    public class DispatchOutMetadata
    {
        [Display(Name = "Số công văn")]
        [Required(ErrorMessage = "Điền số công văn")]        
        public int DispatchNo { get; set; }


        [Display(Name = "Công văn")]
        [Required(ErrorMessage = "Điền công văn")]      
        public int DispatchName { get; set; }

        [Display(Name = "Loại công văn")]
        [Required(ErrorMessage = "Chọn loại công văn")]
        [Range(1, int.MaxValue, ErrorMessage = "Chọn loại công văn")]
        public int DispatchType { get; set; }

        [Display(Name = "Độ ưu tiên")]
        [Required(ErrorMessage = "Chọn độ ưu tiên")]
        [Range(1, int.MaxValue, ErrorMessage = "Chọn độ ưu tiên")]
        public int Priority { get; set; }

        [Display(Name = "Ngày soạn CV")]
        [Required(ErrorMessage = "Chọn ngày soạn CV")]        
        public System.DateTime DateWrite { get; set; }

        [Display(Name = "Tình trạng")]
        [Required(ErrorMessage = "Chọn tình trạng")]
        [Range(1, int.MaxValue, ErrorMessage = "Chọn tình trạng")]
        public int DispatchStatus { get; set; }

        [Display(Name = "Người soạn")]
        [Required(ErrorMessage = "Điền người soạn")]
        [Range(1, int.MaxValue, ErrorMessage = "Chọn người soạn")]
        public int WriterId { get; set; }

        [Display(Name = "Thuộc phòng ban")]
        [Required(ErrorMessage = "Chọn phòng ban")]
        [Range(1, int.MaxValue, ErrorMessage = "Chọn 1 phòng ban")]
        public int DepartmentId { get; set; }


        [Display(Name = "TP duyệt")]
        [Required(ErrorMessage = "Chọn trưởng phòng")]
        [Range(1, int.MaxValue, ErrorMessage = "Chọn 1 trưởng phòng")]
        public int ApproverId { get; set; }

        
        [Display(Name = "Chánh VP")]
        [Required(ErrorMessage = "Chọn chánh VP")]
        [Range(1, int.MaxValue, ErrorMessage = "Chọn 1 chánh VP")]
        public int ChiefOfStaffId { get; set; }

        [Display(Name = "Ghi chú")]        
        public string Note { get; set; }

        [Display(Name = "Tên file")]
        //[Required(ErrorMessage = "Chưa chọn File")]
        public string File { get; set; }

        [Display(Name = "Nội dung")]
        [Required(ErrorMessage = "Chưa nhập nội dung")]
        public string Description { get; set; }
    }
}
