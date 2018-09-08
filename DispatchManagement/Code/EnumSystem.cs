using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DispatchManagement
{
    public class EnumSystem
    {
        public enum EnumPic
        {
            PicCate = 1,
            PicProduct = 2,
            AvatarUser = 3,
            Album = 0
        }
        public enum EnumMenu
        {
            Top = 1,
            Left = 2
        }
        public enum AttributeControlType
        {
            Dropdownlist = 1,
            Checkbox = 2
        }

        public enum EnumOrderStatus
        {
            Created = 1,
            Inprogress = 2,
            Delivery = 3,
            Complete = 4,
            Canceled = 5
        }

        public enum EnumSettings
        {
            HotProduct = 1
        }
    }
}