using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DispatchManagement.Code;
using DispatchManagement.Helper;
using Framework.Configuration;
using System.Web.Hosting;
using DispatchManagement.Models;
using Framework.EF;
using Dispatch;

namespace DispatchManagement.Controllers
{
    public class FileUploadController : Controller
    {
        FilesHelper filesHelper;
        String tempPath = Config.GetConfigByKey("tempPath");
        String serverMapPath = Config.GetConfigByKey("ImagesPath");
        private string StorageRoot
        {
            get { return Path.Combine(HostingEnvironment.MapPath(serverMapPath)); }
        }
        private string UrlBase = Config.GetConfigByKey("ImagesPath");
        String DeleteURL = "/FileUpload/DeleteFile/?file=";
        String DeleteType = "GET";
        public FileUploadController()
        {
            filesHelper = new FilesHelper(DeleteURL, DeleteType, StorageRoot, UrlBase, tempPath, serverMapPath);
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Show()
        {
            JsonFiles ListOfFiles = filesHelper.GetFileList();
            var model = new FilesViewModel()
            {
                Files = ListOfFiles.files
            };

            return View(model);
        }
        [HttpGet]
        public JsonResult showImages(int pageIndex = 1, int pageSize = 10)
        {
            var iplPic = SingletonIpl.GetInstance<IplPicture>();
            int totalRow = 0;

            List<PictureEntity> listPic;

            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    dynamic deserializeResult = JsonConvert.DeserializeObject(searchString);
            //    string searchCode = deserializeResult.searchCode;
            //    string searchName = deserializeResult.searchName;
            //    int makerId = deserializeResult.makerId;
            //    productList = this._iplProducts.ListAllPaging(searchCode.Trim(), searchName.Trim(), makerId, page, pageSize, ref totalRow).ToPagedList(page, pageSize, totalRow);
            //}
            //else
            //{
            listPic = iplPic.ListAllPaging(-1, pageIndex, pageSize, ref totalRow);

            //}
            return Json(new { status = true, data = listPic, total = totalRow }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Upload()
        {
            var resultList = new List<ViewDataUploadFilesResult>();
            var iplPic = SingletonIpl.GetInstance<IplPicture>();
            var CurrentContext = HttpContext;

            filesHelper.UploadAndShowResults(CurrentContext, resultList,200,238);
            JsonFiles files = new JsonFiles(resultList);

            bool isEmpty = !resultList.Any();
            if (isEmpty)
            {
                return Json("Error ");
            }
            else
            {
                foreach (var item in resultList)
                {
                    var picEntity = new PictureEntity
                    {
                        PicturePath = item.url,
                        ThumbPath = item.thumbnailUrl,
                        MimeType = item.type,
                        SeoFilename = item.name,
                        AltAttribute = item.name,
                        TitleAttribute = item.name,
                        Album = EnumSystem.EnumPic.Album.ToString(),
                        IsDel = false,
                        Private = false,
                        PicType = (int)EnumSystem.EnumPic.Album
                    };
                    iplPic.Insert(picEntity);
                }
                return Json(files);
            }
        }
        public JsonResult GetFileList()
        {
            var list = filesHelper.GetFileList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteFile(string file)
        {
            filesHelper.DeleteFile(file);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}