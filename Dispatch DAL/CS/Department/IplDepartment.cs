using Framework;
using Framework.Data;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dispatch
{
    public class IplDepartment : BaseIpl<ADOProvider>, IDepartment
	{
		#region Methods
		/// <summary>
		/// Saves a record to the Department table.
		/// </summary>
		public int Insert(DepartmentEntity department)		{
			int res = 0;
			bool flag = false;
			try
			{
				var p = Param(department);
				flag = (bool)unitOfWork.ProcedureExecute("ptgroup_Department_Insert", p);
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
			}
			return res;
		}

		/// <summary>
		/// Updates a record in the Department table.
		/// </summary>
		public bool Update(DepartmentEntity department)
		{
			bool res = false;
			try
			{
				var p = Param(department, "edit");
				res = (bool)unitOfWork.ProcedureExecute("ptgroup_Department_Update", p);
				return res;
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				return false;
			}
		}

		/// <summary>
		/// Deletes a record from the Department table by its primary key.
		/// </summary>
		public bool Delete(int id)
		{
			try
			{
				bool res = false;
				var p = new DynamicParameters();
				p.Add("@Id", id);
				res = (bool)unitOfWork.ProcedureExecute("ptgroup_Department_Delete", p);
				return res;
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				return false;
			}
		}

		/// <summary>
		/// Selects a single record from the Department table.
		/// </summary>
		public DepartmentEntity ViewDetail(int id)
		{
			try
			{
				var p = new DynamicParameters();
				p.Add("@Id", id);
				var data = unitOfWork.Procedure<DepartmentEntity>("ptgroup_Department_ViewDetail", p).SingleOrDefault();
				return data;
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				return null;
			}
		}

		/// <summary>
		/// Selects all records from the Department table.
		/// </summary>
		public List<DepartmentEntity> ListAll()
		{
			try
			{
				var data = unitOfWork.Procedure<DepartmentEntity>("ptgroup_Department_ListAll");
				return data.ToList();
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				return null;
			}
		}

		/// <summary>
		/// Selects all records from the Department table.
		/// </summary>
		public List<DepartmentEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow)
		{
			try
			{
				var p = new DynamicParameters();
				p.Add("@pageIndex", pageIndex);
				p.Add("@pageSize", pageSize);
				p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
				var data = unitOfWork.Procedure<DepartmentEntity>("ptgroup_Department_ListAllPaging", p);
				totalRow = p.Get<int>("@totalRow");
				return data.ToList();
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				return null;
			}
		}
        public List<DepartmentEntity> ListAllByTreeView() {
            try
            {
                var data = unitOfWork.Procedure<DepartmentEntity>("ptgroup_Department_ListAllDepartmentByTreeView");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public List<DepartmentEntity> ListAllPaging(string searchString, int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@searchString", searchString);
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<DepartmentEntity>("ptgroup_Department_ListAllPaging", p);
                totalRow = p.Get<int>("@totalRow");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }

        /// <summary>
        /// Saves a record to the Department table.
        /// </summary>
        private DynamicParameters Param(DepartmentEntity department, string action = "add")
		{
			var p = new DynamicParameters();
			if (action == "edit")
			{
				p.Add("@Id", department.Id);
                p.Add("@ModifiedDate", department.ModifiedDate);
            }
			else
			{
				p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@CreatedDate", department.CreatedDate);
            }
			p.Add("@Name", department.Name);
            p.Add("@ParentId", department.ParentId);
            p.Add("@Location", department.Location);
            p.Add("@EmployeeManagerId", department.EmployeeManagerId);
            p.Add("@EmployeeDebutyManagerId", department.EmployeeDebutyManagerId);
            p.Add("@EmployeeChiefOfTheOfficeId", department.EmployeeChiefOfTheOfficeId);
            p.Add("@SumEmployeesExpected", department.SumEmployeesExpected);
            return p;
		}
		#endregion
	}
}
