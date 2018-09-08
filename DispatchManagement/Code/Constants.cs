using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DispatchManagement
{
    public class Constants
    {
        public const string ProductImagePath = "/Assets/products-images/";
        public const string DateFormat = "dd-MM-yyyy";
        public const string TimeFormat = " HH:mm:ss";

        public const string Admin = "Admin";
        public const string User = "User";
        public const string Add = "Add";
        public const string Edit = "Edit";
        public const string View = "View";
        public const string Delete = "Delete";
        public const string Import = "Import";
        public const string Export = "Export";
        public const string Upload = "Upload";
        public const string Save = "Publish";
        public const string Report = "Report";
        public const string Print = "Print";
    }
    public class ConstantMsg
    {
        public const string NotPermission = "Bạn không có quyền cho chức năng này. Liên hệ với quản trị viên để được cấp quyền";
        public const string ItemExists = "{0} đã tồn tại trong hệ thống.";
        public const string ErrorImageFormat = "Định dạng ảnh không hợp lệ. (jpg, png, gif)";
        public const string ErrorProgress = "Lỗi hệ thống. Liên hệ với quản trị viên để được trợ giúp";
        public const string EmailExists = "Email này đã tồn tại";
        public const string ErrorLogin = "Tên đăng nhập hoặc mật khẩu không đúng.";
        public const string DataNotFount = "Không tìm thấy dữ liệu hợp lệ!";
        public const string OldPassInvalid = "Mật khẩu cũ không đúng";
    }
}