using System;
using System.ComponentModel.DataAnnotations;

namespace Dispatch
{
    public partial class DispatchWorkEntity
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Nhập loại công việc")]
        [StringLength(50)]
        public string WorkType { get; set; }
        [Required(ErrorMessage = "Nhập tên công việc")]
        [StringLength(50)]
        public string WorkName { get; set; }
        [Required(ErrorMessage = "Nhập mã công việc")]
        public int WorkCode { get; set; }
        [Required(ErrorMessage = "Nhập hệ só lương")]
        [StringLength(50)]
        public string CoefficientsSalary { get; set; }
        public int ParentID { get; set; }
        public string Title { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}
