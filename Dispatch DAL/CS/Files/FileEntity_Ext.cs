using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Dispatch
{
   public partial class FileEntity
    {       
        
        public string disInNo { get; set; }
        public string disOutNo { get; set; }
        public string DisOutName { get; set; }
        public string DisInName { get; set; }
        public string UserName { get; set; }
       
    }
}
