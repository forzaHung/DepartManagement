using Framework;
using Framework.Data;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dispatch
{
    #region IplTransactionLogs
    /// <summary>
    /// This object represents the properties and methods of a TransactionLogs.
    /// </summary>
    public class IplTransactionLogs : BaseIpl<ADOProvider>, ITransactionLogs
    {
        public int Insert(TransactionLogsEntity transactionlogs)
        {
            int res = 0;
            bool flag = false;
            try
            {
                var p = Param(transactionlogs);
                flag = (bool)unitOfWork.ProcedureExecute("ptgroup_InsertTransactionLog", p);
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

        public bool Update(TransactionLogsEntity transactionlogs)
        {
            bool res = false;
            try
            {
                var p = Param(transactionlogs, "edit");
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_TransactionLogs_Update", p);
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
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_TransactionLogs_Delete", p);
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
        public TransactionLogsEntity ViewDetail(int id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);
                var data = unitOfWork.Procedure<TransactionLogsEntity>("ptgroup_TransactionLogs_ViewDetail", p).SingleOrDefault();
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
        public List<TransactionLogsEntity> ListAll()
        {
            try
            {
                var data = unitOfWork.Procedure<TransactionLogsEntity>("ptgroup_TransactionLogs_ListAll");
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
        public List<TransactionLogsEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<TransactionLogsEntity>("ptgroup_TransactionLogs_ListAllPaging", p);
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
        /// Lists all paging.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalRow">The total row.</param>
        /// <returns>List&lt;EmployeeEntity&gt;.</returns>
        public List<TransactionLogsEntity> ListAllPaging(string searchString, int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@searchString", searchString);
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<TransactionLogsEntity>("ptgroup_TransactionLogs_ListAllPaging", p);
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
        private DynamicParameters Param(TransactionLogsEntity transactionlogs, string action = "add")
        {
            var p = new DynamicParameters();
            if (action == "add")
            {
                p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            }
            else
            {
                p.Add("@Id", transactionlogs.Id);
            }
            p.Add("@UserId", transactionlogs.UserId);
            p.Add("@Action", transactionlogs.Action);
            p.Add("@Link", transactionlogs.Link);
            p.Add("@Description", transactionlogs.Description);
            p.Add("@LogDate", transactionlogs.LogDate);

            return p;
        }






    }
    #endregion
}

