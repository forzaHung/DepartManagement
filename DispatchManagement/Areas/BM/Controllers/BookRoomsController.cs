using Dispatch;
using DispatchManagement.Controllers;
using DispatchManagement.Helper;
using Framework.EF;
using MvcPaging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Mvc;

namespace DispatchManagement.Areas.BM.Controllers
{
    public class BookRoomsController : Controller
    {
        private IDepartment _iplDepartment;
        private IEmployee _iplEmployee;
        private IRoom _iplRoom;
        private IBookMeeting _iplBookMeeting;

        public BookRoomsController()
        {
            _iplDepartment = SingletonIpl.GetInstance<IplDepartment>();
            _iplEmployee = SingletonIpl.GetInstance<IplEmployee>();
            _iplRoom = SingletonIpl.GetInstance<IplRoom>();
            _iplBookMeeting = SingletonIpl.GetInstance<IplBookMeeting>();
        }
        // GET: BookMeeting/BookRooms
        public ActionResult Index()
        {
            //var entity = _iplBookMeeting.ListAll();
            return View();
        }
        public JsonResult GetBookroomsList()
        {
            DateTime? searchDate; //= null;
            string keyword = Request.QueryString["keyword"];
            int pageIndex = int.Parse(Request.QueryString["offset"]);
            int pageSize = int.Parse(Request.QueryString["limit"]);
            string fromDate = Request.QueryString["searchDate"];

            //searchDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            int totalRow = 0;
            if (string.IsNullOrEmpty(fromDate))

                //searchDate = default(DateTime);
                //searchDate = new DateTime();
                searchDate = null;

            else
                searchDate = Convert.ToDateTime(fromDate);
            var list = _iplBookMeeting.ListByPaging(searchDate, keyword, pageIndex + 1, pageSize, ref totalRow);
            if (list != null && list.Count > 0)
                return Json(new
                {
                    status = true,
                    rows = list,
                }, JsonRequestBehavior.AllowGet);
            else
                return Json(new
                {
                    status = false,
                    mess = "Không tìm thấy dữ liệu.",
                }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBookRooms(DateTime searchDate, int page = 1, int pageSize = 10)
        {
            string fromDate = Request.QueryString["searchDate"];
            if (string.IsNullOrWhiteSpace(fromDate))
            {
                searchDate = DateTime.ParseExact(fromDate, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            int totalRow = 0;
            var list = this._iplBookMeeting.ListAllPagingTime(searchDate, page, pageSize, ref totalRow);
            if (list != null && list.Count > 0)
                return Json(new
                {
                    status = true,
                    rows = list,
                }, JsonRequestBehavior.AllowGet);
            else
                return Json(new
                {
                    status = false,
                    mess = "Không tìm thấy dữ liệu.",
                }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create(BookMeetingEntity entity)
        {
            ListDropDown();
            return View(entity);
        }
        public ActionResult Edit(int? id)
        {
            var entity = new BookMeetingEntity();
            ListDropDown();
            if (id != null)
            {
                entity = _iplBookMeeting.Detail((int)id);
            }
            return View("Create", entity);
        }
        public JsonResult Delete(int Id)
        {
            if (_iplBookMeeting.Delete(Id))
                return Json(new { stt = true }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { stt = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Save(BookMeetingEntity model)
        {
            ListDropDown();
            var entity = new BookMeetingEntity();
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    entity = _iplBookMeeting.Detail(model.Id);
                    if (entity != null && entity.Id > 0)
                    {
                        entity.DepartmentId = model.DepartmentId;
                        entity.EmployeeId = model.EmployeeId;
                        entity.RoomID = model.RoomID;
                        entity.CreateDate = model.CreateDate;
                        entity.ModifierDate = DateTime.Now;
                        entity.TimeStart = model.TimeStart;
                        entity.TimeEnd = model.TimeEnd;
                        var retVal = _iplBookMeeting.Update(entity);
                        if (retVal)
                        {
                            return RedirectToAction("Index", "BookRooms");
                        }
                    }
                }
                else
                {
                    var bookmeetingid = _iplBookMeeting.Insert(model);
                    if (bookmeetingid > 0)
                    {
                        return RedirectToAction("Index", "BookRooms", new { id = bookmeetingid });
                    }
                }
            }

            ViewBag.Msg = ConstantMsg.ErrorProgress;
            return View("Create", model);
        }
        public void ListDropDown()
        {
            var DepartmentAll = _iplDepartment.ListAllByTreeView();
            ViewData["DepartmentAll"] = DepartmentAll;

            var RoomAll = _iplRoom.GetAll();
            ViewData["RoomAll"] = RoomAll;
        }
        public JsonResult GetlistEmployee(int Id)
        {
            var list = _iplEmployee.ListByIdDepart(Id);
            if (list != null && list.Count > 0)
                return Json(new { stt = true, list = list }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { stt = false }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ValidateBookRoom(int? EmployeeId, int? DepartmentId, int? RoomId, DateTime? dateRoom, DateTime? dateStart, DateTime? dateEnd, int? bookRoomId)
        {
            if (DepartmentId == null || EmployeeId == null || RoomId == null || dateRoom.Value == default(DateTime) || dateStart.Value == default(DateTime) || dateEnd.Value == default(DateTime))
            {
                return Json(new { status = false, mess = "Chọn trường bắt buộc" });
            }

            List<BookMeetingEntity> roomEntity = _iplBookMeeting.DetailWithRoomId(RoomId.Value);

            if (bookRoomId != 0)
            {
                if (DateTime.Compare(dateEnd.Value, dateStart.Value) < 0)
                {
                    return Json(new { status = false, mess = "Giờ kết thúc không nhỏ hơn giờ bắt đầu" });
                }
                foreach (var item in roomEntity)
                {
                    if (item.Id == bookRoomId)
                    {
                        return Json(new { status = true });
                    }
                    if (DateTime.Compare(dateRoom.Value, item.CreateDate) == 0)
                    {
                        if (RoomId == item.RoomID)
                        {
                            if (DateTime.Compare(dateStart.Value, item.TimeStart) == 0 && DateTime.Compare(dateStart.Value, item.TimeEnd) < 0)
                            {
                                return Json(new { status = false, mess = "Phòng đã được đặt" });
                            }
                            if (DateTime.Compare(dateStart.Value, item.TimeStart) > 0 && DateTime.Compare(dateStart.Value, item.TimeEnd) < 0)
                            {
                                return Json(new { status = false, mess = "Phòng đã được đặt" });
                            }
                            if (DateTime.Compare(dateEnd.Value, item.TimeStart) > 0 && DateTime.Compare(dateEnd.Value, item.TimeEnd) < 0)
                            {
                                return Json(new { status = false, mess = "Phòng đã được đặt" });
                            }
                            if (DateTime.Compare(dateStart.Value, item.TimeStart) < 0 && DateTime.Compare(dateEnd.Value, item.TimeEnd) > 0)
                            {
                                return Json(new { status = false, mess = "Phòng đã được đặt" });
                            }
                        }
                    }
                }
            }
            else
            {
                if (roomEntity != null)
                {
                    foreach (var item in roomEntity)
                    {
                        if (DateTime.Compare(dateRoom.Value, item.CreateDate) == 0)
                        {
                            if (RoomId == item.RoomID)
                            {
                                if (DateTime.Compare(dateStart.Value, item.TimeStart) == 0 && DateTime.Compare(dateStart.Value, item.TimeEnd) < 0)
                                {
                                    return Json(new { status = false, mess = "Phòng đã được đặt" });
                                }
                                if (DateTime.Compare(dateStart.Value, item.TimeStart) > 0 && DateTime.Compare(dateStart.Value, item.TimeEnd) < 0)
                                {
                                    return Json(new { status = false, mess = "Phòng đã được đặt" });
                                }
                                if (DateTime.Compare(dateEnd.Value, item.TimeStart) > 0 && DateTime.Compare(dateEnd.Value, item.TimeEnd) < 0)
                                {
                                    return Json(new { status = false, mess = "Phòng đã được đặt" });
                                }
                                if (DateTime.Compare(dateStart.Value, item.TimeStart) < 0 && DateTime.Compare(dateEnd.Value, item.TimeEnd) > 0)
                                {
                                    return Json(new { status = false, mess = "Phòng đã được đặt" });
                                }
                            }
                        }
                    }
                }
            }
            if (DateTime.Compare(dateEnd.Value, dateStart.Value) < 0)
            {
                return Json(new { status = false, mess = "Giờ kết thúc không nhỏ hơn giờ bắt đầu" });
            }
            return Json(new { status = true });
        }
    }
}