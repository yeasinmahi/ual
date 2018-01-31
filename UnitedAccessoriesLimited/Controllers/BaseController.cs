using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UAL.BLL.Models;

namespace UnitedAccessoriesLimited.Controllers
{
    public class BaseController : Controller
    {

        protected override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }
        protected override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            // access 
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();

            Session["TotalPending"] = ual.ArtWorkUploads.Where(m => m.status == "Uploaded").ToList().Count();
            Session["TotalPendingInFactory"] = ual.ArtWorkUploads.Where(m => m.status == "Pending In Factory").ToList().Count();
            Session["TotalDeliveredToUAL"] = ual.ArtWorkUploads.Where(m => m.status == "DeliveredToUAL").ToList().Count();
            Session["TotalSendForApproval"] = ual.ArtWorkUploads.Where(m => m.status == "SendForApproval").ToList().Count();
            Session["TotalApproved"] = ual.ArtWorkUploads.Where(m => m.status == "Approved").ToList().Count();
            Session["TotalUsers"] = ual.Users.Count();
            Session["TotalOrders"] = ual.Sales.Count();
            Session["TotalArtworks"] = ual.ArtWorkUploads.Count();
            Session["TotalBuyers"] = ual.Users.Where(m => m.Role.RoleName == "Customer").ToList().Count();
            AccessToUser atu = new AccessToUser();
            string currContext = Request.Url.AbsolutePath;
            if (Session["UserName"] != null && !currContext.Equals("/Home/NoAccess") && !currContext.Contains("Login"))
            {
                string user = Session["UserName"].ToString();
                var access = (from a in ual.AccessToUsers
                             join b in ual.Urls on a.urlID equals b.urlID
                             join c in ual.Users on a.UserID equals c.UserID
                             where currContext.Contains(b.URL) && c.UserName == user && a.hasAccess == true
                             select a);
                int ca = access.Count();
                if (ca == 1)
                {

                }
                else
                {
                    if (!currContext.Contains("Upload")
                        && !currContext.Contains("UploadImageForSample")
                        && !currContext.Contains("Profile")
                        && !currContext.Contains("sendToFactory")
                        && !currContext.Contains("deliveredToUAL")
                        && !currContext.Contains("submitToBuyer")
                        && !currContext.Contains("ApprovedByBuyer")
                        && !currContext.Contains(""))
                    {
                        context.Result = new RedirectResult("/Home/NoAccess");
                        return;
                    }
                    
//                    RedirectToAction("NoAccess", "Home");
                }
            }


        }
    }
}
