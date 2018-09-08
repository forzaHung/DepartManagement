using Framework;
using Framework.Data;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dispatch
{
    #region IplDispatchOut
    /// <summary>
    /// This object represents the properties and methods of a DispatchOut.
    /// </summary>
    public class IplDispatchOut : BaseIpl<ADOProvider>, IDispatchOut
    {
        public int Insert(DispatchOutEntity dispatchout)
        {
            int res = 0;
            bool flag = false;
            try
            {
                var p = Param(dispatchout);
                flag = (bool)unitOfWork.ProcedureExecute("ptgroup_DispatchOut_Insert", p);
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


        ///Update

        public bool Update(DispatchOutEntity dispatchout)
        {
            bool res = false;
            try
            {
                var p = Param(dispatchout, "edit");
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_DispatchOut_Update", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }

        public bool UpdateFileId(DispatchOutEntity dispatchout)
        {
            bool res = false;
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", dispatchout.Id);
                p.Add("@FileId", dispatchout.FileId);
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_DispatchOut_UpdateFileId", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
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
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_DispatchOut_Delete", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }
        /// <summary>
        /// Selects a single record from the table.
        /// </summary>
        public DispatchOutEntity ViewDetail(int id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);
                var data = unitOfWork.Procedure<DispatchOutEntity>("ptgroup_DispatchOut_ViewDetail", p).SingleOrDefault();
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
        public List<DispatchOutEntity> ListAll()
        {
            try
            {
                var data = unitOfWork.Procedure<DispatchOutEntity>("ptgroup_DispatchOut_ListAll");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }

        /// <summary>
        /// Selects all records from the table.
        /// </summary>
        public List<DispatchOutEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<DispatchOutEntity>("ptgroup_DispatchOut_ListAllPaging", p);
                totalRow = p.Get<int>("@totalRow");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }

        public int CountDispatchOut(int searchType, int searchStatus, int DepartmentId)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@searchType", searchType);
                p.Add("@searchStatus", searchStatus);
                p.Add("@@DepartmentId", DepartmentId);


                var data = unitOfWork.Procedure<DispatchOutEntity>("ptgroup_DispatchOut_Count", p);
                var count = data.Count();
                return count;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
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
        public List<DispatchOutEntity> ListAllPaging(string searchCode, string searchName, int searchType, int searchDepartment,
            int searchStatus, int Priority, string searchDateWrite, int DepartmentId, int pageIndex, int pageSize, ref int totalRow)
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
                p.Add("@searchDateWrite", searchDateWrite);
                p.Add("@DepartmentId", DepartmentId);
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<DispatchOutEntity>("ptgroup_DispatchOut_ListAllPaging", p);
                totalRow = p.Get<int>("@totalRow");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }

        /// <summary>
        /// Saves a record to the Employee table.
        /// </summary>
        private DynamicParameters Param(DispatchOutEntity dispatchout, string action = "add")
        {
            var p = new DynamicParameters();
            if (action == "add")
            {
                p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@CreateBy", dispatchout.CreateBy);
                p.Add("@CreatedDate", dispatchout.CreatedDate);
            }
            else
            {
                p.Add("@Id", dispatchout.Id);
                p.Add("@ModifiedBy", dispatchout.ModifiedBy);
                p.Add("@ModifiedDate", dispatchout.ModifiedDate);
            }
            p.Add("@DispatchNo", dispatchout.DispatchNo);
            p.Add("@DispatchName", dispatchout.DispatchName);
            p.Add("@DispatchType", dispatchout.DispatchType);
            p.Add("@Priority", dispatchout.Priority);
            p.Add("@DateWrite", dispatchout.DateWrite);
            p.Add("@DispatchStatus", dispatchout.DispatchStatus);
            p.Add("@WriterId", dispatchout.WriterId);
            p.Add("@DepartmentId", dispatchout.DepartmentId);
            p.Add("@ApproverId", dispatchout.ApproverId);
            p.Add("@ChiefOfStaffId", dispatchout.ChiefOfStaffId);
            p.Add("@Note", dispatchout.Note);
            p.Add("@FileId", dispatchout.FileId);
            p.Add("@Description", dispatchout.Description);



            return p;
        }






    }
    #endregion
}

