using System.ComponentModel.DataAnnotations;

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
