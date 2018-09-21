using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatch
{
    public interface IEmployee_Payroll
    {
        bool Insert(Employee_PayrollEntity entity);
        bool Update(Employee_PayrollEntity entity);
        bool Delete(int Id);
        List<Employee_PayrollEntity> ListByPaging(DateTime fromDate, DateTime toDate, int employeeId, int PageIndex, int PageSize, ref int totalRow);
        List<Employee_Payroll_ConfigEntity> ListConfigByPaging(int employeeId, int PageIndex, int PageSize, ref int totalRow);
        List<Employee_PayrollEntity> CheckPayroll(DateTime fromDate, DateTime toDate, int employeeId);
        Employee_Payroll_ConfigEntity CheckDetail(int IdEmployee);
        bool UpdateWageAgreement(int Id, decimal wageAgreement);
        bool UpdateProbationaryFromDate(int Id, DateTime probationaryFromDate);
        bool UpdateProbationaryToDate(int Id, DateTime probationaryToDate);
        List<Employee_PayrollEntity> ListByMonthPaging(DateTime fromDate, DateTime toDate, int employeeId, int PageIndex, int PageSize, ref int totalRow);
    }
}
