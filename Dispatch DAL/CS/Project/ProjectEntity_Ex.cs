using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dispatch
{

    public partial class ProjectEntity
    {
       public string CustomerName { get; set; }
        public string Address { get; set; }
        public string StatusName { get; set; }
        //[Required(ErrorMessage = "Chưa chọn vị trí lãnh đạo")]
        public List<int> SelectedIDListManager { get; set; }

        //[Required(ErrorMessage = "Chưa chọn vị trí kỹ thuật")]
        public List<int> SelectedIDListTechnical { get; set; }
        //[Required(ErrorMessage = "Chưa chọn vị trí trợ lý")]
        public List<int> SelectedIDListAssistant { get; set; }
        //[Required(ErrorMessage = "Chưa chọn vị trí mua hàng")]
        public List<int> SelectedIDListBuyer { get; set; }


    }
}

