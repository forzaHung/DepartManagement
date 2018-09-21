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
    #region IplDispatchPriority
    /// <summary>
    /// This object represents the properties and methods of a DispatchPriority.
    /// </summary>
    public class IplDispatchPriority : BaseIpl<ADOProvider>, IDispatchPriority
    {
        public int Insert(DispatchPriorityEntity dispatchpriority)
        {
            int res = 0;
            bool flag = false;
            try
            {
                var p = Param(dispatchpriority);
                flag = (bool)unitOfWork.ProcedureExecute("ptgroup_DispatchPriority_Insert", p);
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
        ///Update
        public bool Update(DispatchPriorityEntity dispatchpriority)
        {
            bool res = false;
            try
            {
                var p = Param(dispatchpriority, "edit");
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_DispatchPriority_Update", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return res;
            }
        }
        ///Delete
        public bool Delete(int id)
        {
            try
            {
                bool res = false;
                var p = new DynamicParameters();
                p.Add("@Id", id);
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_DispatchPriority_Delete", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        /// <summary>
        /// Selects a single record from the table.
        /// </summary>
        public DispatchPriorityEntity ViewDetail(int id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);
                var data = unitOfWork.Procedure<DispatchPriorityEntity>("ptgroup_DispatchPriority_ViewDetail", p).SingleOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        /// <summary>
        /// Selects all records from the table.
        /// </summary>
        public List<DispatchPriorityEntity> ListAll()
        {
            try
            {
                var data = unitOfWork.Procedure<DispatchPriorityEntity>("ptgroup_DispatchPriority_ListAll");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        /// <summary>
        /// Selects all records from the table.
        /// </summary>
        public List<DispatchPriorityEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<DispatchPriorityEntity>("ptgroup_DispatchPriority_ListAllPaging", p);
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
        public List<DispatchPriorityEntity> ListAllPaging(string searchString, int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@searchString", searchString);
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<DispatchPriorityEntity>("ptgroup_DispatchPriority_ListAllPaging", p);
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
        private DynamicParameters Param(DispatchPriorityEntity dispatchpriority, string action = "add")
        {
            var p = new DynamicParameters();
            if (action == "add")
            {
                p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            }
            else
            {
                p.Add("@Id", dispatchpriority.Id);
            }
            p.Add("@Name", dispatchpriority.Name);
            p.Add("@CreateDate", dispatchpriority.CreateDate);
            return p;
        }
    }
    #endregion
}

