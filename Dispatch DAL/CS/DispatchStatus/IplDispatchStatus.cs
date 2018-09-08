using Framework;
using Framework.Data;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dispatch
{
    #region IplDispatchStatus
    /// <summary>
    /// This object represents the properties and methods of a DispatchStatus.
    /// </summary>
    public class IplDispatchStatus : BaseIpl<ADOProvider>, IDispatchStatus
    {
        public int Insert(DispatchStatusEntity dispatchstatus)
        {
            int res = 0;
            bool flag = false;
            try
            {
                var p = Param(dispatchstatus);
                flag = (bool)unitOfWork.ProcedureExecute("ptgroup_DispatchStatus_Insert", p);
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
        public bool Update(DispatchStatusEntity dispatchstatus)
        {
            bool res = false;
            try
            {
                var p = Param(dispatchstatus, "edit");
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_DispatchStatus_Update", p);
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
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_DispatchStatus_Delete", p);
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
        public DispatchStatusEntity ViewDetail(int id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);
                var data = unitOfWork.Procedure<DispatchStatusEntity>("ptgroup_DispatchStatus_ViewDetail", p).SingleOrDefault();
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
        public List<DispatchStatusEntity> ListAll()
        {
            try
            {
                var data = unitOfWork.Procedure<DispatchStatusEntity>("ptgroup_DispatchStatus_ListAll");
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
        public List<DispatchStatusEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<DispatchStatusEntity>("ptgroup_DispatchStatus_ListAllPaging", p);
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
        public List<DispatchStatusEntity> ListAllPaging(string searchString, int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@searchString", searchString);
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<DispatchStatusEntity>("ptgroup_DispatchStatus_ListAllPaging", p);
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
        private DynamicParameters Param(DispatchStatusEntity dispatchstatus, string action = "add")
        {
            var p = new DynamicParameters();
            if (action == "add")
            {
                p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            }
            else
            {
                p.Add("@Id", dispatchstatus.Id);
            }
            p.Add("@Name", dispatchstatus.Name);
            p.Add("@CreateDate", dispatchstatus.CreateDate);
            return p;
        }
    }
    #endregion
}

