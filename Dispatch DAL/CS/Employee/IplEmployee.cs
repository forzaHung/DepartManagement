using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Framework;
using Framework.Data;
using Framework.Helper.Logging;

namespace Dispatch
{
    public class IplEmployee : BaseIpl<ADOProvider>, IEmployee
    {
        #region Methods
        /// <summary>
        /// Saves a record to the Employee table.
        /// </summary>
        public int Insert(EmployeeEntity employee) {
            int res = 0;
            bool flag = false;
            try
            {
                var p = Param(employee);
                flag = (bool)unitOfWork.ProcedureExecute("ptgroupEmployee_Insert", p);
                if (flag)
                {
                    res = p.Get<int>("@Id");
                }
                else
                {
                    res = 0;
                }
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
            }
            return res;
        }
        /// <summary>
        /// Updates a record in the Employee table.
        /// </summary>
        public bool Update(EmployeeEntity employee)
        {
            bool res = false;
            try
            {
                var p = Param(employee, "edit");
                res = (bool)unitOfWork.ProcedureExecute("ptgroupEmployee_Update", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return res;
            }
        }
        public bool InsertDepartment(Employee_Department employeeDepartment)
        {
            bool flag = false;
            try
            {
                var p = new DynamicParameters();
                p.Add("@IdEmployee", employeeDepartment.IdEmployee);
                p.Add("@IdDepartment", employeeDepartment.IdDepartment);
                p.Add("@IdPosition", employeeDepartment.IdPosition);
                flag = (bool)unitOfWork.ProcedureExecute("ptgroupEmployee_Department_Insert", p);
                return flag;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public bool InsertDispatchWork(Employee_DispatchWork employeeDispatchWor)
        {
            bool flag = false;
            try
            {
                var p = new DynamicParameters();
                p.Add("@IdEmployee", employeeDispatchWor.IdEmployee);
                p.Add("@IdDispatchWork", employeeDispatchWor.IdDispatchWork);
                flag = (bool)unitOfWork.ProcedureExecute("ptgroupEmployee_DispatchWor_Insert", p);
                return flag;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public bool DeleteDepartment(int id)
        {
            try
            {
                bool res = false;
                var p = new DynamicParameters();
                p.Add("@Id", id);
                res = (bool)unitOfWork.ProcedureExecute("ptgroupEmployee_Department_Delete", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public bool DeleteWork(int id)
        {
            try
            {
                bool res = false;
                var p = new DynamicParameters();
                p.Add("@Id", id);
                res = (bool)unitOfWork.ProcedureExecute("ptgroupEmployee_DispatchWork_Delete", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public List<Employee_Department> ListDetail(int Id){
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", Id);
                var Data = unitOfWork.Procedure<Employee_Department>("ptgroupEmployee_Department_ListAllById",p).ToList();
                return Data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        /// <summary>
        /// Deletes a record from the Employee table by its primary key.
        /// </summary>
        public bool Delete(int id)
		{
			try
			{
				bool res = false;
				var p = new DynamicParameters();
				p.Add("@Id", id);
				res = (bool)unitOfWork.ProcedureExecute("ptgroupEmployee_Delete", p);
				return res;
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				return false;
			}
		}
		/// <summary>
		/// Selects a single record from the Employee table.
		/// </summary>
		public EmployeeEntity ViewDetail(int id)
		{
			try
			{
				var p = new DynamicParameters();
				p.Add("@Id", id);
				var data = unitOfWork.Procedure<EmployeeEntity>("ptgroup_Employee_ViewDetail", p).SingleOrDefault();
				return data;
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				return null;
			}
		}
		/// <summary>
		/// Selects all records from the Employee table.
		/// </summary>
		public List<EmployeeEntity> ListAll()
		{
			try
			{
				var data = unitOfWork.Procedure<EmployeeEntity>("ptgroupEmployee_ListAll");
				return data.ToList();
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				return null;
			}
		}
        public List<EmployeeEntity> ListAllByPostion(int Position, int IdDepartment)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Position", Position);
                p.Add("@IdDepartment", IdDepartment);
                var data = unitOfWork.Procedure<EmployeeEntity>("ptgroupEmployee_ListAllByPosition", p);
            
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        /// <summary>
        /// Selects all records from the Employee table.
        /// </summary>
        public List<EmployeeEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow)
		{
			try
			{
				var p = new DynamicParameters();
				p.Add("@pageIndex", pageIndex);
				p.Add("@pageSize", pageSize);
				p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
				var data = unitOfWork.Procedure<EmployeeEntity>("ptgroupEmployee_ListAllPaging", p);
				totalRow = p.Get<int>("@totalRow");
				return data.ToList();
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				return null;
			}
		}
        /// <summary>
        /// Lists all paging.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalRow">The total row.</param>
        /// <returns>List&lt;EmployeeEntity&gt;.</returns>
        public List<EmployeeEntity> ListAllPaging(string searchString, int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@searchString", searchString);
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<EmployeeEntity>("ptgroupEmployee_ListAllPaging", p);
                totalRow = p.Get<int>("@totalRow");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        /// <summary>
        /// Saves a record to the Employee table.
        /// </summary>
        private DynamicParameters Param(EmployeeEntity employee, string action = "add")
		{
            var p = new DynamicParameters();
            if (action == "add")
            {
                p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@CreatedDate", DateTime.Now);
            }
            else
            {
                p.Add("@Id", employee.Id);
                p.Add("@ModifiedDate", DateTime.Now);
            }
            p.Add("@DepartmentId", employee.DepartmentId);
            p.Add("@FirstName", employee.FirstName);
            p.Add("@LastName", employee.LastName);
            p.Add("@Address", employee.Address);
            p.Add("@Phone", employee.Phone);
            p.Add("@AcademicLevel", employee.AcademicLevel);
            p.Add("@PicturePath", employee.PicturePath);
            p.Add("@IsActive", employee.IsActive);
            p.Add("@IsDel", employee.IsDel); //thêm trường isdel còn thiếu 
            p.Add("@LastDepartment", employee.LastDepartment);
            p.Add("@Position", employee.Position);
            p.Add("@Birthday", employee.Birthday);
            p.Add("@Gender", employee.Gender);
            p.Add("@WageAgreement", employee.WageAgreement);
            p.Add("@ProbationaryFromDate", employee.ProbationaryFromDate);
            p.Add("@ProbationaryToDate", employee.ProbationaryToDate);
            p.Add("@WorkFromDate", employee.WorkFromDate);
            p.Add("@WorkToDate", employee.WorkToDate);
            return p;
        }
        public List<EmployeeEntity> ListByName (string name)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Name", name);
                var data = unitOfWork.Procedure<EmployeeEntity>("ptgroupEmployee_ListByName", p);
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public List<EmployeeEntity> ListByIdDepart(int Id) {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", Id);
                var data = unitOfWork.Procedure<EmployeeEntity>("ptgroupEmployee_ListByIdDepart", p);
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
		#endregion
	}
}
