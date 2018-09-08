using System.ComponentModel.DataAnnotations;

namespace Dispatch
{
    public class DispatchTypeMetadata
    {
        [Display(Name = "Loại công văn")]
        [Required(ErrorMessage = "Chọn loại công văn")]
        [Range(1, int.MaxValue, ErrorMessage = "Chọn loại công văn")]
        public int Name { get; set; }
       
    }
}
