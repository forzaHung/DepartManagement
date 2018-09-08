using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DispatchManagement.Models
{
    public class ChangePasswordModel
    {
        [Display(Name = "Old Password")]
        [Required(ErrorMessage = "input Password")]
        [MaxLength(50, ErrorMessage = "Password is too long")]
        public string OldPassword { get; set; }

        [Display(Name = "New Password")]
        [Required(ErrorMessage = "input Password")]
        [MaxLength(50, ErrorMessage = "Password is too long")]
        public string NewPassword { get; set; }

        [Display(Name = "Re-type Password")]
        [NotMapped]
        [Required(ErrorMessage = "Confirm Password required")]
        [Compare("NewPassword", ErrorMessage = "Password doesn't match.")]
        public string RetypePassword { get; set; }
    }
}
