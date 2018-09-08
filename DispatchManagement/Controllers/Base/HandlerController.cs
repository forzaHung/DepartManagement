using DispatchManagement;
using Framework.Configuration;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DispatchManagement.Controllers
{
    public class HandlerController : Controller
    {

        public System.Drawing.Image processImagesUrl(string imageUrl)
        {
            System.Drawing.Image image = null;

            try
            {
                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(imageUrl);
                webRequest.AllowWriteStreamBuffering = true;
                webRequest.Timeout = 30000;

                System.Net.WebResponse webResponse = webRequest.GetResponse();

                System.IO.Stream stream = webResponse.GetResponseStream();

                image = System.Drawing.Image.FromStream(stream);

                webResponse.Close();
            }
            catch (Exception ex)
            {
                Logging.PutError("ERROR Download img", ex);
                return null;
            }

            return image;
        }
        [HttpPost]
        public JsonResult DownloadImageFromUrl(string imageUrl)
        {
            var format = imageUrl.Split('.');

            System.Drawing.Image image = processImagesUrl(imageUrl);
            var ImagePath = Config.GetConfigByKey("ImagePath");
            var strSaveFolder = String.Format(DateTime.Now.ToString("\\\\yyyy\\\\MM\\\\dd\\\\"));
            string rootPath = Server.MapPath(ImagePath + strSaveFolder);
            if (System.IO.Directory.Exists(rootPath) == false)
            {
                //Create Directory
                System.IO.Directory.CreateDirectory(rootPath);
            }

            string fileName = DateTime.Now.Ticks.ToString() + "." + format[format.Count() - 1];
            string savefile = rootPath + fileName;
            if (image != null)
            {
                var bmpOut = new Bitmap(image);
                Graphics g = Graphics.FromImage(bmpOut);
                var size = Utility.GetThumbSize(image, 560);
                image = image.GetThumbnailImage(size.Width, size.Height, null, IntPtr.Zero);
                image.Save(savefile);
                return Json(new
                {
                    fileName = ImagePath + strSaveFolder + fileName
                });
            }
            else
            {
                return Json(new
                {
                    fileName = imageUrl
                });
            }


        }
    }
}