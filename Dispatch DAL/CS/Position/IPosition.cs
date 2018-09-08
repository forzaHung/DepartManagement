using System.Collections.Generic;

namespace Dispatch
{
    public interface IPosition
    {
		int Insert(PositionEntity employee_history);
        bool Update(PositionEntity employee_history);
		bool Delete(int id);
		List<PositionEntity> ListAll();        
        List<PositionEntity> ListAllPaging(int pageIndex, int pageSize, ref int totalRow);
        List<PositionEntity> ListAllPaging(string searchString, int pageIndex, int pageSize, ref int totalRow);
        PositionEntity ViewDetail(int id);
	}
}
