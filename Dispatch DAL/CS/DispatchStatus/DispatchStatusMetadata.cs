using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatch
{
    public class DispatchStatusMetadata
    {
        [Display(Name = "Tình trạng")]
        [Required(ErrorMessage = "Chọn tình trạng công văn")]
        [Range(1, int.MaxValue, ErrorMessage = "Chọn tình trạng công văn")]
        public int Name { get; set; }
       
    }
}
