using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UAL.BLL.Data;
using UAL.BLL.Models;

namespace UnitedAccessoriesLimited.Controllers
{
    public class SalesController : Controller
    {
        //
        // GET: /Sales/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Create(Sale s)
        {
            string[] size = Request.Form.GetValues("size[]");
            string[] qty = Request.Form.GetValues("qty[]");
            s.Status = "Pending";
            s.ProductionHours = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(s.OrderQty) * Convert.ToDouble(s.Pick) / Convert.ToDouble(s.Cutter) / 600 / 60));
            s.WOIssueDate = DateTime.ParseExact(Request.Form["issuedate"], "dd-MMMM-yyyy", CultureInfo.InvariantCulture);
            OrderRepository or = new OrderRepository();
            s.WO = or.generateWONumber(s.Label);
            if (or.createSales(s, size, qty) == 1)
            {
                Session["SalesCreated"] = "Sales Item Created Successfully";
            }
            return View();
        }
        public ActionResult SalesList()
        {
            OrderRepository o = new OrderRepository();
            string s = Request.Form["issuedate"];
            string month = "";
            string[] date = s.Split('-');
            switch (date[0])
            {
                case "January": { month = "01"; break; }
                case "February": { month = "02"; break; }
                case "March": { month = "03"; break; }
                case "April": { month = "04"; break; }
                case "May": { month = "05"; break; }
                case "June": { month = "06"; break; }
                case "July": { month = "07"; break; }
                case "August": { month = "08"; break; }
                case "September": { month = "09"; break; }
                case "October": { month = "10"; break; }
                case "November": { month = "11"; break; }
                case "December": { month = "12"; break; }
            }

            string year = date[1];
            Session["Month"] = date[0];
            Session["Year"] = year;
            Session["Label"] = Request.Form["label"];
            List<Sale> los = o.getSalesList(Request.Form["label"], month, year);
            return View(los);
        }

    }
}
