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
	public class IplModule : BaseIpl<ADOProvider>, IModule
	{


		#region Methods

		/// <summary>
		/// Saves a record to the Module table.
		/// </summary>
		public int Insert(ModuleEntity module)		{
			int res = 0;
			bool flag = false;
			try
			{
				var p = Param(module);
				flag = (bool)unitOfWork.ProcedureExecute("ptgroup_Module_Insert", p);
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
		/// Updates a record in the Module table.
		/// </summary>
		public bool Update(ModuleEntity module)
		{
			bool res = false;
			try
			{
				var p = Param(module, "edit");
				res = (bool)unitOfWork.ProcedureExecute("ptgroup_Module_Update", p);
				return res;
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				throw;
			}
		}

		/// <summary>
		/// Deletes a record from the Module table by its primary key.
		/// </summary>
		public bool Delete(int iD)
		{
			try
			{
				bool res = false;
				var p = new DynamicParameters();
				p.Add("@ID", iD);
				res = (bool)unitOfWork.ProcedureExecute("ptgroup_Module_Delete", p);
				return res;
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				throw;
			}
		}

		/// <summary>
		/// Selects a single record from the Module table.
		/// </summary>
		public ModuleEntity ViewDetail(int iD)
		{
			try
			{
				var p = new DynamicParameters();
				p.Add("@ID", iD);
				var data = unitOfWork.Procedure<ModuleEntity>("ptgroup_Module_ViewDetail", p).SingleOrDefault();
				return data;
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				return null;
			}

		}

		/// <summary>
		/// Selects all records from the Module table.
		/// </summary>
		public List<ModuleEntity> ListAll()
		{
			try
			{
				var data = unitOfWork.Procedure<ModuleEntity>("ptgroup_Module_ListAll");
				return data.ToList();
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				throw;
			}
		}

		/// <summary>
		/// Selects all records from the Module table.
		/// </summary>
		public List<ModuleEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow)
		{
			try
			{
				var p = new DynamicParameters();
				p.Add("@pageIndex", pageIndex);
				p.Add("@pageSize", pageSize);
				p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
				var data = unitOfWork.Procedure<ModuleEntity>("ptgroup_Module_ListAllPaging", p);
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
		/// Saves a record to the Module table.
		/// </summary>
		private DynamicParameters Param(ModuleEntity module, string action = "add")
		{
			var p = new DynamicParameters();
			if (action == "edit")
			{
				p.Add("@ID", module.ID);
			}
			else
			{
				p.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);
			}
			p.Add("@Name", module.Name);
			return p;
		}
        public ModuleEntity ViewDetailByName(string Name)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Name", Name);
                var data = unitOfWork.Procedure<ModuleEntity>("ptgroup_Module_ViewDetail_ByName", p).SingleOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }

        }

        #endregion
    }
}
