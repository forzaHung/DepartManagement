using System;
using System.Collections.Generic;
namespace Dispatch
{
    public interface IProject
    {
        int Insert(ProjectEntity project);
        int Update(ProjectEntity project);
        bool Delete(long id);
        List<ProjectEntity> ListAll();
        List<ProjectEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow);
        List<ProjectEntity> ListAllPaging(string searchString, int pageIndex, int pageSize, ref int totalRow);
        ProjectEntity ViewDetail(long id);
        ProjectEntity ViewDetailWithCustomer(int id);
    }

}

