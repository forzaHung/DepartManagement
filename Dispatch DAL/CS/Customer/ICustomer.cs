using System;
using System.Collections.Generic;
namespace Dispatch
{
    public interface ICustomer
    {
        int Insert(CustomerEntity customer);
        bool Update(CustomerEntity customer);
        bool Delete(int id);
        List<CustomerEntity> ListAll();
        List<CustomerEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow);
        List<CustomerEntity> ListAllPaging(string searchString, int pageIndex, int pageSize, ref int totalRow);
        CustomerEntity ViewDetail(int id);
        List<CustomerEntity> ListByType(string Customer_TypeId);
    }

}

