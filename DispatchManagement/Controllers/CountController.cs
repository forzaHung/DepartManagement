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
using System.Web.UI;

namespace DispatchManagement.Controllers
{
    public class CountController : Controller
    {
        private IDispatchIn _ipldispatchin;
        private IDispatchOut _ipldispatchout;
        private IDepartment _iplDepartment;
        private IDispatchType _ipldispatchtype;
        private IDispatchStatus _ipldispatchstatus;
        public CountController()
        {
            _ipldispatchin = SingletonIpl.GetInstance<IplDispatchIn>();
            _ipldispatchout = SingletonIpl.GetInstance<IplDispatchOut>();
            _iplDepartment = SingletonIpl.GetInstance<IplDepartment>();
            _ipldispatchtype = SingletonIpl.GetInstance<IplDispatchType>();
            _ipldispatchstatus = SingletonIpl.GetInstance<IplDispatchStatus>();
        }
        #region DispatchIn
        [OutputCache(Duration = 300, Location = OutputCacheLocation.Client, VaryByParam = "*")]
        public ActionResult _MenuDepartMent()
        {
            var department = _iplDepartment.ListAll();
            return View(department);
        }
        [OutputCache(Duration = 300, Location = OutputCacheLocation.Client, VaryByParam = "*")]
        public ActionResult _MenuTypeDisIn()
        {
            var dispatchtype = _ipldispatchtype.ListAll();
            return View(dispatchtype);
        }
        [OutputCache(Duration = 300, Location = OutputCacheLocation.Client, VaryByParam = "*")]
        public ActionResult _MenuStatusDisIn()
        {
            var dispatchstatus = _ipldispatchstatus.ListAll();
            return View(dispatchstatus);
        }
        #endregion
        #region DispatchOut
        [OutputCache(Duration = 300, Location = OutputCacheLocation.Client, VaryByParam = "*")]
        public ActionResult _MenuDepartMentOut()
        {
            var department = _iplDepartment.ListAll();
            return View(department);
        }
        [OutputCache(Duration = 300, Location = OutputCacheLocation.Client, VaryByParam = "*")]
        public ActionResult _MenuTypeDisOut()
        {
            var dispatchtype = _ipldispatchtype.ListAll();
            return View(dispatchtype);
        }
        [OutputCache(Duration = 300, Location = OutputCacheLocation.Client, VaryByParam = "*")]
        public ActionResult _MenuStatusDisOut()
        {
            var dispatchstatus = _ipldispatchstatus.ListAll();
            return View(dispatchstatus);
        }
        #endregion

        #region Count thường
        // GET: Count
        [OutputCache(Duration = 300, Location = OutputCacheLocation.Client, VaryByParam = "*")]
        public ActionResult _Count(int searchType = 0, int searchStatus = 0, int AddressToId = 0)
        {
            var count = _ipldispatchin.CountDispatchIn(searchType, searchStatus, AddressToId);
            CountList c = new CountList();
            c.returnCount = count;
            return View(c);
        }
        [OutputCache(Duration = 300, Location = OutputCacheLocation.Client, VaryByParam = "*")]
        public ActionResult _CountOut(int searchType = 0, int searchStatus = 0, int DepartmentId = 0)
        {
            var count = _ipldispatchout.CountDispatchOut(searchType, searchStatus, DepartmentId);
            CountList c = new CountList();
            c.returnCount = count;
            return View(c);
        }
        #endregion

        #region count ajax dispatchIn
        [HttpPost]
        public JsonResult ShowMenuDepartMent(int id)
        {
            var department = _iplDepartment.ListAll();
            if (department == null)
            {
                return Json(new
                {
                    status = false
                });
            }
            else
            {
                return Json(new
                {
                    status = true,
                    data=department
                });
            }
           
        }
        public JsonResult ShowMenuStatus(int id)
        {
            var dispatchstatus = _ipldispatchstatus.ListAll();
            if (dispatchstatus == null)
            {
                return Json(new
                {
                    status = false
                });
            }
            else
            {
                return Json(new
                {
                    status = true,
                    data = dispatchstatus
                });
            }

        }
        public JsonResult ShowMenuType(int id)
        {
            var dispatchtype = _ipldispatchtype.ListAll();
            if (dispatchtype == null)
            {
                return Json(new
                {
                    status = false
                });
            }
            else
            {
                return Json(new
                {
                    status = true,
                    data = dispatchtype
                });
            }

        }
        public JsonResult CountDispatchIn(int searchType = 0, int searchStatus = 0, int AddressToId = 0)
        {
            var count = _ipldispatchin.CountDispatchIn(searchType, searchStatus, AddressToId);
         
            return Json(count);
        }
        public JsonResult CountDispatchOut(int searchType = 0, int searchStatus = 0, int DepartmentId = 0)
        {
            var count = _ipldispatchout.CountDispatchOut(searchType, searchStatus, DepartmentId);
          
            return Json(count);
        }
        #endregion
    }
}