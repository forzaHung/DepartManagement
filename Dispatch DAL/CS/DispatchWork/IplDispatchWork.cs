using Framework;
using Framework.Data;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dispatch
{
    public class IplDispatchWork : BaseIpl<ADOProvider>, IDispatchWork
    {
        public List<DispatchWorkEntity> ListAllByTreeView()
        {
            try
            {
                var data = unitOfWork.Procedure<DispatchWorkEntity>("ptgroup_DispatchWork_ListAllDispatchWorkByTreeView");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public DispatchWorkEntity ViewDetail(int id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);
                var data = unitOfWork.Procedure<DispatchWorkEntity>("ptgroup_DispatchWork_ViewDetail", p).SingleOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                bool res = false;
                var p = new DynamicParameters();
                p.Add("@Id", id);
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_DispatchWork_Delete", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public int Insert(DispatchWorkEntity dispatchWork)
        {
            int res = 0;
            bool flag = false;
            try
            {
                var p = Param(dispatchWork);
                flag = (bool)unitOfWork.ProcedureExecute("ptgroup_DispatchWork_Insert", p);
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
                throw;
            }
            return res;
        }
        public bool Update(DispatchWorkEntity dispatchWork)
        {
            bool res = false;
            try
            {
                var p = Param(dispatchWork, "edit");
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_DispatchWork_Update", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }
        private DynamicParameters Param(DispatchWorkEntity dispatchWork, string action = "add")
        {
            var p = new DynamicParameters();
            if (action == "edit")
            {
                p.Add("@Id", dispatchWork.ID);
                p.Add("@ModifiedDate", dispatchWork.ModifiedDate);
            }
            else
            {
                p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@CreatedDate", dispatchWork.CreatedDate);
            }
            p.Add("@CoefficientsSalary", dispatchWork.CoefficientsSalary);
            p.Add("@ParentID", dispatchWork.ParentID);
            p.Add("@WorkCode", dispatchWork.WorkCode);
            p.Add("@WorkName", dispatchWork.WorkName);
            p.Add("@WorkType", dispatchWork.WorkType);

            return p;
        }
        public List<Employee_DispatchWork> ListDetail(int Id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", Id);
                var Data = unitOfWork.Procedure<Employee_DispatchWork>("ptgroupEmployee_DispatchWork_ListAllById", p).ToList();
                return Data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
    }
}
