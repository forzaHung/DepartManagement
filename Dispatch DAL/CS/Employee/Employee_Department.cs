namespace Dispatch
{
    public partial class Employee_Department
    {
        public int IdEmployee { get; set; }
        public int IdDepartment { get; set; }
        public int IdPosition { get; set; }
        public string NamePosition { get; set; }

        // Name Department
        public string NameDepartment { get; set; }
    }
    public partial class Employee_History
    {
        public int IdDepartment { get; set; }
        public string NameDepartment { get; set; }
        public string Note { get; set; }
    }
}
