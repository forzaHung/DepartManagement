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
    #region IplCustomer_Type
    /// <summary>
    /// This object represents the properties and methods of a Customer_Type.
    /// </summary>
    public class IplCustomer_Type : BaseIpl<ADOProvider>, ICustomer_Type
    {
        public int Insert(Customer_TypeEntity customer_type)
        {
            int res = 0;
            bool flag = false;
            try
            {
                var p = Param(customer_type);
                flag = (bool)unitOfWork.ProcedureExecute("ptgroup_Customer_Type_Insert", p);
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

        public bool Update(Customer_TypeEntity customer_type)
        {
            bool res = false;
            try
            {
                var p = Param(customer_type, "edit");
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_Customer_Type_Update", p);
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
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_Customer_Type_Delete", p);
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
        public Customer_TypeEntity ViewDetail(int id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);
                var data = unitOfWork.Procedure<Customer_TypeEntity>("ptgroup_Customer_Type_ViewDetail", p).SingleOrDefault();
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
        public List<Customer_TypeEntity> ListAll()
        {
            try
            {
                var data = unitOfWork.Procedure<Customer_TypeEntity>("ptgroup_Customer_Type_ListAll");
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
        public List<Customer_TypeEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<Customer_TypeEntity>("ptgroup_Customer_Type_ListAllPaging", p);
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
        public List<Customer_TypeEntity> ListAllPaging(string searchString, int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@searchString", searchString);
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<Customer_TypeEntity>("ptgroup_Customer_Type_ListAllPaging", p);
                totalRow = p.Get<int>("@totalRow");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }


        public List<Customer_TypeEntity> BindTreeview()
        {
            try
            {
                var p = new DynamicParameters();
                var data = unitOfWork.Procedure<Customer_TypeEntity>("Customer_Type_ListAllCateByTreeView", p);
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
        private DynamicParameters Param(Customer_TypeEntity customer_type, string action = "add")
        {
            var p = new DynamicParameters();
            if (action == "add")
            {
                p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            }
            else
            {
                p.Add("@Id", customer_type.Id);
            }
            p.Add("@ParentId", customer_type.ParentId);
            p.Add("@Name", customer_type.Name);
            p.Add("@IsDel", customer_type.IsDel);
            p.Add("@IsActive", customer_type.IsActive);

            return p;
        }






    }
    #endregion
}

