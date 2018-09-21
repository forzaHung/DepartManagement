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
    public class IplHoliday : BaseIpl<ADOProvider>, IHoliday
    {
        public bool Insert(HolidayEntity enity) {
            bool flag = false;
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", enity.Id);
                p.Add("@HolidayDate", enity.HolidayDate);
                p.Add("@Description", enity.Description);
                flag = (bool)unitOfWork.ProcedureExecute("ptgroup_Holiday_Insert", p);
                return flag;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return flag;
            }
        }
        public bool Update(HolidayEntity enity)
        {
            bool flag = false;
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", enity.Id);
                p.Add("@HolidayDate", enity.HolidayDate);
                p.Add("@Description", enity.Description);
                flag = (bool)unitOfWork.ProcedureExecute("ptgroup_Holiday_Update", p);
                return flag;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return flag;
            }
        }
        public bool ChangeDescription(long Id, string description) {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", Id);
                p.Add("@Description", description);
                var data = (bool)unitOfWork.ProcedureExecute("ptgroup_Holiday_ChangeDescription", p);
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public bool Delete(long Id)
        {
            bool flag = false;
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", Id);
                flag = (bool)unitOfWork.ProcedureExecute("ptgroup_Holiday_Delete", p);
                return flag;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return flag;
            }
        }
        public List<HolidayEntity> ListHolidayAllPaging(DateTime fromDate, DateTime toDate, int pageIndex, int pageSize, ref int totalRow) {
            try
            {
                var p = new DynamicParameters();
                p.Add("@fromDate", fromDate);
                p.Add("@toDate", toDate);
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<HolidayEntity>("ptgroup_Holiday_AllPaging", p);
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public HolidayEntity Detail(DateTime HolidayDate){
            try
            {
                var p = new DynamicParameters();
                p.Add("@HolidayDate", HolidayDate);
                var data = unitOfWork.Procedure<HolidayEntity>("ptgroup_Holiday_Detail",p).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
    }
}
