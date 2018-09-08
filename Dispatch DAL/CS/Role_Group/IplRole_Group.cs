using Framework;
using Framework.Data;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dispatch
{
    public class IplRole_Group : BaseIpl<ADOProvider>, IRole_Group
	{


		#region Methods

		/// <summary>
		/// Saves a record to the Role_Group table.
		/// </summary>
		public int Insert(Role_GroupEntity role_Group)		{
			int res = 0;
			bool flag = false;
			try
			{
				var p = Param(role_Group);
				flag = (bool)unitOfWork.ProcedureExecute("ptgroup_Role_Group_Insert", p);
				if (flag)
				{
					res = p.Get<int>("@ID");
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
		/// Updates a record in the Role_Group table.
		/// </summary>
		public bool Update(Role_GroupEntity role_Group)
		{
			bool res = false;
			try
			{
				var p = Param(role_Group, "edit");
				res = (bool)unitOfWork.ProcedureExecute("ptgroup_Role_Group_Update", p);
				return res;
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				throw;
			}
		}

		/// <summary>
		/// Deletes a record from the Role_Group table by its primary key.
		/// </summary>
		public bool Delete(int iD)
		{
			try
			{
				bool res = false;
				var p = new DynamicParameters();
				p.Add("@ID", iD);
				res = (bool)unitOfWork.ProcedureExecute("ptgroup_Role_Group_Delete", p);
				return res;
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				throw;
			}
		}

		/// <summary>
		/// Selects a single record from the Role_Group table.
		/// </summary>
		public Role_GroupEntity ViewDetail(int iD)
		{
			try
			{
				var p = new DynamicParameters();
				p.Add("@ID", iD);
				var data = unitOfWork.Procedure<Role_GroupEntity>("ptgroup_Role_Group_ViewDetail", p).SingleOrDefault();
				return data;
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				return null;
			}

		}

		/// <summary>
		/// Selects all records from the Role_Group table.
		/// </summary>
		public List<Role_GroupEntity> ListAll()
		{
			try
			{
				var data = unitOfWork.Procedure<Role_GroupEntity>("ptgroup_Role_Group_ListAll");
				return data.ToList();
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				throw;
			}
		}

		/// <summary>
		/// Selects all records from the Role_Group table.
		/// </summary>
		public List<Role_GroupEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow)
		{
			try
			{
				var p = new DynamicParameters();
				p.Add("@pageIndex", pageIndex);
				p.Add("@pageSize", pageSize);
				p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
				var data = unitOfWork.Procedure<Role_GroupEntity>("ptgroup_Role_Group_ListAllPaging", p);
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
		/// Saves a record to the Role_Group table.
		/// </summary>
		private DynamicParameters Param(Role_GroupEntity role_Group, string action = "add")
		{
			var p = new DynamicParameters();
			if (action == "edit")
			{
				p.Add("@ID", role_Group.ID);
			}
			else
			{
				p.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);
			}
			p.Add("@Name", role_Group.Name);
			return p;
		}


		#endregion
	}
}
