using Dispatch;
using DispatchManagement.Controllers;
using Framework.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DispatchManagement.Areas.BM.Controllers
{
    public class RoomsController : Controller
    {
        private IRoom _iplRoom;

        public RoomsController()
        {
            _iplRoom = SingletonIpl.GetInstance<IplRoom>();
        }
        // GET: BookMeeting/BookMeetings
        public ActionResult Index()
        {
            var rooms = _iplRoom.GetAll();
            return View(rooms);
        }
        public ActionResult Save(FormCollection colection)
        {
            int Id = 0;
            if (colection["IdRoom"] != "" || !string.IsNullOrWhiteSpace(colection["IdRoom"]))
                Id = int.Parse(colection["IdRoom"]);
            var entity = new RoomEntity();
            if (ModelState.IsValid)
            {
                if (Id > 0)
                {
                    entity = _iplRoom.ViewDetail(int.Parse(colection["IdRoom"]));
                    if (entity != null && entity.Id > 0)
                    {
                        entity.Name = colection["NameRoom"];
                        var retVal = _iplRoom.Update(entity);
                        if (retVal)
                        {
                            return RedirectToAction("Index", "Rooms");
                        }
                    }
                }
                else
                {
                    entity.Name = colection["NameRoom"];
                    var roomid = _iplRoom.Insert(entity);
                    if (roomid)
                    {
                        return RedirectToAction("Index", "Rooms", new { id = roomid });
                    }
                }
            }

            ViewBag.Msg = ConstantMsg.ErrorProgress;
            return View("CreateRoom", entity);
        }
        public ActionResult Edit(int? id)
        {
            RoomEntity entity = new RoomEntity();
            if (id != null)
            {
                entity = _iplRoom.ViewDetail(id.Value);
            }
            return RedirectToAction("Index", "BM/Rooms");
        }
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var ret = _iplRoom.Delete((int)id);
            }
            return RedirectToAction("Index", "BM/Rooms");
        }
    }
}