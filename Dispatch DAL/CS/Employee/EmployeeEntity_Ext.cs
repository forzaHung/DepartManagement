using System;
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
        [Display(Name = "Lương thoả thuận")]
        [DisplayFormat(DataFormatString = "{0:N}")]
        public double WageAgreement { get; set; }
        [Display(Name = "Thử việc từ ngày")]
        public DateTime ProbationaryFromDate { get; set; }
        [Display(Name = "Thử việc đến ngày")]
        public DateTime ProbationaryToDate { get; set; }
        [Display(Name = "Ngày bắt đầu làm chính thức")]
        public DateTime WorkFromDate { get; set; }
        [Display(Name = "Ngày kết thúc làm")]
        public DateTime WorkToDate { get; set; }
    }
}
