using Framework.Configuration;
using Framework.EF;
using Dispatch;
using MvcPaging;
using DispatchManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DispatchManagement.Controllers
{
    public class DispatchWorkController : BaseController
    {
        private IDispatchWork _iplDispatchWork;
        public DispatchWorkController(){
            _iplDispatchWork = SingletonIpl.GetInstance<IplDispatchWork>();
        }
        // GET: DispatchWork
        public ActionResult Index()
        {
            var department = _iplDispatchWork.ListAllByTreeView();
            return View(department);
        }
        //[AuthorizeUser(ModuleName = "Department", AccessLevel = Constants.Add)]
        public ActionResult CreateDispatchWork()
        {
            var entity = new DispatchWorkEntity();
            ViewData["ParentID"] = _iplDispatchWork.ListAllByTreeView();
            return View(entity);
        }
        public ActionResult EditDispatchWork(int? id)
        {
            List<DispatchWorkEntity> depart = _iplDispatchWork.ListAllByTreeView();
            foreach (var item in depart)
            {
                if (item.ID == id)
                {
                    List<DispatchWorkEntity> b = DepartmentChild(item.ID, depart);
                    foreach (var item2 in b) depart = depart.Where(x => x.ID != item2.ID).ToList();
                    depart = depart.Where(x => x.ID != item.ID).ToList();
                }
            }
            ViewData["ParentID"] = depart;
            DispatchWorkEntity entity = new DispatchWorkEntity();
            if (id != null)
            {
                entity = _iplDispatchWork.ViewDetail((int)id);
            }
            return View("CreateDispatchWork", entity);
        }
        public ActionResult Delete(int id)
        {
            List<DispatchWorkEntity> depart = _iplDispatchWork.ListAllByTreeView();
            foreach (var item in depart)
            {
                if (item.ID == id)
                {
                    List<DispatchWorkEntity> b = DepartmentChild(item.ID, depart);
                    b.Add(item);
                    foreach (var item2 in b)
                    {
                        var detail = _iplDispatchWork.ViewDetail(item2.ID);
                        if (detail != null && detail.ID > 0)
                        {
                            var ret = _iplDispatchWork.Delete(item2.ID);
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Save(DispatchWorkEntity model)
        {
            var entity = new DispatchWorkEntity();
            if (ModelState.IsValid)
            {
                if (model.ID > 0) //update
                {
                    entity = _iplDispatchWork.ViewDetail(model.ID);
                    if (entity != null && entity.ID > 0)
                    {
                        //get thông tin của Phòng
                        entity.ParentID = model.ParentID;
                        entity.CoefficientsSalary = model.CoefficientsSalary;
                        entity.WorkCode = model.WorkCode;
                        entity.ModifiedDate = DateTime.Now;
                        entity.WorkName = model.WorkName;
                        entity.WorkType = model.WorkType;
                        var retVal = _iplDispatchWork.Update(entity);
                        return RedirectToAction("Index", "DispatchWork");
                    }

                }
                else //insert
                {
                    model.CreatedDate = DateTime.Now;
                    var departid = _iplDispatchWork.Insert(model);
                    return RedirectToAction("Index", "DispatchWork");
                }
            }
            ViewData["ParentID"] = _iplDispatchWork.ListAllByTreeView();
            return View("CreateDispatchWork", model);
        }
        public List<DispatchWorkEntity> DepartmentChild(int id, List<DispatchWorkEntity> depart)
        {
            List<DispatchWorkEntity> con = new List<DispatchWorkEntity>();
            foreach (var item in depart)
            {
                if (item.ParentID == id)
                {
                    con.Add(item);
                    depart = depart.Where(x => x.ID != item.ID).ToList();
                    List<DispatchWorkEntity> b = DepartmentChild(item.ID, depart);
                    foreach (var item2 in b) con.Add(item2);
                }
            }
            return con;
        }
    }
}