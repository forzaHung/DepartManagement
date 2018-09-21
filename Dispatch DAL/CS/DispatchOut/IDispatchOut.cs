using System;
using System.Collections.Generic;
namespace Dispatch
{
    public interface IDispatchOut
    {
        int Insert(DispatchOutEntity dispatchin);
        bool Update(DispatchOutEntity dispatchin);
        bool UpdateFileId(DispatchOutEntity dispatchin);
        bool Delete(int id);
        List<DispatchOutEntity> ListAll();

        List<DispatchOutEntity> ListAllPaging(string searchCode, string searchName, int searchType,int searchDepartment,
            int searchStatus,int Priority,string searchDateFrom, int AddressToId, int pageIndex, int pageSize, ref int totalRow);
        int CountDispatchOut(int searchType, int searchStatus, int AddressToId);
        DispatchOutEntity ViewDetail(int id);
    }

}

