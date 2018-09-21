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
    public class IplRoom : BaseIpl<ADOProvider>, IRoom
    {
        public bool Delete(int id)
        {
            try
            {
                bool res = false;
                var p = new DynamicParameters();
                p.Add("@Id", id);
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_Room_Delete", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }

        public List<RoomEntity> GetAll()
        {
            try
            {
                var data = unitOfWork.Procedure<RoomEntity>("ptgroup_Room_GetAll");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }

        public bool Insert(RoomEntity room)
        {
            bool flag = false;
            try
            {
                var p = new DynamicParameters();
                p.Add("@Name", room.Name);
                flag = (bool)unitOfWork.ProcedureExecute("ptgroup_Room_Insert", p);
               
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
            }
            return flag;
        }

        public bool Update(RoomEntity room)
        {
            bool res = false;
            try
            {
                var p = Param(room, "edit");
                res = (bool)unitOfWork.ProcedureExecute("ptgroup_Room_Update", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }

        public RoomEntity ViewDetail(int id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);
                var data = unitOfWork.Procedure<RoomEntity>("ptgroup_Room_ViewDetail", p).SingleOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }


        private DynamicParameters Param(RoomEntity room, string action = "add")
        {
            var p = new DynamicParameters();
            if (action == "edit")
            {
                p.Add("@Id", room.Id);
            }
            else
            {
                p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            }

            p.Add("@Name", room.Name);
            return p;
        }
    }
}
