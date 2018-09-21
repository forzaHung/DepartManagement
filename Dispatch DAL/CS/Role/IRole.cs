using System;
using System.Collections.Generic;

namespace Dispatch
{
	public interface IRole
	{
		Int32 Insert(RoleEntity role);		bool Update(RoleEntity role);
		bool Delete(int userId, int moduleId);
		List<RoleEntity> ListAll();
		List<RoleEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow);
		RoleEntity ViewDetail(int userId, int moduleId);
        List<RoleEntity> ListAll(int userId);
    }
}
