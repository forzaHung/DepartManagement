/*
 * Created: Pham Thanh Hải
 * Date: 12/04/2017
 * Name: CustomerTyped
 */
using Framework.EF;
using MvcPaging;
using Newtonsoft.Json;
using Dispatch;
using ProjectManagement.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DispatchManagement.Controllers
{
    public class CustomerTypeController : BaseController
    {
        private IplCustomer_Type _iplCustomerType;
        /// <summary>
        /// CustomerTypeController
        /// </summary>
        public CustomerTypeController()
        {
            _iplCustomerType = SingletonIpl.GetInstance<IplCustomer_Type>();
        }
        /// <summary>
        /// Index
        /// </summary>
        /// <returns>Toàn bộ danh sách CustomerType</returns>
        // GET: Customer_Type
        public ActionResult Index()
        {
            //var treeCustomer = _iplCustomerType.BindTreeview();
            //return View(treeCustomer);
            int page = 1;
            int totalRow = 0;
            int pageSize = 10;
            var customerType = _iplCustomerType.ListAllPaging(page, pageSize, ref totalRow);
            return View(customerType.ToPagedList(page, pageSize, totalRow));
        }
        /// <summary>
        /// Create or Edit CustomerType Giao diện
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Kiểm tra xem là create or edit để trả về giao diện</returns>
        public ActionResult CreateOrEdit(int? id)
        {
            var treeCustomer = _iplCustomerType.BindTreeview();
            ViewData["AllCustomer"] = treeCustomer;
            Customer_TypeEntity entity = new Customer_TypeEntity();
            if (id.HasValue)
            {
                ViewBag.Title = "Sửa loại khách hàng";
                ViewBag.SubTitle = "Sửa thông tin loại khách hàng";
                entity = _iplCustomerType.ViewDetail(id.Value);
                if (entity == null)
                    return HttpNotFound("Không có thông tin loại khách hàng");
                return View(entity);
            }
            ViewBag.Title = "Thêm loại khách hàng";
            ViewBag.SubTitle = "Thêm thông tin loại khách hàng";
            return View(entity);
        }
        /// <summary>
        /// Phương thức thêm mới hoặc sửa thông tin CustomerType
        /// </summary>
        /// <param name="temp">CustomerTypeEntity</param>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateOrEdit(Customer_TypeEntity temp, int? id)
        {
            var treeCustomer = _iplCustomerType.BindTreeview();
            ViewData["AllCustomer"] = treeCustomer;
            Customer_TypeEntity entityTemp = new Customer_TypeEntity();

            if (!ModelState.IsValid)
                return View(temp);
            int CustomerID = 0;
            if (id.HasValue && id.Value > 0)
            {
                entityTemp = _iplCustomerType.ViewDetail(id.Value);
                if (entityTemp == null)
                {
                    ViewBag.MgsNotFound = "Customer Type does not exist";
                    return View(temp);
                }
                CustomerID = id.Value;
                entityTemp.IsActive = temp.IsActive;
                entityTemp.Id = id.Value;
                entityTemp.IsDel = temp.IsDel;
                entityTemp.Name = temp.Name;
                entityTemp.ParentId = temp.ParentId;
                _iplCustomerType.Update(entityTemp);
            }
            else
            {
                CustomerID = _iplCustomerType.Insert(temp);
                return RedirectToAction("Index");
            }
            if (CustomerID == -1)
            {
                ModelState.AddModelError("Name", "Trùng tên. Có thể danh mục này đã bị xóa. Bạn có thể kiểm tra trong thùng rác");
                return View(temp);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Delete CustomerType
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            var objTemp = _iplCustomerType.ViewDetail(id);
            if (objTemp != null && objTemp.Id > 0)
            {
                var trave = _iplCustomerType.Delete(id);
                //var treeCustomer = _iplCustomerType.BindTreeview();
                if (trave)
                {
                    int totalRow = 0;
                    var obj = _iplCustomerType.ListAllPaging(1, 10, ref totalRow);
                    return View("Index", obj.ToPagedList(1, 10, totalRow));
                }
                else
                {
                    ViewBag.msg = "Occur Error. Contact to Administrator";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("err", "Dupplicate code");
                return View(objTemp);
            }
        }
        /// <summary>
        /// Cập nhật trạng thái kích hoạt
        /// </summary>
        /// <param name="moduleId">ID</param>
        /// <param name="action">Action</param>
        /// <param name="access">Kết quả</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Publish(int moduleId, string action, bool access)
        {
            var detail = _iplCustomerType.ViewDetail(moduleId);
            switch (action)
            {
                case "Active":
                    if (access)
                    {
                        detail.IsActive = true;
                    }
                    else
                    {
                        detail.IsActive = false;
                    }
                    break;
            }
            var trave = _iplCustomerType.Update(detail);
            return Json(new { status = trave });
        }/// <summary>
         /// Hàm tìm kiếm
         /// </summary>
         /// <param name="searchString"></param>
         /// <param name="page"></param>
         /// <param name="pageSize"></param>
         /// <returns></returns>
        public PartialViewResult GetCustomerType(string searchString = "", int page = 1, int pageSize = 10)
        {
            int totalRow = 0;
            IEnumerable<Customer_TypeEntity> lst;
            if (!string.IsNullOrEmpty(searchString))
            {
                dynamic dnamic = JsonConvert.DeserializeObject(searchString);
                string searchName = dnamic.searchName;
                lst = this._iplCustomerType.ListAllPaging(searchName.Trim(), page, pageSize, ref totalRow).ToPagedList(page, pageSize, totalRow);

            }
            else
            {
                lst = _iplCustomerType.ListAllPaging(page, pageSize, ref totalRow).ToPagedList(page, pageSize, totalRow);
            }
            return PartialView("_CustomerTypeList", lst);
        }
    }
}