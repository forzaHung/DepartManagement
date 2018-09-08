using System.Collections.Generic;

namespace Dispatch
{
    public interface IDepart_Module
	{
		int Insert(Depart_ModuleEntity depart_Module);		bool Update(Depart_ModuleEntity depart_Module);
		bool Delete(int id);
		List<Depart_ModuleEntity> ListAll();
		List<Depart_ModuleEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow);
		Depart_ModuleEntity ViewDetail(int id);
	}
}
