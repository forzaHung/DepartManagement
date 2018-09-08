using System.Collections.Generic;

namespace Dispatch
{
    public interface IDepartment
	{
		int Insert(DepartmentEntity department);		bool Update(DepartmentEntity department);
		bool Delete(int id);
        List<DepartmentEntity> ListAllByTreeView();
        List<DepartmentEntity> ListAll();
		List<DepartmentEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow);
        List<DepartmentEntity> ListAllPaging(string searchString, int pageIndex, int pageSize, ref int totalRow);
        DepartmentEntity ViewDetail(int id);
	}
}
