using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UnitedAccessoriesLimited.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ViewResult Index()
        {
            return View("Error");
        }
        public ViewResult NotFound()
        {
            //Session.Abandon();
            //Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            //string sessionId = System.Web.HttpContext.Current.Session.SessionID;
            //@Session["dynamic"] = Convert.ToString(sessionId);
            Response.StatusCode = 404;  //you may want to set this to 200
            return View("NotFound");
        }

    }
}
