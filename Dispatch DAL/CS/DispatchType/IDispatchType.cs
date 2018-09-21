using System;
using System.Collections.Generic;
namespace Dispatch
{
    public interface IDispatchType
    {
        int Insert(DispatchTypeEntity dispatchtype);
        bool Update(DispatchTypeEntity dispatchtype);
        bool Delete(int id);
        List<DispatchTypeEntity> ListAll();
        List<DispatchTypeEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow);
        List<DispatchTypeEntity> ListAllPaging(string searchString, int pageIndex, int pageSize, ref int totalRow);
        DispatchTypeEntity ViewDetail(int id);
    }

}

