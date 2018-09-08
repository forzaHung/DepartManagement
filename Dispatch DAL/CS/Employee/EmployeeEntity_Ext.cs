using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Dispatch
{
    public partial class EmployeeEntity
    {
        [Display(Name = "Upload File")]
        public HttpPostedFileBase file { get; set; }
        public string UserName { get; set; }
        //public string DepartmentName { get; set; }
        public int UserId { get; set; }
        // add name department
        public string DepartmentName { get; set; }
    }
}
