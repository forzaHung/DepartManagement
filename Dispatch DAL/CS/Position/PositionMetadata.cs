using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatch
{
    public class PositionMetadata
    {
     

        [Display(Name = "Chức vụ")]
        [Required(ErrorMessage = "Nhập chức vụ")]
        [MaxLength(500, ErrorMessage = "Tên chức vụ quá dài, không được vượt quá 500 kí tự")]
        public string Name { get; set; }
      
    }
}
