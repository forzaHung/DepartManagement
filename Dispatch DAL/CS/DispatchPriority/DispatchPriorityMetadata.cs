using System.ComponentModel.DataAnnotations;

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
