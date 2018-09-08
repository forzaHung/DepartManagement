using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DispatchManagement.Models
{
    public partial class CountList
    {
        public int returnCount { get; set; }
        public int AddressToId { get; set; }
        public int searchType { get; set; }
        public int searchStatus { get; set; }
        public int DepartmentId { get; set; }
    }
}