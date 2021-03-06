﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
