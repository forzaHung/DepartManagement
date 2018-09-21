using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatch
{
    public class RoomMetadata
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Tên phòng họp ")]
        public string Name { get; set; }

        public bool isDel { get; set; }

        public bool isActive { get; set; }
    }
}
