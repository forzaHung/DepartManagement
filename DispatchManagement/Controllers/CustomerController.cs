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
    public class CustomerController : BaseController
    {
        private  IplCustomer _iplCustomer;
        private IplCustomer_Type _iplCustomerType;
        public CustomerController()
        {
            _iplCustomer = SingletonIpl.GetInstance<IplCustomer>();
            _iplCustomerType = SingletonIpl.GetInstance<IplCustomer_Type>();
        }
        // GET: Customer
        public ActionResult Index()
        {
            int page = 1;
            int pageSize = 10;
            int totalRow = 0;
            var entity = _iplCustomer.ListAllPaging(page, pageSize, ref totalRow);
            return View(entity.ToPagedList(page, pageSize, totalRow));
        }
        /// <summary>
        /// Load giao diện create or edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CreateOrEdit(int? id)
        {
            var treeCustomer = _iplCustomerType.BindTreeview();
            ViewData["AllCustomer"] = treeCustomer;
            CustomerEntity entity = new CustomerEntity();
            if (id.HasValue)
            {
                ViewBag.Title = "Sửa khách hàng";
                ViewBag.SubTitle = "Sửa thông tin khách hàng";
                entity = _iplCustomer.ViewDetail(id.Value);
                if (entity == null)
                    return HttpNotFound("Không có thông tin khách hàng");
                return View(entity);
            }
            ViewBag.Title = "Thêm khách hàng";
            ViewBag.SubTitle = "Thêm thông tin khách hàng";
            return View(entity);
        }
        /// <summary>
        /// Hàm xử lý create or edit customer
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateOrEdit(CustomerEntity entity, int? id)
        {
            string customerTypeId="";
            var treeCustomer = _iplCustomerType.BindTreeview();
            ViewData["AllCustomer"] = treeCustomer;
            if (!ModelState.IsValid)
                return View(entity);
            int customerId = 0;
            if(id.HasValue && id.Value > 0)
            {
                var entityTemp = _iplCustomer.ViewDetail(id.Value);
                if(entityTemp==null)
                {
                    ViewBag.MgsNotFound = "Customer does not exist";
                    return View(entity);
                }
                customerTypeId = loadCustomerType(entity.ParentId);
                customerId = id.Value;
                
                entityTemp.Customer_TypeId = customerTypeId;
                entityTemp.Address = entity.Address;
                entityTemp.Createdate = entity.Createdate;
                entityTemp.Id = entity.Id;
                entityTemp.IsActive = entity.IsActive;
                entityTemp.IsDel = entity.IsDel;
                entityTemp.ModifiedDate = DateTime.Now;
                entityTemp.Name = entity.Name;
                entityTemp.ParentId = entity.ParentId;
                _iplCustomer.Update(entityTemp);
            }
            else
            {
                entity.Createdate = DateTime.Now;
                customerTypeId = loadCustomerType(entity.ParentId);
                entity.Customer_TypeId = customerTypeId;
                customerId = _iplCustomer.Insert(entity);

                return RedirectToAction("Index");
            }
            if (customerId == -1)
            {
                ModelState.AddModelError("Name", "Trùng tên. Có thể danh mục này đã bị xóa. Bạn có thể kiểm tra trong thùng rác");
                return View(entity);
            }
            return RedirectToAction("Index");
           
        }
        /// <summary>
        /// Hàm xử lý thay đổi trạng thái kích hoạt
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="action"></param>
        /// <param name="access"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Publish(int moduleId, string action, bool access)
        {
            var detail = _iplCustomer.ViewDetail(moduleId);
            switch (action)
            {
                case "Active":
                    if (access)
                    {
                        detail.IsActive = true;
                        detail.ModifiedDate = DateTime.Now;
                    }
                    else
                    {
                        detail.IsActive = false;
                        detail.ModifiedDate = DateTime.Now;
                    }
                    break;
            }
            var trave = _iplCustomer.Update(detail);
            return Json(new
            {
                status = trave,
                data =detail
            });
        }
        /// <summary>
        /// Hàm tìm kiếm customer
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PartialViewResult GetCustomer(string searchString="", int page = 1, int pageSize = 10)
        {

            int totalRow = 0;
            IEnumerable<CustomerEntity> lst;
            if (!string.IsNullOrEmpty(searchString))
            {
                dynamic dnamic = JsonConvert.DeserializeObject(searchString);
                string searchName = dnamic.searchName;
                lst = this._iplCustomer.ListAllPaging(searchName.Trim(), page, pageSize, ref totalRow).ToPagedList(page, pageSize, totalRow);
            }
            else
            {
                lst = _iplCustomer.ListAllPaging(page, pageSize, ref totalRow).ToPagedList(page, pageSize, totalRow);
            }
            return PartialView("_CustomerList", lst);
        }
        /// <summary>
        /// Lấy customer type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string loadCustomerType(int id)
        {
            //List<Customer_TypeEntity> lst = new List<Customer_TypeEntity>();
            Customer_TypeEntity obj = new Customer_TypeEntity();
            obj = _iplCustomerType.ViewDetail(id);
            //if (obj.ParentId == null)
            //{
            //    lst.Add(obj);
            //}
            //else
            //{

            //}
            string customerType = obj.ParentId + ";" + obj.Id+";";
            return customerType;
        }
        /// <summary>
        /// Hàm xóa customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            var objTemp = _iplCustomer.ViewDetail(id);
            if (objTemp != null && objTemp.Id > 0)
            {
                var trave = _iplCustomer.Delete(id);
                if (trave)
                {
                    int totalRow = 0;
                    var obj = _iplCustomer.ListAllPaging("", 1, 10, ref totalRow);
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
    }
}