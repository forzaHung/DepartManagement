using System;
using System.Collections.Generic;
namespace Dispatch
{
    public interface IDispatchPriority
    {
        int Insert(DispatchPriorityEntity dispatchpriority);
        bool Update(DispatchPriorityEntity dispatchpriority);
        bool Delete(int id);
        List<DispatchPriorityEntity> ListAll();
        List<DispatchPriorityEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow);
        List<DispatchPriorityEntity> ListAllPaging(string searchString, int pageIndex, int pageSize, ref int totalRow);
        DispatchPriorityEntity ViewDetail(int id);
    }

}

