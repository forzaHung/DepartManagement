using System.ComponentModel.DataAnnotations;

namespace Dispatch
{
    public class UserMetadata
    {
        
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Dữ liệu không được bỏ trống")]
        [MaxLength(50, ErrorMessage = "Tên đăng nhập quá dài")]
        public string UserName { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Dữ liệu không được bỏ trống")]
        [MaxLength(50, ErrorMessage = "Mật khẩu quá dài, Mật khẩu từ 8 - 20 kí tự")]
        [Range(8, int.MaxValue, ErrorMessage = "Mật khẩu đăng nhập quá ngắn.")]
        public string Password { get; set; }
       
    }
}
