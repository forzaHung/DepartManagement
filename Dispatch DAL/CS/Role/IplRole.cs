using Framework;
using Framework.Data;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dispatch
{
    public class IplRole : BaseIpl<ADOProvider>, IRole
	{


		#region Methods

		/// <summary>
		/// Saves a record to the Role table.
		/// </summary>
		public Int32 Insert(RoleEntity role)		{
			bool flag = false;
			try
			{
				var p = Param(role);
				flag = (bool)unitOfWork.ProcedureExecute("ptgroup_Role_Insert", p);
				if (flag)
					return 1;
				return 0;
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				throw;
			}
			return 0;
		}

		/// <summary>
		/// Updates a record in the Role table.
		/// </summary>
		public bool Update(RoleEntity role)
		{
			bool res = false;
			try
			{
				var p = Param(role, "edit");
				res = (bool)unitOfWork.ProcedureExecute("ptgroup_Role_Update", p);
				return res;
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
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
        public bool Update(int userId, int moduleId, string action, bool access)
        {
            bool res = false;
            try
            {
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                p.Add("@ModuleId", moduleId);
                p.Add("@action", action);
                p.Add("@access", access);
                res = (bool)unitOfWork.ProcedureExecute("ptgroupRole_UpdateDynamic", p);
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
        public bool Delete(int userId, int moduleId)
		{
			try
			{
				bool res = false;
				var p = new DynamicParameters();
				p.Add("@UserId", userId);
				p.Add("@ModuleId", moduleId);
				res = (bool)unitOfWork.ProcedureExecute("ptgroup_Role_Delete", p);
				return res;
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				throw;
			}
		}

		/// <summary>
		/// Selects a single record from the Role table.
		/// </summary>
		public RoleEntity ViewDetail(int userId, int moduleId)
		{
			try
			{
				var p = new DynamicParameters();
				p.Add("@UserId", userId);
				p.Add("@ModuleId", moduleId);
				var data = unitOfWork.Procedure<RoleEntity>("ptgroup_Role_ViewDetail", p).SingleOrDefault();
				return data;
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				return null;
			}

		}

		/// <summary>
		/// Selects all records from the Role table.
		/// </summary>
		public List<RoleEntity> ListAll()
		{
			try
			{
				var data = unitOfWork.Procedure<RoleEntity>("ptgroup_Role_ListAll");
				return data.ToList();
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				throw;
			}
		}
        /// <summary>
        /// Lists all.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List&lt;RoleEntity&gt;.</returns>
        public List<RoleEntity> ListAll(int userId)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@userId", userId);

                var data = unitOfWork.Procedure<RoleEntity>("ptgroupRole_ListAll", p);
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
        public List<RoleEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow)
		{
			try
			{
				var p = new DynamicParameters();
				p.Add("@pageIndex", pageIndex);
				p.Add("@pageSize", pageSize);
				p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
				var data = unitOfWork.Procedure<RoleEntity>("ptgroup_Role_ListAllPaging", p);
				totalRow = p.Get<int>("@totalRow");
				return data.ToList();
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				throw;
			}
		}

		/// <summary>
		/// Saves a record to the Role table.
		/// </summary>
		private DynamicParameters Param(RoleEntity role, string action = "add")
		{
			var p = new DynamicParameters();
			p.Add("@UserId", role.UserId);
			p.Add("@ModuleId", role.ModuleId);
			p.Add("@Add", role.Add);
			p.Add("@Edit", role.Edit);
			p.Add("@View", role.View);
			p.Add("@Delete", role.Delete);
			p.Add("@Import", role.Import);
			p.Add("@Export", role.Export);
			p.Add("@Upload", role.Upload);
			p.Add("@Publish", role.Publish);
			p.Add("@Report", role.Report);
			p.Add("@Print", role.Print);
			return p;
		}


		#endregion
	}
}
