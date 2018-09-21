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
    #region IplCustomer
    /// <summary>
    /// This object represents the properties and methods of a Customer.
    /// </summary>
    public class IplCustomer : BaseIpl<ADOProvider>, ICustomer
    {
        public int Insert(CustomerEntity customer)
        {
            int res = 0;
            bool flag = false;
            try
            {
                var p = Param(customer);
                flag = (bool)unitOfWork.ProcedureExecute("ptgroup_Customer_Insert", p);
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

        public bool Update(CustomerEntity customer)
        {
            bool res = false;
            try
            {
                var p = Param(customer, "edit");        
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_Customer_Update", p);
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
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_Customer_Delete", p);
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
        public CustomerEntity ViewDetail(int id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);
                var data = unitOfWork.Procedure<CustomerEntity>("ptgroup_Customer_ViewDetail", p).SingleOrDefault();
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
        public List<CustomerEntity> ListAll()
        {
            try
            {
                var data = unitOfWork.Procedure<CustomerEntity>("ptgroup_Customer_ListAll");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }
        public List<CustomerEntity> ListByType(string Customer_TypeId)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Customer_TypeId", Customer_TypeId);

                var data = unitOfWork.Procedure<CustomerEntity>("ptgroup_Customer_ViewDetail_ByType", p);

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
        public List<CustomerEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<CustomerEntity>("ptgroup_Customer_ListAllPaging", p);
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
        public List<CustomerEntity> ListAllPaging(string searchString, int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@searchString", searchString);
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<CustomerEntity>("ptgroup_Customer_ListAllPaging", p);
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
        private DynamicParameters Param(CustomerEntity customer, string action = "add")
        {
            var p = new DynamicParameters();
            if (action == "add")
            {
                p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Createdate", customer.Createdate);
            }
            else
            {
                p.Add("@Id", customer.Id);
                p.Add("@ModifiedDate", customer.ModifiedDate);
            }
            p.Add("@ParentId", customer.ParentId);
            p.Add("@Name", customer.Name);
            p.Add("@Address", customer.Address);
            p.Add("@Customer_TypeId", customer.Customer_TypeId);
         
         
            p.Add("@IsDel", customer.IsDel);
            p.Add("@IsActive", customer.IsActive);

            return p;
        }






    }
    #endregion
}

