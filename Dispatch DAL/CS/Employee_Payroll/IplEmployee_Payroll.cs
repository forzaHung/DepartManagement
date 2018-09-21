using Framework;
using Framework.Data;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatch
{
    public class IplEmployee_Payroll : BaseIpl<ADOProvider>, IEmployee_Payroll
    {
        public bool Insert(Employee_PayrollEntity entity) {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", entity.Id);
                p.Add("@DaysProbationarySalary", entity.DaysProbationarySalary);
                p.Add("@WageAgreement", entity.WageAgreement);
                p.Add("@IdEmployee", entity.IdEmployee);
                p.Add("@NamePayroll", entity.NamePayroll);
                p.Add("@StandardWorkDays", entity.StandardWorkDays);
                p.Add("@NumberDaysOfficialSalary", entity.NumberDaysOfficialSalary);
                p.Add("@TotalMonthlySalary", entity.TotalMonthlySalary);
                p.Add("@PremiumSalary", entity.PremiumSalary);
                p.Add("@CompulsoryInsurance", entity.CompulsoryInsurance);
                p.Add("@UnionFees", entity.UnionFees);
                p.Add("@CreateDate", entity.CreateDate);
                p.Add("@ToDate", entity.ToDate);
                p.Add("@FromDate", entity.FromDate);
                var data = unitOfWork.ProcedureExecute("ptgroup_Employee_Payroll_Insert", p);
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public bool Update(Employee_PayrollEntity entity)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", entity.Id);
                p.Add("@DaysProbationarySalary", entity.DaysProbationarySalary);
                p.Add("@WageAgreement", entity.WageAgreement);
                p.Add("@IdEmployee", entity.IdEmployee);
                p.Add("@NamePayroll", entity.NamePayroll);
                p.Add("@StandardWorkDays", entity.StandardWorkDays);
                p.Add("@NumberDaysOfficialSalary", entity.NumberDaysOfficialSalary);
                p.Add("@TotalMonthlySalary", entity.TotalMonthlySalary);
                p.Add("@PremiumSalary", entity.PremiumSalary);
                p.Add("@CompulsoryInsurance", entity.CompulsoryInsurance);
                p.Add("@UnionFees", entity.UnionFees);
                p.Add("@CreateDate", entity.CreateDate);
                p.Add("@ToDate", entity.ToDate);
                p.Add("@FromDate", entity.FromDate);
                var data = unitOfWork.ProcedureExecute("ptgroup_Employee_Payroll_Update", p);
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public bool Delete(int Id) {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", Id);
                var flag = (bool)unitOfWork.ProcedureExecute("ptgroup_Employee_Payroll_Delete", p);
                return flag;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public List<Employee_PayrollEntity> ListByPaging(DateTime fromDate, DateTime toDate, int employeeId, int PageIndex, int PageSize, ref int totalRow) {
            try
            {
                var p = new DynamicParameters();
                p.Add("@fromDate", fromDate);
                p.Add("@toDate", toDate);
                p.Add("@employeeId", employeeId);
                p.Add("@pageIndex", PageIndex);
                p.Add("@pageSize", PageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<Employee_PayrollEntity>("ptgroup_Employee_Payroll_ListByPaging", p);
                totalRow = p.Get<int>("@totalRow");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public List<Employee_PayrollEntity> ListByMonthPaging(DateTime fromDate, DateTime toDate, int employeeId, int PageIndex, int PageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@fromMonth", fromDate.Month);
                p.Add("@toMonth", toDate.Month);
                p.Add("@employeeId", employeeId);
                p.Add("@pageIndex", PageIndex);
                p.Add("@pageSize", PageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<Employee_PayrollEntity>("ptgroup_Employee_Payroll_ListByMonthPaging", p);
                totalRow = p.Get<int>("@totalRow");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public List<Employee_Payroll_ConfigEntity> ListConfigByPaging(int employeeId, int PageIndex, int PageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@employeeId", employeeId);
                p.Add("@pageIndex", PageIndex);
                p.Add("@pageSize", PageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<Employee_Payroll_ConfigEntity>("ptgroup_Employee_Payroll_ListConfigByPaging", p);
                totalRow = p.Get<int>("@totalRow");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public List<Employee_PayrollEntity> CheckPayroll(DateTime fromDate, DateTime toDate, int employeeId)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@fromDate", fromDate);
                p.Add("@toDate", toDate);
                p.Add("@employeeId", employeeId);
                var data = unitOfWork.Procedure<Employee_PayrollEntity>("ptgroup_Employee_Payroll_CheckPayroll", p);
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public Employee_Payroll_ConfigEntity CheckDetail(int IdEmployee) {
            try
            {
                var p = new DynamicParameters();
                p.Add("@IdEmployee", IdEmployee);
                var data = unitOfWork.Procedure<Employee_Payroll_ConfigEntity>("ptgroup_Employee_Payroll_CheckDetail", p);
                return data.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public bool InsertConfig(int IdEmployee){
            try
            {
                var p = new DynamicParameters();
                p.Add("@IdEmployee", IdEmployee);
                var flag = (bool)unitOfWork.ProcedureExecute("ptgroup_Employee_Payroll_InsertConfig",p);
                return flag;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public bool UpdateWageAgreement(int Id, decimal wageAgreement)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", Id);
                p.Add("@wageAgreement", wageAgreement);
                var flag = (bool)unitOfWork.ProcedureExecute("ptgroup_Employee_Update_WageAgreement", p);
                return flag;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public bool UpdateProbationaryFromDate(int Id, DateTime probationaryFromDate)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", Id);
                p.Add("@probationaryFromDate", probationaryFromDate);
                var flag = (bool)unitOfWork.ProcedureExecute("ptgroup_Employee_Update_ProbationaryFromDate", p);
                return flag;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public bool UpdateProbationaryToDate(int Id, DateTime probationaryToDate)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", Id);
                p.Add("@probationaryToDate", probationaryToDate);
                var flag = (bool)unitOfWork.ProcedureExecute("ptgroup_Employee_Update_ProbationaryToDate", p);
                return flag;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
    }
}
