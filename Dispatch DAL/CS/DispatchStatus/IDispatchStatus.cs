using System.Collections.Generic;
namespace Dispatch
{
    public interface IDispatchStatus
    {
        int Insert(DispatchStatusEntity dispatchstatu);
        bool Update(DispatchStatusEntity dispatchstatu);
        bool Delete(int id);
        List<DispatchStatusEntity> ListAll();
        List<DispatchStatusEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow);
        List<DispatchStatusEntity> ListAllPaging(string searchString, int pageIndex, int pageSize, ref int totalRow);
        DispatchStatusEntity ViewDetail(int id);
    }

}

