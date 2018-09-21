using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
