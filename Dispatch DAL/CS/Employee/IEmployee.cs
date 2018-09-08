using System.Collections.Generic;

namespace Dispatch
{
    public interface IEmployee
	{
		int Insert(EmployeeEntity employee);		bool Update(EmployeeEntity employee);
		bool Delete(int id);
		List<EmployeeEntity> ListAll();
        List<EmployeeEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow);
        List<EmployeeEntity> ListAllPaging(string searchString, int pageIndex, int pageSize, ref int totalRow);
        EmployeeEntity ViewDetail(int id);
        bool InsertDepartment(Employee_Department employeeDepartment);
        bool InsertDispatchWork(Employee_DispatchWork employeeDispatchWor);
        bool DeleteDepartment(int id);
        bool DeleteWork(int id);
        List<Employee_Department> ListDetail(int Id);
        List<EmployeeEntity> ListAllByPostion(int Position, int IdDepartment);
        List<EmployeeEntity> ListByName(string name);
    }
}
