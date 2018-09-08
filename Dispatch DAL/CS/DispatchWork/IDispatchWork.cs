using System.Collections.Generic;

namespace Dispatch
{
    public interface IDispatchWork
    {
        List<DispatchWorkEntity> ListAllByTreeView();
        DispatchWorkEntity ViewDetail(int id);
        bool Delete(int id);
        bool Update(DispatchWorkEntity dispatchWork);
        int Insert(DispatchWorkEntity dispatchWork);
        List<Employee_DispatchWork> ListDetail(int Id);
    }
}
