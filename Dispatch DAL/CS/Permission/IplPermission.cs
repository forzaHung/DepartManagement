using Framework;
using Framework.Data;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dispatch
{
    public class IplPermission : BaseIpl<ADOProvider>, IPermission
	{


		#region Methods

		/// <summary>
		/// Saves a record to the Permission table.
		/// </summary>
		public int Insert(PermissionEntity permission)		{
			int res = 0;
			bool flag = false;
			try
			{
				var p = Param(permission);
				flag = (bool)unitOfWork.ProcedureExecute("ptgroup_Permission_Insert", p);
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
				Logging.PutError(ex.Message , ex);
				throw;
			}
			return res;
		}

		/// <summary>
		/// Updates a record in the Permission table.
		/// </summary>
		public bool Update(PermissionEntity permission)
		{
			bool res = false;
			try
			{
				var p = Param(permission, "edit");
				res = (bool)unitOfWork.ProcedureExecute("ptgroup_Permission_Update", p);
				return res;
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				throw;
			}
		}

		/// <summary>
		/// Deletes a record from the Permission table by its primary key.
		/// </summary>
		public bool Delete(int id)
		{
			try
			{
				bool res = false;
				var p = new DynamicParameters();
				p.Add("@Id", id);
				res = (bool)unitOfWork.ProcedureExecute("ptgroup_Permission_Delete", p);
				return res;
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				throw;
			}
		}

		/// <summary>
		/// Selects a single record from the Permission table.
		/// </summary>
		public PermissionEntity ViewDetail(int id)
		{
			try
			{
				var p = new DynamicParameters();
				p.Add("@Id", id);
				var data = unitOfWork.Procedure<PermissionEntity>("ptgroup_Permission_ViewDetail", p).SingleOrDefault();
				return data;
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				return null;
			}

		}

		/// <summary>
		/// Selects all records from the Permission table.
		/// </summary>
		public List<PermissionEntity> ListAll()
		{
			try
			{
				var data = unitOfWork.Procedure<PermissionEntity>("ptgroup_Permission_ListAll");
				return data.ToList();
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				throw;
			}
		}

		/// <summary>
		/// Selects all records from the Permission table.
		/// </summary>
		public List<PermissionEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow)
		{
			try
			{
				var p = new DynamicParameters();
				p.Add("@pageIndex", pageIndex);
				p.Add("@pageSize", pageSize);
				p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
				var data = unitOfWork.Procedure<PermissionEntity>("ptgroup_Permission_ListAllPaging", p);
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
		/// Saves a record to the Permission table.
		/// </summary>
		private DynamicParameters Param(PermissionEntity permission, string action = "add")
		{
			var p = new DynamicParameters();
			if (action == "edit")
			{
				p.Add("@Id", permission.Id);
			}
			else
			{
				p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
			}
			p.Add("@UserId", permission.UserId);
			p.Add("@GroupId", permission.GroupId);
			return p;
		}


		#endregion
	}
}
