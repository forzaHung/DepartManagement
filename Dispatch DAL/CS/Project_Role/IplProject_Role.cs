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
    public class IplProject_Role : BaseIpl<ADOProvider>, IProject_Role
    {


        #region Methods

        /// <summary>
        /// Saves a record to the Role table.
        /// </summary>
        public Int32 Insert(Project_RoleEntity role)
        {
            bool flag = false;
            try
            {
                var p = Param(role);
                flag = (bool)unitOfWork.ProcedureExecute("ptgroup_Project_Role_Insert", p);
                if (flag)
                    return 1;
                return 0;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
            return 0;
        }

        /// <summary>
        /// Updates a record in the Role table.
        /// </summary>
        public bool Update(Project_RoleEntity role)
        {
            bool res = false;
            try
            {
                var p = Param(role, "edit");
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_Project_Role_Update", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }
        /// <summary>
        /// Updates the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="moduleId">The module identifier.</param>
        /// <param name="action">The action.</param>
        /// <param name="access">if set to <c>true</c> [access].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Update(int userId, long ProjectId, string action, bool access)
        {
            bool res = false;
            try
            {
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                p.Add("@ProjectId", ProjectId);
                p.Add("@action", action);
                p.Add("@access", access);
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_Project_Role_UpdateDynamic", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }
        /// <summary>
        /// Deletes a record from the Role table by its primary key.
        /// </summary>
        public bool Delete(int userId, long ProjectId)
        {
            try
            {
                bool res = false;
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                p.Add("@ProjectId", ProjectId);
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_Project_Role_Delete", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }

        /// <summary>
        /// Selects a single record from the Role table.
        /// </summary>
        public Project_RoleEntity ViewDetail(int userId, long ProjectId)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                p.Add("@ProjectId", ProjectId);
                var data = unitOfWork.Procedure<Project_RoleEntity>("ptgroup_Project_Role_ViewDetail", p).SingleOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }

        }

        /// <summary>
        /// Selects all records from the Role table.
        /// </summary>
        public List<Project_RoleEntity> ListAll()
        {
            try
            {
                var data = unitOfWork.Procedure<Project_RoleEntity>("ptgroup_Project_Role_ListAll");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }
        /// <summary>
        /// Lists all.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List&lt;Project_RoleEntity&gt;.</returns>
        public List<Project_RoleEntity> ListAllByUser(int userId)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@userId", userId);

                var data = unitOfWork.Procedure<Project_RoleEntity>("ptgroup_Project_Role_ListAllByUser", p);
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }
        public List<Project_RoleEntity> ListAllByProject(long ProjectId)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@ProjectId", ProjectId);

                var data = unitOfWork.Procedure<Project_RoleEntity>("ptgroup_Project_Role_ListAllByProject", p);
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }

        public List<Project_RoleEntity> ListAllByUserProject(int userId, long ProjectId)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@userId", userId);
                p.Add("@ProjectId", ProjectId);
                var data = unitOfWork.Procedure<Project_RoleEntity>("ptgroup_Project_Role_ListAllByUserAndProjectId", p);
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }
        /// <summary>
        /// Selects all records from the Role table.
        /// </summary>
        public List<Project_RoleEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<Project_RoleEntity>("ptgroup_Project_Role_ListAllPaging", p);
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
        /// Saves a record to the Role table.
        /// </summary>
        private DynamicParameters Param(Project_RoleEntity project_role, string action = "add")
        {
            var p = new DynamicParameters();
            p.Add("@UserId", project_role.UserId);
            p.Add("@ProjectId", project_role.ProjectId);
            p.Add("@Add", project_role.Add);
            p.Add("@Edit", project_role.Edit);
            p.Add("@View", project_role.View);
            p.Add("@Delete", project_role.Delete);
            p.Add("@Print", project_role.Print);
            p.Add("@ViewPrice", project_role.ViewPrice);
            p.Add("@Position", project_role.Position);
            return p;
        }


        #endregion
    }
}
