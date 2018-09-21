using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatch
{
    public class DispatchPriorityMetadata
    {
        [Display(Name = "Độ ưu tiên")]
        [Required(ErrorMessage = "Chọn độ ưu tiên")]
        [Range(1, int.MaxValue, ErrorMessage = "Chọn độ ưu tiên")]
        public int Name { get; set; }
       
    }
}
