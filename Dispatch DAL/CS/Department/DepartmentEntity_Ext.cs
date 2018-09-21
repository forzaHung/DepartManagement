using System.ComponentModel.DataAnnotations;

namespace Dispatch
{
    public partial class DepartmentEntity
    {

        public int EmployeeManagerId { get; set; }

        public int EmployeeDebutyManagerId { get; set; }
        public int EmployeeChiefOfTheOfficeId { get; set; }
        public bool isActive { get; set; }
        public bool isDel { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; }

        public int CountEmployee { get; set; }

        [Display(Name = "Số nhân viên dự kiến")]
        [Range(0, 100, ErrorMessage = "Nhập số nhân viên trong khoảng từ 1 đến 100")]
        public int SumEmployeesExpected { get; set; }
    }
}
