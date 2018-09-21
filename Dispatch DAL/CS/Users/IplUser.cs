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
	public class IplUser : BaseIpl<ADOProvider>, IUser
	{


		#region Methods

		/// <summary>
		/// Saves a record to the Users table.
		/// </summary>
		public int Insert(UserEntity user){
			int res = 0;
			bool flag = false;
			try
			{
				var p = Param(user);
				flag = (bool)unitOfWork.ProcedureExecute("ptgroupUser_Insert", p);
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
				Logging.PutError(ex.Message , ex);
				throw;
			}
			return res;
		}

		/// <summary>
		/// Updates a record in the Users table.
		/// </summary>
		public bool Update(UserEntity user)
		{
			bool res = false;
			try
			{
				var p = Param(user, "edit");
				res = (bool)unitOfWork.ProcedureExecute("ptgroupUser_Update", p);
				return res;
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				throw;
			}
		}
        public bool UpdateActive(int userId, bool access)
        {
            int res;
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", userId);
                p.Add("@IsActive", access);
                p.Add("@return", DbType.Int32, direction: ParameterDirection.Output);
                bool kq =  unitOfWork.ProcedureExecute("ptgroupUser_UpdateActive", p);
                res = p.Get<int>("@return");
                if (res == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false; 
            }
        }

        /// <summary>
        /// Deletes a record from the Users table by its primary key.
        /// </summary>
        public bool Delete(int id)
		{
			try
			{
				bool res = false;
				var p = new DynamicParameters();
				p.Add("@Id", id);
				res = (bool)unitOfWork.ProcedureExecute("ptgroup_User_Delete", p);
				return res;
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				throw;
			}
		}

		/// <summary>
		/// Selects a single record from the Users table.
		/// </summary>
		public UserEntity ViewDetail(int id)
		{
			try
			{
				var p = new DynamicParameters();
				p.Add("@Id", id);
				var data = unitOfWork.Procedure<UserEntity>("ptgroupUser_ViewDetail", p).SingleOrDefault();
				return data;
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				return null;
			}

		}

		/// <summary>
		/// Selects all records from the Users table.
		/// </summary>
		public List<UserEntity> ListAll()
		{
			try
			{
				var data = unitOfWork.Procedure<UserEntity>("ptgroupUser_ListAll");
				return data.ToList();
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				throw;
			}
		}

		/// <summary>
		/// Selects all records from the Users table.
		/// </summary>
		public List<UserEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow)
		{
			try
			{
				var p = new DynamicParameters();
				p.Add("@pageIndex", pageIndex);
				p.Add("@pageSize", pageSize);
				p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
				var data = unitOfWork.Procedure<UserEntity>("ptgroupUser_ListAllPaging", p);
				totalRow = p.Get<int>("@totalRow");
				return data.ToList();
			}
			catch (Exception ex)
			{
				Logging.PutError(ex.Message , ex);
				throw;
			}
		}
        /// <summary>
        /// Changes the pass.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="newPass">The new pass.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ChangePass(int userId, string newPass)
        {
            bool res = false;
            try
            {
                var p = new DynamicParameters();
                p.Add("@userId", userId);
                p.Add("@newPass", newPass);
                res = (bool)unitOfWork.ProcedureExecute("ptgroupUser_ChangePass", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }
        /// <summary>
        /// Logins the specified user name.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <returns>UserEntity.</returns>
        public UserEntity Login(string UserName)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@UserName", UserName);
                var data = unitOfWork.Procedure<UserEntity>("ptgroupUser_Login", p).SingleOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }

        /// <summary>
        /// Saves a record to the Users table.
        /// </summary>
        private DynamicParameters Param(UserEntity user, string action = "add")
		{
            var p = new DynamicParameters();
            if (action == "add")
            {
                p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@EmployeeId", user.EmployeeId);
                p.Add("@UserName", user.UserName);
                p.Add("@IsActive", user.IsActive);
            }
            else
            {
                p.Add("@Id", user.Id);
                p.Add("@LastPass", user.LastPass);
                p.Add("@LastChangePassword", user.LastChangePassword);
            }

            p.Add("@Password", user.Password);
            return p;
        }


		#endregion
	}
}
