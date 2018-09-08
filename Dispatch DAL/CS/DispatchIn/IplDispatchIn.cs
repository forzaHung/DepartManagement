using Framework;
using Framework.Data;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dispatch
{
    #region IplDispatchIn
    /// <summary>
    /// This object represents the properties and methods of a DispatchIn.
    /// </summary>
    public class IplDispatchIn : BaseIpl<ADOProvider>, IDispatchIn
    {
        public int Insert(DispatchInEntity dispatchin)
        {
            int res = 0;
            bool flag = false;
            try
            {
                var p = Param(dispatchin);
                flag = (bool)unitOfWork.ProcedureExecute("ptgroup_DispatchIn_Insert", p);
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
        public bool Update(DispatchInEntity dispatchin)
        {
            bool res = false;
            try
            {
                var p = Param(dispatchin, "edit");
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_DispatchIn_Update", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return res;
            }
        }
        public bool UpdateFileId(DispatchInEntity dispatchin)
        {
            bool res = false;
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", dispatchin.Id);
                p.Add("@FileId", dispatchin.FileId);
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_DispatchIn_UpdateFileId", p);
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
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_DispatchIn_Delete", p);
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
        public DispatchInEntity ViewDetail(int id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);
                var data = unitOfWork.Procedure<DispatchInEntity>("ptgroup_DispatchIn_ViewDetail", p).SingleOrDefault();
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
        public List<DispatchInEntity> ListAll()
        {
            try
            {
                var data = unitOfWork.Procedure<DispatchInEntity>("ptgroup_DispatchIn_ListAll");
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
        public List<DispatchInEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<DispatchInEntity>("ptgroup_DispatchIn_ListAllPaging", p);
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
        public List<DispatchInEntity> ListAllPaging(string searchCode, string searchName, int searchType, int searchDepartment,
            int searchStatus, int Priority, string searchDateFrom, int AddressToId, int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@searchCode", searchCode);
                p.Add("@searchName", searchName);
                p.Add("@searchType", searchType);
                p.Add("@searchDepartment", searchDepartment);
                p.Add("@searchStatus", searchStatus);
                p.Add("@Priority", Priority);
                p.Add("@searchDateFrom", searchDateFrom);
                p.Add("@AddressToId", AddressToId);
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<DispatchInEntity>("ptgroup_DispatchIn_ListAllPaging", p);
                totalRow = p.Get<int>("@totalRow");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public int CountDispatchIn(int searchType,int searchStatus, int AddressToId)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@searchType", searchType);               
                p.Add("@searchStatus", searchStatus);
                p.Add("@AddressToId", AddressToId);
                var data = unitOfWork.Procedure<DispatchInEntity>("ptgroup_DispatchIn_Count", p);
                var count = data.Count();
                return count;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return 0;
            }
        }
        /// <summary>
        /// Saves a record to the Employee table.
        /// </summary>
        private DynamicParameters Param(DispatchInEntity dispatchin, string action = "add")
        {
            var p = new DynamicParameters();
            if (action == "add")
            {
                p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@CreatedBy", dispatchin.CreatedBy);
                p.Add("@CreatedDate", dispatchin.CreatedDate);
            }
            else
            {
                p.Add("@Id", dispatchin.Id);
                p.Add("@ModifiedBy", dispatchin.ModifiedBy);
                p.Add("@ModifiedDate", dispatchin.ModifiedDate);
            }
            p.Add("@DispatchNo", dispatchin.DispatchNo);
            p.Add("@DispatchName", dispatchin.DispatchName);
            p.Add("@DispatchType", dispatchin.DispatchType);
            p.Add("@Priority", dispatchin.Priority);
            p.Add("@DateFrom", dispatchin.DateFrom);
            p.Add("@DatePublish", dispatchin.DatePublish);
            p.Add("@Signer", dispatchin.Signer);
            p.Add("@DispatchStatus", dispatchin.DispatchStatus);
            p.Add("@AddressFromId", dispatchin.AddressFromId);
            p.Add("@AddressToId", dispatchin.AddressToId);
            p.Add("@ReceiverId", dispatchin.ReceiverId);
            p.Add("@ChiefOfStaffId", dispatchin.ChiefOfStaffId);
            p.Add("@Note", dispatchin.Note);
            p.Add("@FileId", dispatchin.FileId);
            p.Add("@Description", dispatchin.Description);
            return p;
        }
    }
    #endregion
}