using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatch
{
    public interface IBookMeeting
    {
        int Insert(BookMeetingEntity bookmeeting);
        bool Update(BookMeetingEntity bookmeeting);
        bool Delete(int id);
        BookMeetingEntity Detail(int id);
        List<BookMeetingEntity> DetailWithRoomId(int id);
        List<BookMeetingEntity> ListAll();
        List<BookMeetingEntity> ListByPaging(DateTime? searchDate, string keyword, int pageIndex, int pageSize, ref int totalRow);
        List<BookMeetingEntity> ListAllPagingTime(DateTime searchDate, int pageIndex, int pageSize, ref int totalRow);
    }
}
