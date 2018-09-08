using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Framework.Configuration;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace DispatchManagement
{
    public static class Utility
    {
        #region format datetime
        /// <summary>
        /// Convert string to datetime with current format dd/MM/yyyy or MM/dd/yyyy. input default format dd/MM/yyyy
        /// </summary>
        /// <param name="date"></param>
        /// <param name="strSpilt"></param>
        /// <param name="currentformat"></param>
        /// <returns></returns>
        public static DateTime convertDateToDateTimeMDY(string date, char strSpilt, string currentformat = "dd/MM/yyyy")
        {
            try
            {
                var tmpDate = date.Split(strSpilt);

                if (currentformat.ToLower().Equals("dd/mm/yyyy") || currentformat.ToLower().Equals("mm/dd/yyyy"))
                {
                    CultureInfo provider = new CultureInfo("fr-FR");
                    return DateTime.ParseExact(tmpDate[0] + "/" + tmpDate[1] + "/" + tmpDate[2], currentformat, provider);
                }
                else
                {
                    CultureInfo provider = new CultureInfo("en-US");
                    return DateTime.ParseExact(tmpDate[1] + "/" + tmpDate[0] + "/" + tmpDate[2], currentformat, provider);
                }
            }
            catch { return DateTime.Now; }
        }
        public static DateTime? convertToDateTime(string date, char strSpilt = '/', string currentformat = "dd/MM/yyyy")
        {
            try
            {
                if (date.IndexOf("01/01/0001") != -1)
                {
                    return null;
                }
                var tmpDate = date.Split(strSpilt);

                if (currentformat.ToLower().Equals("dd/mm/yyyy") || currentformat.ToLower().Equals("mm/dd/yyyy"))
                {
                    CultureInfo provider = new CultureInfo("fr-FR");
                    return DateTime.ParseExact(tmpDate[0] + "/" + tmpDate[1] + "/" + tmpDate[2], currentformat, provider);
                }
                else
                {
                    CultureInfo provider = new CultureInfo("en-US");
                    return DateTime.ParseExact(tmpDate[1] + "/" + tmpDate[0] + "/" + tmpDate[2], currentformat, provider);
                }
            }
            catch { return null; }
        }
        #endregion
        #region import file        
        public static Dictionary<int, string> RowToDictionary(this IXLRangeRow row)
        {
            var dictResult = new Dictionary<int, string>();
            var columnCount = row.CellCount();
            for (int i = 1; i <= columnCount; i++)
            {
                dictResult.Add(i, row.Cell(i).GetString());
            }
            return dictResult;
        }
        #endregion
        #region upload file
        public static UploadFileClass upload(string mediaPath, HttpPostedFileBase file, byte type = 1)
        {

            UploadFileClass objUpload = new UploadFileClass();
            objUpload = Utility.GetUploadFile(mediaPath, file.FileName, true);
            file.SaveAs(objUpload.Fullpath);
            if (type == 1)
            {
                objUpload.pathThumb = Utility.thumbImg(objUpload.StrPathTemp, objUpload.FolderPath, file.FileName);
                Utility.thumbImg(objUpload.StrPathTemp, objUpload.FolderPath, file.FileName, 185, 111, false);
            }
            return objUpload;
        }
        public static UploadFileClass GetUploadFile(string MediaPath, string strFileName, bool AddDatePath)
        {
            UploadFileClass tempUploadClass = new UploadFileClass();
            System.Text.RegularExpressions.Match matchResults;
            string strAdditionFolder = (AddDatePath ? String.Format(DateTime.Now.ToString("\\\\yyyy\\\\MM\\\\dd\\\\")) : "");
            string strSaveFile = strFileName;
            string strSaveFolder = MediaPath + strAdditionFolder;
            //Check folder exist
            try
            {
                if (System.IO.Directory.Exists(strSaveFolder) == false)
                {
                    //Create Directory
                    System.IO.Directory.CreateDirectory(strSaveFolder);
                }
                if (System.IO.File.Exists(strSaveFolder + strSaveFile))
                {
                    while (System.IO.File.Exists(strSaveFolder + strSaveFile))
                    {
                        matchResults = System.Text.RegularExpressions.Regex.Match(strSaveFile, "(?<FileName>.*?)(?:\\((?<AutoNumber>\\d*?)\\))?\\.(?<FileType>\\w*?)(?!.)");
                        if (matchResults.Success)
                        {
                            if (matchResults.Groups["AutoNumber"].Value == string.Empty)
                            {
                                strSaveFile = matchResults.Groups["FileName"].Value + "(1)." + matchResults.Groups["FileType"].Value;
                            }
                            else
                            {
                                strSaveFile = matchResults.Groups["FileName"].Value + "(" + (int.Parse(matchResults.Groups["AutoNumber"].Value) + 1).ToString() + ")." + matchResults.Groups["FileType"].Value;
                            }
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            tempUploadClass.Virtualpath = strAdditionFolder.Replace("\\", "/") + strSaveFile;
            tempUploadClass.FileName = strSaveFile;
            tempUploadClass.Fullpath = strSaveFolder + strSaveFile;
            tempUploadClass.FolderPath = strAdditionFolder.Replace("\\", "/");
            tempUploadClass.StrPathTemp = strSaveFolder;
            return tempUploadClass;
        }
        public static UploadFileClass GetUploadFile(string MediaPath, string strFileName, bool AddDatePath, string UID)
        {
            UploadFileClass tempUploadClass = new UploadFileClass();
            System.Text.RegularExpressions.Match matchResults;
            string strAdditionFolder = (AddDatePath ? String.Format(DateTime.Now.ToString("\\\\yyyy\\\\MM\\\\dd\\\\")) : UID);
            string strSaveFile = strFileName;
            string strSaveFolder = MediaPath + strAdditionFolder;
            //Check folder exist
            try
            {
                if (System.IO.Directory.Exists(strSaveFolder) == false)
                {
                    //Create Directory
                    System.IO.Directory.CreateDirectory(strSaveFolder);
                }
                if (System.IO.File.Exists(strSaveFolder + strSaveFile))
                {
                    while (System.IO.File.Exists(strSaveFolder + strSaveFile))
                    {
                        matchResults = System.Text.RegularExpressions.Regex.Match(strSaveFile, "(?<FileName>.*?)(?:\\((?<AutoNumber>\\d*?)\\))?\\.(?<FileType>\\w*?)(?!.)");
                        if (matchResults.Success)
                        {
                            if (matchResults.Groups["AutoNumber"].Value == string.Empty)
                            {
                                strSaveFile = matchResults.Groups["FileName"].Value + "(1)." + matchResults.Groups["FileType"].Value;
                            }
                            else
                            {
                                strSaveFile = matchResults.Groups["FileName"].Value + "(" + (int.Parse(matchResults.Groups["AutoNumber"].Value) + 1).ToString() + ")." + matchResults.Groups["FileType"].Value;
                            }
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            tempUploadClass.Virtualpath = strAdditionFolder.Replace("\\", "/") + strSaveFile;
            tempUploadClass.FileName = strSaveFile;
            tempUploadClass.Fullpath = strSaveFolder + strSaveFile;
            tempUploadClass.FolderPath = strAdditionFolder.Replace("\\", "/");
            tempUploadClass.StrPathTemp = strSaveFolder;
            return tempUploadClass;
        }
        public static bool CheckfileUpload(string fileName)
        {
            bool ret = false;
            string name = "";
            name = Path.GetExtension(fileName.ToLower());
            switch (name.ToLower())
            {
                case ".jpg":
                    ret = true;
                    break;
                case ".png":
                    ret = true;
                    break;
                case ".gif":
                    ret = true;
                    break;
                case ".bmp":
                    ret = true;
                    break;
                case ".doc":
                    ret = true;
                    break;
                case ".docx":
                    ret = true;
                    break;
                case ".xls":
                    ret = true;
                    break;
                case ".xlsx":
                    ret = true;
                    break;
                case ".rar":
                    ret = true;
                    break;
                case ".zip":
                    ret = true;
                    break;
                case ".pdf":
                    ret = true;
                    break;
            }
            return (ret);
        }

        public static bool CheckImageFormat(string filename)
        {
            bool ret = false;
            string name = "";
            name = Path.GetExtension(filename.ToLower());
            switch (name.ToLower())
            {
                case ".jpg":
                    ret = true;
                    break;
                case ".png":
                    ret = true;
                    break;
                case ".gif":
                    ret = true;
                    break;
                case ".bmp":
                    ret = true;
                    break;
            }
            return (ret);
        }

        public static Size GetThumbSize(System.Drawing.Image original, int maxPixels)
        {
            int originalWidth = original.Width;
            int originalHeight = original.Height;
            double factor;
            if (originalWidth > originalHeight)
            {
                factor = (double)maxPixels / originalWidth;
            }
            else
            {
                factor = (double)maxPixels / originalHeight;
            }
            return new Size((int)(originalWidth * factor), (int)(originalHeight * factor));
        }
        /// <summary>
        /// Thumb images. Default W = 100;
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="virtualPath"></param>
        /// <param name="fileName"></param>
        /// <param name="tbwidth"></param>
        /// <param name="tbheight"></param>
        /// <param name="autoSize"></param>
        /// <returns></returns>
        public static string thumbImg(string fullPath, string virtualPath, string fileName, int tbwidth = 0, int tbheight = 0, bool autoSize = true)
        {
            int width = 0;
            int height = 0;
            // Thumb image
            System.Drawing.Image image = System.Drawing.Image.FromFile(fullPath + fileName);
            Size thumbSize = Utility.GetThumbSize(image, 100);
            if (autoSize)
            {
                width = thumbSize.Width;
                height = thumbSize.Height;
            }
            else
            {
                width = tbwidth;
                height = tbheight;
            }
            System.Drawing.Image thumb = image.GetThumbnailImage(width, height, null, IntPtr.Zero);
            var thumbPath = System.IO.Path.Combine(virtualPath, "thumb_" + width + "_" + fileName);
            var savePath = System.IO.Path.Combine(fullPath, "thumb_" + width + "_" + fileName);
            thumb.Save(savePath);
            thumb.Dispose();
            image.Dispose();
            // End thumb
            return thumbPath;
        }
        #endregion
        #region process text and dayofweek
        public static string Change_AV(string ip_str_change)
        {

            Regex v_reg_regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string v_str_FormD = ip_str_change.Normalize(NormalizationForm.FormD);
            var str = v_reg_regex.Replace(v_str_FormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            str = str.Replace(" ", "-");
            str = str.Replace("?", "");
            str = str.Replace("@", "");
            str = str.Replace(".", "");
            str = str.Replace(":", "");
            str = str.Replace("&", "");
            str = str.Replace(",", "");
            str = str.Replace("/", "");
            str = str.Replace("%", "");
            str = str.Replace("'", "");

            return str.Trim();
        }
        public static string ReturnThu(DayOfWeek dow)
        {

            switch (dow)
            {
                case DayOfWeek.Monday:
                    return "Thứ hai";
                    break;
                case DayOfWeek.Tuesday:
                    return "Thứ ba";
                    break;
                case DayOfWeek.Wednesday:
                    return "Thứ tư";
                    break;
                case DayOfWeek.Thursday:
                    return "Thứ năm";
                    break;
                case DayOfWeek.Friday:
                    return "Thứ sáu";
                    break;
                case DayOfWeek.Saturday:
                    return "Thứ bảy";
                    break;
                case DayOfWeek.Sunday:
                    return "Chủ Nhật";
                    break;
                default:
                    return dow.ToString();
                    break;
            }
        }
        public static string RandomCode(int lenght = 6)
        {
            Random random = new Random();

            string s = "";
            for (int i = 0; i < lenght; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
        /// <summary>
        /// Gens the file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="format">The format.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool GenFile(string path, string fileName, string format)
        {

            string FilePath = path + fileName;
            StreamWriter sw;
            try
            {
                sw = File.CreateText(FilePath);
                sw.WriteLine(format, Encoding.UTF8);
                sw.Flush();
                sw.Close();
                sw.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Logging.PutError("Error gen Adv:", ex);
            }
            return false;
        }
        /// <summary>
        /// Reads the menu to file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>System.String.</returns>
        public static string ReadMenuToFile(string path, string fileName)
        {
            try
            {
                string filePath = path + fileName;
                StreamReader sr = File.OpenText(filePath);
                string readText = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
                return readText;
            }
            catch (Exception ex)
            {
                Logging.PutError("Cannot read file adv:", ex);
            }
            return "";
        }
        #endregion
        #region Detech mobile

        public static bool checkMobile()
        {
            string u = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
            Regex b = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Regex v = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if ((b.IsMatch(u) || v.IsMatch(u.Substring(0, 4))))
            {
                return true;
            }
            return false;
        }
        #endregion
        #region CacheASP.NET
        /// <summary>
        /// Lưu cache với thời gian nhất định
        /// </summary>
        /// <param name="key">Từ khóa</param>
        /// <param name="value">giá trị</param>
        /// <param name="TimeMinute">thời gian hủy cache</param>
        public static void AddCacheAbsoluteExpiration(string key, object value, int TimeMinute)
        {
            if (value != null)
                HttpContext.Current.Cache.Insert(key, value, null, DateTime.Now.AddMinutes(TimeMinute), TimeSpan.Zero);
        }
        /// <summary>
        /// Lưu cache với thời gian ko dùng tới
        /// </summary>
        /// <param name="key">Từ khóa</param>
        /// <param name="value">giá trị</param>
        /// <param name="TimeMinute">thời gian ko truy xuất cache</param>
        public static void AddCacheSlidingExpiration(string key, object value, int TimeMinute)
        {
            HttpContext.Current.Cache.Insert(key, value, null, DateTime.MaxValue, TimeSpan.FromMinutes(TimeMinute));
        }
        public static object GetCache(string key)
        {
            return HttpContext.Current.Cache.Get(key);
        }
        /// <summary>
        /// remove cache by key
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveCache(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }
        #endregion
        #region Cookie ASP.NET
        /// <summary>
        /// Creates the cookie.
        /// </summary>
        /// <param name="cookieName">Name of the cookie.</param>
        /// <param name="value">The value.</param>
        /// <param name="expires">The expires defaule 30 minute.</param>
        public static void CreateCookie(string cookieName, string value, int expires = 30)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Value = value;
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        /// <summary>
        /// Removes the cookie.
        /// </summary>
        /// <param name="cookieName">Name of the cookie.</param>
        public static void RemoveCookie(string cookieName)
        {
            if (HttpContext.Current.Request.Cookies.AllKeys.Contains(cookieName))
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        public static HttpCookie GetCookie(string cookieName)
        {
            if (HttpContext.Current.Request.Cookies.AllKeys.Contains(cookieName))
            {
                return HttpContext.Current.Request.Cookies[cookieName];
            }
            return null;
        }
        #endregion
        public static string GetUserIP()
        {
            System.Web.HttpContext context = HttpContext.Current;
            string visitorIPAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (String.IsNullOrEmpty(visitorIPAddress))
                visitorIPAddress = context.Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(visitorIPAddress))
                visitorIPAddress = context.Request.UserHostAddress;

            return visitorIPAddress.ToString();
        }

        #region Message language
        /// <summary>
        /// Returns the message resource.
        /// </summary>
        /// <param name="keyResource">The key resource.</param>
        /// <returns>System.String.</returns>
        public static string ReturnMessageResource(string keyResource)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var val = (string)HttpContext.GetGlobalResourceObject("Language", keyResource, culture);
            return val;
        }
        #endregion
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
     (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> knownKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (knownKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
    public class UploadFileClass
    {
        private string fileName;

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        private string fullpath;

        public string Fullpath
        {
            get { return fullpath; }
            set { fullpath = value; }
        }
        private string virtualpath;

        public string Virtualpath
        {
            get { return virtualpath; }
            set { virtualpath = value; }
        }

        private string folderPath;

        public string FolderPath
        {
            get { return folderPath; }
            set { folderPath = value; }
        }

        private string strPathTemp;

        public string StrPathTemp
        {
            get { return strPathTemp; }
            set { strPathTemp = value; }
        }
        public string pathThumb { get; set; }
    }
}