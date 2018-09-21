using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Framework;
using Framework.Data;
using Framework.Helper.Logging;

namespace Dispatch
{
    #region IplProject_Comment
    /// <summary>
    /// This object represents the properties and methods of a Project_Comment.
    /// </summary>
    public class IplProject_Comment : BaseIpl<ADOProvider>, IProject_Comment
    {
        public int Insert(Project_CommentEntity project_comment)
        {
            int res = 0;
            bool flag = false;
            try
            {
                var p = Param(project_comment);
                flag = (bool)unitOfWork.ProcedureExecute("ptgroup_Project_Comment_Insert", p);
                if (flag)
                {
                    res = p.Get<int>("@Id");
                }
                else
                {
                    res = 0;
                }
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
            return res;
        }


        ///Update

        public bool Update(Project_CommentEntity project_comment)
        {
            bool res = false;
            try
            {
                var p = Param(project_comment, "edit");
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_Project_Comment_Update", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }

        ///Delete

        public bool Delete(int id)
        {
            try
            {
                bool res = false;
                var p = new DynamicParameters();
                p.Add("@Id", id);
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_Project_Comment_Delete", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }
        /// <summary>
        /// Selects a single record from the table.
        /// </summary>
        public Project_CommentEntity ViewDetail(int id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);
                var data = unitOfWork.Procedure<Project_CommentEntity>("ptgroup_Project_Comment_ViewDetail", p).SingleOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }

        }

        /// <summary>
        /// Selects all records from the table.
        /// </summary>
        public List<Project_CommentEntity> ListAll()
        {
            try
            {
                var data = unitOfWork.Procedure<Project_CommentEntity>("ptgroup_Project_Comment_ListAll");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }

        /// <summary>
        /// Selects all records from the table.
        /// </summary>
        public List<Project_CommentEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<Project_CommentEntity>("ptgroup_Project_Comment_ListAllPaging", p);
                totalRow = p.Get<int>("@totalRow");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }
        public List<Project_CommentEntity> ListViewByProjectTop1(long ProjectId)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@ProjectId", ProjectId);               
                var data = unitOfWork.Procedure<Project_CommentEntity>("ptgroup_Project_Comment_ViewOrderByTop1", p);
            
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }
        /// <summary>
        /// Lists all paging.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalRow">The total row.</param>
        /// <returns>List&lt;EmployeeEntity&gt;.</returns>
        public List<Project_CommentEntity> ListAllPaging(string searchString, int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@searchString", searchString);
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<Project_CommentEntity>("ptgroup_Project_Comment_ListAllPaging", p);
                totalRow = p.Get<int>("@totalRow");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }

        /// <summary>
        /// Saves a record to the Employee table.
        /// </summary>
        private DynamicParameters Param(Project_CommentEntity project_comment, string action = "add")
        {
            var p = new DynamicParameters();
            if (action == "add")
            {
                p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@CreateDate", project_comment.CreateDate);
            }
            else
            {
                p.Add("@Id", project_comment.Id);
                p.Add("@ModifiedDate", project_comment.ModifiedDate);
            }
            p.Add("@ParentId", project_comment.ParentId);
            p.Add("@ProjectId", project_comment.ProjectId);
            p.Add("@UserId", project_comment.UserId);
            p.Add("@FullName", project_comment.FullName);
            p.Add("@Description", project_comment.Description);
         
         
            p.Add("@IsDel", project_comment.IsDel);

            return p;
        }
        public List<Project_CommentEntity> GetByFKProject(long id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@ProjectId", id);
                var data = unitOfWork.Procedure<Project_CommentEntity>("ptgroup_Project_Comment_GetByFK_ProjectID", p);
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }

        }
        public List<Project_CommentEntity> GetBySK(long id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);
                var data = unitOfWork.Procedure<Project_CommentEntity>("ptgroup_Project_Comment_GetBySK", p);
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }

        }




    }
    #endregion
}

