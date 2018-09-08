using Framework;
using Framework.Data;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dispatch
{
    public class IplEmployee_History : BaseIpl<ADOProvider>, IEmployee_History
    {


        #region Methods

        /// <summary>
        /// Saves a record to the Employee table.
        /// </summary>
        public int Insert(Employee_HistoryEntity employee_history)
        {
            int res = 0;
            bool flag = false;
            try
            {
                var p = Param(employee_history);
                flag = (bool)unitOfWork.ProcedureExecute("ptgroup_Employee_History_Insert", p);
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
                throw;
            }
            return res;
        }



        /// <summary>
        /// Updates a record in the Employee table.
        /// </summary>
        public bool Update(Employee_HistoryEntity employee_history)
        {
            bool res = false;
            try
            {
                var p = Param(employee_history, "edit");
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_Employee_History_Update", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return res;
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
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_Employee_History_Delete", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }

        /// <summary>
        /// Selects a single record from the Employee table.
        /// </summary>
        public Employee_HistoryEntity ViewDetail(int id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);
                var data = unitOfWork.Procedure<Employee_HistoryEntity>("ptgroup_Employee_History_ViewDetail", p).SingleOrDefault();
                return data;
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
        public List<Employee_HistoryEntity> ListAll()
        {
            try
            {
                var data = unitOfWork.Procedure<Employee_HistoryEntity>("ptgroup_Employee_History_ListAll");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public Employee_HistoryEntity SelectByPositionDepartment(int EmployeeId, int Position, int DepartmentId)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@EmployeeId", EmployeeId);
                p.Add("@Position", Position);
                p.Add("@DepartmentId", DepartmentId);

                var data = unitOfWork.Procedure<Employee_HistoryEntity>("ptgroup_Employee_History_ListAllByPositionDepartment", p).FirstOrDefault();
                return data;
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
        public List<Employee_HistoryEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<Employee_HistoryEntity>("ptgroup_Employee_History_ListAllPaging", p);
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
        /// Lists all paging.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalRow">The total row.</param>
        /// <returns>List&lt;EmployeeEntity&gt;.</returns>
        public List<Employee_HistoryEntity> ListAllPaging(string searchString, int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@searchString", searchString);
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<Employee_HistoryEntity>("ptgroup_Employee_History_ListAllPaging", p);
                totalRow = p.Get<int>("@totalRow");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public List<Employee_HistoryEntity> ListAllTimeOutNull(int IdEmployee)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@IdEmployee", IdEmployee);
                var data = unitOfWork.Procedure<Employee_HistoryEntity>("ptgroup_Employee_History_ListAllTimeOutNull", p);
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
        private DynamicParameters Param(Employee_HistoryEntity employee_history, string action = "add")
        {
            var p = new DynamicParameters();
            if (action == "add")
            {
                p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            }
            else
            {
                p.Add("@Id", employee_history.Id);
                p.Add("@TimeOut", employee_history.TimeOut);
            }
            p.Add("@EmployeeId", employee_history.EmployeeId);
            p.Add("@DepartmentId", employee_history.DepartmentId);
            p.Add("@Position", employee_history.Position);
            p.Add("@TimeIn", employee_history.TimeIn);
            p.Add("@Note",employee_history.Note);

            return p;
        }


        #endregion
    }
}
