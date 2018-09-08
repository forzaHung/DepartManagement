using System.Collections.Generic;
namespace Dispatch
{
    public interface IDispatchIn
    {
        int Insert(DispatchInEntity dispatchin);
        bool Update(DispatchInEntity dispatchin);
        bool UpdateFileId(DispatchInEntity dispatchin);
        bool Delete(int id);
        List<DispatchInEntity> ListAll();
        List<DispatchInEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow);
       
        List<DispatchInEntity> ListAllPaging(string searchCode, string searchName, int searchType,int searchDepartment,
            int searchStatus,int Priority,string searchDateFrom, int AddressToId, int pageIndex, int pageSize, ref int totalRow);

        int CountDispatchIn(int searchType, int searchStatus, int AddressToId);

        DispatchInEntity ViewDetail(int id);
    }

}

