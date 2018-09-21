using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatch
{
    public class RoomEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nhập tên phòng họp")]
        public string Name { get; set; }
        public bool isDel { get; set; }
        public bool isActive { get; set; }
    }
}
