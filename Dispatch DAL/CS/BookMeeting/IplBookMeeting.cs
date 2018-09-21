using Framework;
using Framework.Data;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatch
{
    public class IplBookMeeting : BaseIpl<ADOProvider>, IBookMeeting
    {
        public bool Delete(int id)
        {
            try
            {
                bool res = false;
                var p = new DynamicParameters();
                p.Add("@Id", id);
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_BookMeeting_Delete", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }

        public BookMeetingEntity Detail(int id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);
                var data = unitOfWork.Procedure<BookMeetingEntity>("ptgroup_BookMeeting_Viewdetail", p).SingleOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }

        public int Insert(BookMeetingEntity bookMeeting)
        {
            int res = 0;
            bool flag = false;
            try
            {
                var p = Param(bookMeeting);
                flag = (bool)unitOfWork.ProcedureExecute("ptgroup_BookMeeting_Insert", p);
                if (flag)
                {
                    res = p.Get<int>("@Id");
                }
                else
                {
                    res = 0;
                }
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
            }
            return res;
        }

        public List<BookMeetingEntity> ListAll()
        {
            try
            {
                var data = unitOfWork.Procedure<BookMeetingEntity>("ptgroup_BookMeeting_GetAll");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public List<BookMeetingEntity> ListByPaging(DateTime? searchDate, string keyword, int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@searchDate", searchDate);
                p.Add("@keyword", keyword);
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<BookMeetingEntity>("ptgroup_BookMeeting_ListByPaging", p);
                totalRow = p.Get<int>("@totalRow");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }

        public bool Update(BookMeetingEntity bookMeeting)
        {
            bool res = false;
            try
            {
                var p = Param(bookMeeting, "edit");
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_BookMeeting_Update", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }


        public DynamicParameters Param(BookMeetingEntity bookMeeting, string action = "add")
        {
            var p = new DynamicParameters();
            if (action == "edit")
            {
                p.Add("@Id", bookMeeting.Id);
                p.Add("@ModifierDate", bookMeeting.ModifierDate);
            }
            else
            {
                p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@CreateDate", bookMeeting.CreateDate);
            }
            p.Add("@DepartmentId", bookMeeting.DepartmentId);
            p.Add("@CreateDate", bookMeeting.CreateDate);
            p.Add("@EmployeeId", bookMeeting.EmployeeId);

            p.Add("@TimeStart", bookMeeting.TimeStart);
            p.Add("@TimeEnd", bookMeeting.TimeEnd);
            p.Add("@RoomId", bookMeeting.RoomID);

            return p;
        }

        public List<BookMeetingEntity> DetailWithRoomId(int id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@id", id);
                var data = unitOfWork.Procedure<BookMeetingEntity>("ptgroup_BookMeeting_ViewdetailWithRoomId", p).ToList();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }

        public List<BookMeetingEntity> ListAllPagingTime(DateTime searchDate, int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@searchDate", searchDate);
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<BookMeetingEntity>("ptgroup_BookMeeting_ListByPagingTime", p);
                totalRow = p.Get<int>("@totalRow");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
    }
}
