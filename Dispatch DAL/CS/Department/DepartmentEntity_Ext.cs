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

        // add so luong nhan vien real va so nhan vien du kien

        public int SoNhanVien { get; set; }

        public int SumEmployeesExpected { get; set; }
    }
}
