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
    #region IplProject
    /// <summary>
    /// This object represents the properties and methods of a Project.
    /// </summary>
    public class IplProject : BaseIpl<ADOProvider>, IProject
    {
        public int Insert(ProjectEntity project)
        {
            int res = 0;
            bool flag = false;
            try
            {
                var p = Param(project);
                flag = (bool)unitOfWork.ProcedureExecute("ptgroup_Project_Insert", p);
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

        public int Update(ProjectEntity project)
        {
            int res = 0;
            try
            {
                var p = Param(project, "edit");
                var flag = (bool)unitOfWork.ProcedureExecute("ptgroup_Project_Update", p);
                if (flag)
                {
                    res =int.Parse(project.Id.ToString());
                }
                else
                {
                    res = 0;
                }
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }

        ///Delete

        public bool Delete(long id)
        {
            try
            {
                bool res = false;
                var p = new DynamicParameters();
                p.Add("@Id", id);
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_Project_Delete", p);
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
        public ProjectEntity ViewDetail(long id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);
                var data = unitOfWork.Procedure<ProjectEntity>("ptgroup_Project_ViewDetail", p).SingleOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }

        }

        public ProjectEntity ViewDetailWithCustomer(int id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);
                var data = unitOfWork.Procedure<ProjectEntity>("ptgroup_Project_ViewDetailWith_Customer", p).SingleOrDefault();
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
        public List<ProjectEntity> ListAll()
        {
            try
            {
                var data = unitOfWork.Procedure<ProjectEntity>("ptgroup_Project_ListAll");
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
        public List<ProjectEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<ProjectEntity>("ptgroup_Project_ListAllPaging", p);
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
        public List<ProjectEntity> ListAllPaging(string searchString, int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@searchString", searchString);
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<ProjectEntity>("ptgroup_Project_ListAllPaging", p);
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
        private DynamicParameters Param(ProjectEntity project, string action = "add")
        {
            var p = new DynamicParameters();
            if (action == "add")
            {
                p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@CreateDate", project.CreateDate);
            }
            else
            {
                p.Add("@Id", project.Id);
                p.Add("@ModifiedDate", project.ModifiedDate);
            }
            p.Add("@Name", project.Name);
            p.Add("@CustomerId", project.CustomerId);
            p.Add("@Price", project.Price);
            p.Add("@SalesId", project.SalesId);
            p.Add("@Description", project.Description);
           
         
            p.Add("@IsDel", project.IsDel);
            p.Add("@StatusId", project.StatusId);
            p.Add("@TypeId", project.TypeId);
            p.Add("@ListManager", project.ListManager);
            p.Add("@ListTechnical", project.ListTechnical);
            p.Add("@ListAssistant", project.ListAssistant);
            p.Add("@ListBuyer", project.ListBuyer);

            return p;
        }






    }
    #endregion
}

