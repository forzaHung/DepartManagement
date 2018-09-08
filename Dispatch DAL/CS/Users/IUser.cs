using System.Collections.Generic;

namespace Dispatch
{
    public interface IUser
	{
		int Insert(UserEntity users);
        bool Update(UserEntity users);
		bool Delete(int id);
        bool UpdateActive(int userId, bool access);

        List<UserEntity> ListAll();
		List<UserEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow);
		UserEntity ViewDetail(int id);
        bool ChangePass(int userId, string newPass);
        UserEntity Login(string UserName);
    }
}
