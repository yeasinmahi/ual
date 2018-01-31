using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UAL.BLL.Data;
using UAL.BLL.Models;

namespace UnitedAccessoriesLimited.Controllers
{
    public class BreakdownController : Controller
    {
        //
        // GET: /Breakdown/

        public ActionResult CreatePO()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatePO(Breakdown bd)
        {
            OrderRepository or = new OrderRepository();
            if (or.createPO(bd) == 1)
            {
                Session["PoCreated"] = bd.PONumber + " is created.";
            }
            return View();
        }
        public ActionResult BreakDownList()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BreakDownList(Breakdown b)
        {
            return RedirectToAction("GenerateList", new { refN = b.ReferenceNo });
        }
        public ActionResult GenerateList(string refN)
        {
            
            OrderRepository or = new OrderRepository();
            List<Breakdown> bd = or.getBreakDown(refN);
            Session["Customer"] = bd[0].Order.CustomerName;
            Session["OrderRef"] = bd[0].Order.OrderRefNo;
            Session["TypeofAccessories"] = bd[0].Order.AccessoryType;
            Session["ReferenceNo"] = bd[0].ArtWorkUpload.refNumber;
            return View(bd);
        }

    }
}
