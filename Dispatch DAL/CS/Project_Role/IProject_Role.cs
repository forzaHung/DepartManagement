using System;
using System.Collections.Generic;

namespace Dispatch
{
    public interface IProject_Role
    {
        Int32 Insert(Project_RoleEntity role);
        bool Update(Project_RoleEntity role);
        bool Delete(int userId, long ProjectId);
        List<Project_RoleEntity> ListAll();
        List<Project_RoleEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow);
        Project_RoleEntity ViewDetail(int userId, long ProjectId);
        List<Project_RoleEntity> ListAllByUser(int userId);
        List<Project_RoleEntity> ListAllByProject(long ProjectId);
        List<Project_RoleEntity> ListAllByUserProject(int userId, long ProjectId);
    }
}
