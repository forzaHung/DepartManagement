using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DispatchManagement.Models
{
    public class LoginModel
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "input User Name")]
        [MaxLength(50, ErrorMessage = "User name is too long")]
        public string UserName { get; set; }

        /// <summary>
        /// MinhDuongVan generator
        /// Gets or sets the Password value.
        /// 2016-12-14 17:22:15Z
        /// </summary>
        [Display(Name = "Password")]
        [Required(ErrorMessage = "input Password")]
        [MaxLength(50, ErrorMessage = "Password is too long")]
        public string Password { get; set; }
    }
}