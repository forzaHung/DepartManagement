using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dispatch;
using Framework.EF;

namespace DispatchManagement.Areas.BM
{
    public class FuntionInView
    {
        public static List<EmployeeEntity> listEmployee (int Id) {
            var _iplEmployee = SingletonIpl.GetInstance<IplEmployee>();
            var list = _iplEmployee.ListByIdDepart(Id);
            return list;
        }
    }
}