using System;
using System.Collections.Generic;
namespace Dispatch
{
    public interface ICustomer_Type
    {
        int Insert(Customer_TypeEntity customer_type);
        bool Update(Customer_TypeEntity customer_type);
        bool Delete(int id);
        List<Customer_TypeEntity> ListAll();
        List<Customer_TypeEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow);
        List<Customer_TypeEntity> ListAllPaging(string searchString, int pageIndex, int pageSize, ref int totalRow);
        Customer_TypeEntity ViewDetail(int id);
        List<Customer_TypeEntity> BindTreeview();
    }

}

