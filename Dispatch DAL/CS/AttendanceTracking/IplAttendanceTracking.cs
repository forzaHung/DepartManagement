using Framework;
using Framework.Data;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dispatch
{
    public class IplAttendanceTracking : BaseIpl<ADOProvider>, IAttendanceTracking
    {
        public bool Insert(string xml)
        {
            bool flag = false;
            try
            {
                var p = new DynamicParameters();
                p.Add("@xml", xml);
                flag = (bool)unitOfWork.ProcedureExecute("ptgroup_AttendanceTracking_Import", p);
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
            }
            return flag;
        }
        public List<AttendanceTrackingEntity> ListAttendanceLastMonth(DateTime fromDate, DateTime toDate,string employeeId, int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@fromDate", fromDate);
                p.Add("@toDate", toDate);
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@employeeId", employeeId);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<AttendanceTrackingEntity>("ptgroupAttendanceTracking_ListLastMonth", p);
                totalRow = p.Get<int>("@totalRow");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public List<AttendanceTrackingEntity> ListAll(DateTime fromDate, DateTime toDate, int employeeId)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@fromDate", fromDate);
                p.Add("@toDate", toDate);
                p.Add("@employeeId", employeeId);
                var data = unitOfWork.Procedure<AttendanceTrackingEntity>("ptgroupAttendanceTracking_ListAll", p);
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
