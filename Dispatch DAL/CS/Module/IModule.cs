using System;
using System.Collections.Generic;

namespace Dispatch
{
	public interface IModule
	{
		int Insert(ModuleEntity module);		bool Update(ModuleEntity module);
		bool Delete(int iD);
		List<ModuleEntity> ListAll();
		List<ModuleEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow);
		ModuleEntity ViewDetail(int iD);
        ModuleEntity ViewDetailByName(string Name);

    }
}
