using System;
using System.Collections.Generic;

namespace Dispatch
{
	public interface IPermission
	{
		int Insert(PermissionEntity permission);		bool Update(PermissionEntity permission);
		bool Delete(int id);
		List<PermissionEntity> ListAll();
		List<PermissionEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow);
		PermissionEntity ViewDetail(int id);
	}
}
