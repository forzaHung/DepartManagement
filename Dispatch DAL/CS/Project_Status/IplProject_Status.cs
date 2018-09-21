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
    #region IplProject_Status
    /// <summary>
    /// This object represents the properties and methods of a Project_Status.
    /// </summary>
    public class IplProject_Status : BaseIpl<ADOProvider>, IProject_Status
    {
        public int Insert(Project_StatusEntity project_status)
        {
            int res = 0;
            bool flag = false;
            try
            {
                var p = Param(project_status);
                flag = (bool)unitOfWork.ProcedureExecute("ptgroup_Project_Status_Insert", p);
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

        public bool Update(Project_StatusEntity project_status)
        {
            bool res = false;
            try
            {
                var p = Param(project_status, "edit");
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_Project_Status_Update", p);
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
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_Project_Status_Delete", p);
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
        public Project_StatusEntity ViewDetail(int id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);
                var data = unitOfWork.Procedure<Project_StatusEntity>("ptgroup_Project_Status_ViewDetail", p).SingleOrDefault();
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
        public List<Project_StatusEntity> ListAll()
        {
            try
            {
                var data = unitOfWork.Procedure<Project_StatusEntity>("ptgroup_Project_Status_ListAll");
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
        public List<Project_StatusEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<Project_StatusEntity>("ptgroup_Project_Status_ListAllPaging", p);
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
        public List<Project_StatusEntity> ListAllPaging(string searchString, int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@searchString", searchString);
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<Project_StatusEntity>("ptgroup_Project_Status_ListAllPaging", p);
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
        private DynamicParameters Param(Project_StatusEntity project_status, string action = "add")
        {
            var p = new DynamicParameters();
            if (action == "add")
            {
                p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            }
            else
            {
                p.Add("@Id", project_status.Id);
            }
            p.Add("@Name", project_status.Name);
            p.Add("@IsDel", project_status.IsDel);
            p.Add("@IsActive", project_status.IsActive);
            return p;
        }






    }
    #endregion
}

