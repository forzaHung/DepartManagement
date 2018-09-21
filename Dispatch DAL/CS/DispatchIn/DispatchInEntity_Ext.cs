using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Dispatch
{
   public partial class DispatchInEntity
    {       
        public string FromDepartment { get; set; }
        public string ToDepartment { get; set; }
        public string DispatchSatusName { get; set; }
        public string DispatchTypeName { get; set; }
        public string FileName { get; set; }
        public string PriorityName { get; set; }
        public string EmployFirtName { get; set; }
        public string EmployLastName { get; set; }
       
        public HttpPostedFileBase File { get; set; }
        public bool IsDel { get; set; }
    }
}
