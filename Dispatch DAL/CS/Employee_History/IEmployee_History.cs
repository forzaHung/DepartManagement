using System;
using System.Collections.Generic;

namespace Dispatch
{
	public interface IEmployee_History
    {
		int Insert(Employee_HistoryEntity employee_history);
        bool Update(Employee_HistoryEntity employee_history);
		bool Delete(int id);
		List<Employee_HistoryEntity> ListAll();
        Employee_HistoryEntity SelectByPositionDepartment(int EmployeeId, int Position, int DepartmentId);
        List<Employee_HistoryEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow);
        List<Employee_HistoryEntity> ListAllPaging(string searchString, int pageIndex, int pageSize, ref int totalRow);
        List<Employee_HistoryEntity> ListAllTimeOutNull(int IdEmployee);
        Employee_HistoryEntity ViewDetail(int id);
	}
}
