using Framework.Configuration;
using Framework.EF;
using Dispatch;
using MvcPaging;
using DispatchManagement.Models;
using Prototype.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;


namespace DispatchManagement.Controllers
{
    public class FilesController : BaseController
    {
        private IFile _iplfile;
        private string _FilePathIn = Config.GetConfigByKey("FileDispatchIn");
        private string _FilePathOut = Config.GetConfigByKey("FileDispatchOut");
        // GET: Files
        public FilesController()
        {           
            _iplfile = SingletonIpl.GetInstance<IplFile>();
        }

        public ActionResult Index(string Code = "", string Name = "",  string Date = "", int page = 1, int pageSize = 10)
        {
            int totalRow = 0;

            ViewBag.FilePatchIn = _FilePathIn;
            ViewBag.FilePatchOut = _FilePathOut;
            var filelist = _iplfile.ListAllPaging(Code, Name, Date, page, pageSize, ref totalRow);
            return View(filelist.ToPagedList(page, pageSize, totalRow));
        }
        public PartialViewResult GetFile(string Code = "", string Name = "", string Date = "", int page = 1, int pageSize = 10)
        {
            int totalRow = 0;

            var filelist = _iplfile.ListAllPaging(Code, Name, Date, page, pageSize, ref totalRow).ToPagedList(page, pageSize, totalRow);

            return PartialView("_FileList", filelist);
        }

    }
}