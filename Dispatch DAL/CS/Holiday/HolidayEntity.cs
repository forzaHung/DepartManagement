using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatch
{
    public partial class HolidayEntity
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public DateTime HolidayDate { get; set; }
    }
}
