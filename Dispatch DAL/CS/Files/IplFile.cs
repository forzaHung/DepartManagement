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
    public class IplFile : BaseIpl<ADOProvider>, IFile
    {
        #region Methods
        /// <summary>
        /// Saves a record to the Files table.
        /// </summary>
        public long Insert(FileEntity file)
        {
            long res = 0;
            bool flag = false;
            try
            {
                var p = Param(file);
                flag = (bool)unitOfWork.ProcedureExecute("ptgroup_File_Insert", p);
                if (flag)
                {
                    res = p.Get<long>("@Id");
                }
                else
                {
                    res = 0;
                }
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
            }
            return res;
        }
        /// <summary>
        /// Updates a record in the Files table.
        /// </summary>
        public bool Update(FileEntity file)
        {
            bool res = false;
            try
            {
                var p = Param(file, "edit");
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_File_Update", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return res;
            }
        }
        /// <summary>
        /// Deletes a record from the Files table by its primary key.
        /// </summary>
        public bool Delete(long id)
        {
            try
            {
                bool res = false;
                var p = new DynamicParameters();
                p.Add("@Id", id);
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_File_Delete", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        /// <summary>
        /// Selects a single record from the Files table.
        /// </summary>
        public FileEntity ViewDetail(long id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);
                var data = unitOfWork.Procedure<FileEntity>("ptgroup_File_ViewDetail", p).SingleOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public FileEntity ViewDetailByDispatch(long DispatchId,bool Type)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@DispatchId", DispatchId);
                p.Add("@Type", Type);
                var data = unitOfWork.Procedure<FileEntity>("ptgroup_File_ViewDetailByDispatchId", p).SingleOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        /// <summary>
        /// Selects all records from the Files table.
        /// </summary>
        public List<FileEntity> ListAll()
        {
            try
            {
                var data = unitOfWork.Procedure<FileEntity>("ptgroup_File_ListAll");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        /// <summary>
        /// Selects all records from the Files table.
        /// </summary>
        public List<FileEntity> ListAllPaging(string searchCode, string searchName, string searchUploadDate, int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@searchCode", searchCode);
                p.Add("@searchName", searchName);
                p.Add("@searchUploadDate", searchUploadDate);
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<FileEntity>("ptgroup_Files_ListAllPaging", p);
                totalRow = p.Get<int>("@totalRow");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        /// <summary>
        /// Saves a record to the Files table.
        /// </summary>
        private DynamicParameters Param(FileEntity file, string action = "add")
        {
            var p = new DynamicParameters();
            if (action == "edit")
            {
                p.Add("@Id", file.Id);
            }
            else
            {
                p.Add("@Id", dbType: DbType.Int64, direction: ParameterDirection.Output);
            }
            p.Add("@UserId", file.UserId);
            p.Add("@Name", file.Name);
            p.Add("@MimeType", file.MimeType);
            p.Add("@FileSize", file.FileSize);
            p.Add("@IsDel", file.IsDel);
            p.Add("@Private", file.Private);
            p.Add("@UploadDate", file.UploadDate);
            p.Add("@DispatchId", file.DispatchId);
            p.Add("@Type", file.Type);
            return p;
        }
        #endregion
    }
}
