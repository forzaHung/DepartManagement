using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatch
{
    public partial class Project_RoleEntity
    {
        public string Name { get; set; }
        public int mId { get; set; }
        public string FullName { get; set; }
        public string PositionName { get; set; }
        public int SelectFullName { get; set; }
        public int SelectPosition { get; set; }
    }
}
