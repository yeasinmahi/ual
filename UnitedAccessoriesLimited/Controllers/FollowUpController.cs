using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UAL.BLL.Data;
using UAL.BLL.Models;

namespace UnitedAccessoriesLimited.Controllers
{
    public class FollowUpController : Controller
    {
        //
        // GET: /FollowUp/

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Indent I)
        {
            string[] lists = Request.Form.GetValues("follow[]");
            Session["list"] = lists;
            if (Request.Form["submitbutton1"] != null)
            {
                return RedirectToAction("List");
            }
            else if (Request.Form["submitButton2"] != null)
            {
                return RedirectToAction("Payment");
            }
            return View();
            
        }
        public ActionResult Payment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Payment(Payment p)
        {
            if (Session["PaymentSub"]!=null && Session["PaymentSub"].ToString() == "Yes")
            {
                return RedirectToAction("ProformaInvoice");
            }
            Session["BankName"] = p.PaymentMethod;
            Session["BankAddress"] = p.BankAddress;
            Session["BankSwift"] = p.Swift;
            Session["BankAccountNo"] = p.AccountNo;

            Session["PaymentSub"] = "Yes";
            PaymentRepository pr = new PaymentRepository();
            pr.SubmitPayment(p);
            return View();
        }
        public ActionResult ProformaInvoice()
        {
            long[] myInts = Array.ConvertAll((String[])Session["list"], s => long.Parse(s));
            PaymentRepository pr = new PaymentRepository();
            int invoiceNumber = pr.getInvoiceNumber();
            List<Indent> li= pr.getProformaInvoice(myInts);
            Session["invoiceNumber"] = invoiceNumber;
            
            return View(li);
        }
        public ActionResult List(String[] list)
        {
            long[] myInts = Array.ConvertAll((String[])Session["list"], s => long.Parse(s));
            OrderRepository or = new OrderRepository();
            List<Indent> li = or.getFollowUpList(myInts);
            return View(li);
        }
        public ActionResult CreateIndent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateIndent(Indent I)
        {
            OrderRepository or = new OrderRepository();
            string[] wo = Request.Form.GetValues("workOrder[]");
            string[] refN = Request.Form.GetValues("refN[]");
            var woAndRefN = wo.Zip(refN, (w,r) => new { WorkOrder = w, Reference = r });
            long sil = or.getSIL();
            foreach (var x in woAndRefN)
            {
                
                or.createIndent(x.WorkOrder,x.Reference,sil);
            }
            return View();
        }
    }
}
