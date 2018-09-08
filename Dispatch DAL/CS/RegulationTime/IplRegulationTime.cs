using Framework;
using Framework.Data;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dispatch
{
    public class IplRegulationTime : BaseIpl<ADOProvider>, IRegulationTime
    {
        public List<RegulationTimeEntity> ListAllPaging(string searchString, int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@searchString", searchString);
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<RegulationTimeEntity>("ptgroupRegulation_ListAllPaging", p);
                totalRow = p.Get<int>("@totalRow");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public bool ChangeAllowedLate(int Id, int allowedLate)
        {
            try
            {
                bool res = false;
                var p = new DynamicParameters();
                p.Add("@Id", Id);
                p.Add("@AllowedLate", allowedLate);
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_RegulationTime_ChangeAllowedLate", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public bool ChangeAllowEarly(int Id, int allowEarly)
        {
            try
            {
                bool res = false;
                var p = new DynamicParameters();
                p.Add("@Id", Id);
                p.Add("@AllowedEarly", allowEarly);
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_RegulationTime_ChangeAllowedEarly", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public bool ChangeTimesLate(int Id, int timesLate)
        {
            try
            {
                bool res = false;
                var p = new DynamicParameters();
                p.Add("@Id", Id);
                p.Add("@TimesLate", timesLate);
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_RegulationTime_ChangeTimesLate", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public bool Delete(int Id)
        {
            try
            {
                bool res = false;
                var p = new DynamicParameters();
                p.Add("@Id", Id);
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_RegulationTime_Delete", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public bool Insert(RegulationTimeEntity regulationTime)
        {
            bool flag = false;
            try
            {
                var p = new DynamicParameters();
                p.Add("@AllowedEarly", regulationTime.AllowedEarly);
                p.Add("@AllowedLate", regulationTime.AllowedLate);
                p.Add("@IdDepartment", regulationTime.IdDepartment);
                p.Add("@IdPosition", regulationTime.IdPosition);
                p.Add("@TimesLate", regulationTime.TimesLate);
                flag = (bool)unitOfWork.ProcedureExecute("ptgroup_RegulationTime_Insert", p);
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
            }
            return flag;
        }
        public List<RegulationTimeEntity> ListAll(){
            try
            {
                var data = unitOfWork.Procedure<RegulationTimeEntity>("ptgroup_RegulationTime_ListAll");
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
