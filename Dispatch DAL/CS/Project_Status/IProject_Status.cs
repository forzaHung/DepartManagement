using System;
using System.Collections.Generic;
namespace Dispatch
{
    public interface IProject_Status
    {
        int Insert(Project_StatusEntity project_status);
        bool Update(Project_StatusEntity project_status);
        bool Delete(int id);
        List<Project_StatusEntity> ListAll();
        List<Project_StatusEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow);
        List<Project_StatusEntity> ListAllPaging(string searchString, int pageIndex, int pageSize, ref int totalRow);
        Project_StatusEntity ViewDetail(int id);
    }

}

