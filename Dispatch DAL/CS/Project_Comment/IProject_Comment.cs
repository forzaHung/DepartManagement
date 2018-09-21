using System;
using System.Collections.Generic;
namespace Dispatch
{
    public interface IProject_Comment
    {
        int Insert(Project_CommentEntity project_comment);
        bool Update(Project_CommentEntity project_comment);
        bool Delete(int id);
        List<Project_CommentEntity> ListAll();
        List<Project_CommentEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow);
        List<Project_CommentEntity> ListAllPaging(string searchString, int pageIndex, int pageSize, ref int totalRow);
        List<Project_CommentEntity> ListViewByProjectTop1(long ProjectId);

        Project_CommentEntity ViewDetail(int id);
        List<Project_CommentEntity> GetByFKProject(long projectId);
        List<Project_CommentEntity> GetBySK(long id);
    }

}

