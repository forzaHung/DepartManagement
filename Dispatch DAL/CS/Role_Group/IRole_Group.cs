using System;
using System.Collections.Generic;

namespace Dispatch
{
	public interface IRole_Group
	{
		int Insert(Role_GroupEntity role_Group);		bool Update(Role_GroupEntity role_Group);
		bool Delete(int iD);
		List<Role_GroupEntity> ListAll();
		List<Role_GroupEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow);
		Role_GroupEntity ViewDetail(int iD);
	}
}
