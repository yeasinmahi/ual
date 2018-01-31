using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using UAL.BLL.DataModel;
using UAL.BLL.Models;
using UAL.BLL.Data;
//using UnitedAccessoriesLimited.Models;
//using UAL.BLL.



namespace UnitedAccessoriesLimited.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            if (goToLogin() == true)
                return RedirectToAction("Home");
            return RedirectToAction("Login", "Home");
        }
        public ActionResult Home()
        {
            if(Session["UserType"] != null)
            switch (Session["UserType"].ToString())
            {
                case "Admin": { return RedirectToAction("Dashboard", "Home"); }
                case "General": { return RedirectToAction("DropFiles", "Artwork"); }
                case "Customer": { return RedirectToAction("PendingInFactory", "Artwork"); }

            }

            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult NoAccess()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }
        [HttpPost]
        public ActionResult Login(UserInfo u)
        {
            DataLayer dl = new DataLayer();
            string type = dl.findUser(u);
            if (type != "")
            {
                Session["UserName"] = u.userName;
                Session["UserType"] = type;
                if (type == "Customer")
                {
                    return RedirectToAction("PendingInFactory","Artwork");
                }
                return RedirectToAction("Dashboard", "Home");
            }
            @TempData["ErrorMessage"] = "Invalid Password or User Name: <b>" + u.userName + "</b>";
            return View();
        }
        public bool goToLogin()
        {
            if (Session["UserName"] == null)
                return false;
            else return true;
        }
        
    }
}
