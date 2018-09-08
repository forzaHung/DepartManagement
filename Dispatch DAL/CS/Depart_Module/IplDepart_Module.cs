using Framework;
using Framework.Data;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dispatch
{
    public class IplDepart_Module : BaseIpl<ADOProvider>, IDepart_Module
	{


		#region Methods

		/// <summary>
		/// Saves a record to the Depart_Module table.
		/// </summary>
		public int Insert(Depart_ModuleEntity depart_Module)		{
			int res = 0;
			bool flag = false;
			try
			{
				var p = Param(depart_Module);
				flag = (bool)unitOfWork.ProcedureExecute("ptgroup_Depart_Module_Insert", p);
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
		/// Updates a record in the Depart_Module table.
		/// </summary>
		public bool Update(Depart_ModuleEntity depart_Module)
		{
			bool res = false;
			try
			{
				var p = Param(depart_Module, "edit");
				res = (bool)unitOfWork.ProcedureExecute("ptgroup_Depart_Module_Update", p);
				return res;
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				throw;
			}
		}

		/// <summary>
		/// Deletes a record from the Depart_Module table by its primary key.
		/// </summary>
		public bool Delete(int id)
		{
			try
			{
				bool res = false;
				var p = new DynamicParameters();
				p.Add("@Id", id);
				res = (bool)unitOfWork.ProcedureExecute("ptgroup_Depart_Module_Delete", p);
				return res;
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				throw;
			}
		}

		/// <summary>
		/// Selects a single record from the Depart_Module table.
		/// </summary>
		public Depart_ModuleEntity ViewDetail(int id)
		{
			try
			{
				var p = new DynamicParameters();
				p.Add("@Id", id);
				var data = unitOfWork.Procedure<Depart_ModuleEntity>("ptgroup_Depart_Module_ViewDetail", p).SingleOrDefault();
				return data;
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				return null;
			}

		}

		/// <summary>
		/// Selects all records from the Depart_Module table.
		/// </summary>
		public List<Depart_ModuleEntity> ListAll()
		{
			try
			{
				var data = unitOfWork.Procedure<Depart_ModuleEntity>("ptgroup_Depart_Module_ListAll");
				return data.ToList();
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				throw;
			}
		}

		/// <summary>
		/// Selects all records from the Depart_Module table.
		/// </summary>
		public List<Depart_ModuleEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow)
		{
			try
			{
				var p = new DynamicParameters();
				p.Add("@pageIndex", pageIndex);
				p.Add("@pageSize", pageSize);
				p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
				var data = unitOfWork.Procedure<Depart_ModuleEntity>("ptgroup_Depart_Module_ListAllPaging", p);
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
		/// Saves a record to the Depart_Module table.
		/// </summary>
		private DynamicParameters Param(Depart_ModuleEntity depart_Module, string action = "add")
		{
			var p = new DynamicParameters();
			if (action == "edit")
			{
				p.Add("@Id", depart_Module.Id);
			}
			else
			{
				p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
			}
			p.Add("@DepartId", depart_Module.DepartId);
			p.Add("@ModuleId", depart_Module.ModuleId);
			return p;
		}


		#endregion
	}
}
