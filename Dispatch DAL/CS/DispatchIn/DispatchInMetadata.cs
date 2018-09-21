using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Dispatch
{
    public class DispatchInMetadata
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

        [Display(Name = "Ngày công văn đến")]
        [Required(ErrorMessage = "Chọn ngày cv đến")]        
        public System.DateTime DateFrom { get; set; }

        [Display(Name = "Ngày phát hành")]
        [Required(ErrorMessage = "Chọn ngày phát hành CV")]       
        public System.DateTime DatePublish { get; set; }

        [Display(Name = "Người ký")]
        [Required(ErrorMessage = "Điền người ký")]
        [MaxLength(150, ErrorMessage = "Họ và tên đệm quá dài, không được vượt quá 150 kí tự")]
        public string Signer { get; set; }

        [Display(Name = "Tình trạng")]
        [Required(ErrorMessage = "Chọn tình trạng")]
        [Range(1, int.MaxValue, ErrorMessage = "Chọn 1 tình trạng")]
        public int DispatchStatus { get; set; }

        [Display(Name = "Nơi gửi")]
        [Required(ErrorMessage = "Chọn nơi gửi")]
        [Range(1, int.MaxValue, ErrorMessage = "Chọn 1 nơi gửi")]
        public int AddressFromId { get; set; }

        [Display(Name = "Nơi nhận")]
        [Required(ErrorMessage = "Chọn nơi nhận")]
        [Range(1, int.MaxValue, ErrorMessage = "Chọn 1 nơi nhận")]
        public int AddressToId { get; set; }

        [Display(Name = "Người nhận")]
        [Required(ErrorMessage = "Chọn người nhận")]
        [Range(1, int.MaxValue, ErrorMessage = "Chọn 1 người nhận")]
        public int ReceiverId { get; set; }

        [Display(Name = "Chánh VP")]
        [Required(ErrorMessage = "Chọn chánh VP")]
        [Range(1, int.MaxValue, ErrorMessage = "Chọn 1 chánh VP")]
        public int ChiefOfStaffId { get; set; }

        [Display(Name = "Ghi chú")]        
        public string Note { get; set; }

        [Display(Name = "Nội dung")]
        [Required(ErrorMessage = "Chưa nhập nội dung")]
        public string Description { get; set; }


        [Display(Name = "File up lên")]
        //[Required(ErrorMessage = "Chưa chọn File")]
        public HttpPostedFileBase File { get; set; }

    }
}
