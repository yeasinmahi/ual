using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UAL.BLL.Data;
using UAL.BLL.Models;

namespace UnitedAccessoriesLimited.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /Order/

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Order o, FormCollection frm)
        {
            o.Date = DateTime.ParseExact(Request.Form["date"], "dd-MMMM-yyyy", CultureInfo.InvariantCulture);
            o.DeliveryDate = DateTime.ParseExact(Request.Form["deliveryDate"], "dd-MMMM-yyyy", CultureInfo.InvariantCulture);
            o.CreationDate = DateTime.Now;
            o.CreatedBy = Session["UserName"].ToString();
            OrderRepository or = new OrderRepository();
            int x = or.CreateOrder(o);
            if (Request.Form["sizewise"]!= null)
            if (Request.Form["sizewise"].Equals("on"))
            {
                Size s = new Size();
                s.OrderRef = o.OrderRef;
                s.S = Convert.ToInt64(Request.Form["s"]);
                s.M = Convert.ToInt64(Request.Form["m"]);
                s.L = Convert.ToInt64(Request.Form["l"]);
                s.XL = Convert.ToInt64(Request.Form["xl"]);
                s.XXL = Convert.ToInt64(Request.Form["xxl"]);
                or.createSizewiseOrder(s);
            }
            return RedirectToAction("WorkOrder", "Order", new { order = o.OrderRef });
        }
        public ActionResult WorkOrder(string order)
        {
            OrderRepository or = new OrderRepository();
            Order o = or.getOrder(order);
            Session["hasSize"] = "no";
            if (or.hasSize(order) == 1)
            {
                Session["hasSize"] = "yes";
            }
            return View(o);
        }
        public ActionResult Edit(string order)
        {
            Session["WorkOrderUpdated"] = "";
            OrderRepository or = new OrderRepository();
            Order o = or.getOrder(order);
            return View(o);
        }
        [HttpPost]
        public ActionResult Edit(Order o)
        {
            OrderRepository or = new OrderRepository();
            if (or.updateOrder(o)==1)
            {
                Session["WorkOrderUpdated"] = "Work order: " + o.OrderRef + " has been updated.";
            }
            return View(o);
        }
        public ActionResult WorkOrderList()
        {
            OrderRepository or = new OrderRepository();
            List<Order> lor = or.getOrderList();
            return View(lor);
        }
        public ActionResult WorkOrders()
        {
            OrderRepository or = new OrderRepository();
            List<Sale> lor = or.getWorkOrderList();
            return View(lor);

        }
        public ActionResult Completed(string woId)
        {
            OrderRepository or = new OrderRepository();
            or.updateStatus(woId);
            return RedirectToAction("WorkOrders");
        }
        public ActionResult Stock(string woId)
        {
            OrderRepository or = new OrderRepository();
            Sale s = or.getSalesItem(woId);
            return View(s);
        }
        public ActionResult CreatePurchaseOrder()
        {
            Session["POCreated"] = "";
            return View();
        }
        [HttpPost]
        public ActionResult CreatePurchaseOrder(PurchaseOrder pos)
        {
            try{
            pos.IssueDate = DateTime.ParseExact(Request.Form["issuedate"], "dd-MMMM-yyyy", CultureInfo.InvariantCulture);
            
            }
            catch (Exception)
            {
                Session["POCreated"] = "Please fill ISSUE DATE Field with proper value";
                return View();
            }
            OrderRepository or = new OrderRepository();
            if (or.CreatePurchaseOrder(pos) == 1)
            {
                Session["POCreated"] = "Purchase Order has been Created Successfully.";
            }
            else
            {
                Session["POCreated"] = "Failed to create Purchase Order.";
            }
            return View();
        }
        public ActionResult PurchaseOrderList()
        {
            OrderRepository or = new OrderRepository();
            List<PurchaseOrder> lpo = or.listOfPO();
            return View(lpo);
        }
        public ActionResult POComplete(string OrdRef)
        {
            OrderRepository or = new OrderRepository();
            if (or.updatePOStatus(OrdRef) == 1)
            {
                Session["POCompleted"] = "Purchase Order Completed.";
            }
            else Session["POCompleted"] = "Failed to Update Purchase Order.";
            return RedirectToAction("PurchaseOrderList");
        }
        public ActionResult PurchaseOrder(string OrdRef)
        {
            OrderRepository or = new OrderRepository();
            List<PurchaseOrder> poItem = or.GetPurchaseOrder(OrdRef);
            PurchaseOrder poxx = poItem.FirstOrDefault();
            Session["FromCompany"] = poxx.FromCompany;
            Session["ToCompany"] = poxx.ToCompany;
            Session["OrderRef"] = poxx.OrderRef;
            Session["IssueDate"] = poxx.IssueDate;
            Session["Attn"] = poxx.Attn;
            return View(poItem);
        }
        public ActionResult BreakDown(long id)
        {
            OrderRepository or = new OrderRepository();
            PurchaseOrder pos = or.GetPurchaseOrder(id);
            return View(pos);
        }
    }
}
