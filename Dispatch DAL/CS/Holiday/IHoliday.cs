using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatch
{
    public interface IHoliday
    {
        bool Insert(HolidayEntity enity);
        bool Update(HolidayEntity enity);
        bool ChangeDescription(long Id, string description);
        bool Delete(long Id);
        List<HolidayEntity> ListHolidayAllPaging(DateTime fromDate, DateTime toDate, int pageIndex, int pageSize, ref int totalRow);
        HolidayEntity Detail(DateTime HolidayDate);
    }
}
