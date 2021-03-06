﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatch
{
    public partial class RegulationTimeEntity
    {
        public int Id { get; set; }
        public int IdDepartment { get; set; }
        public string Department { get; set; }
        public int IdPosition { get; set; }
        public string Position { get; set; }
        public int AllowedLate { get; set; }
        public int AllowedEarly { get; set; }
        public int TimesLate { get; set; }
    }
}
