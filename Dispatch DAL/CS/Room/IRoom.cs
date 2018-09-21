using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatch
{
    public interface IRoom
    {
        bool Insert(RoomEntity room);
        bool Update(RoomEntity room);
        bool Delete(int id);
        RoomEntity ViewDetail(int id);
        List<RoomEntity> GetAll();
    }
}
