using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using UAL.BLL.Data;
using UAL.BLL.DataModel;
//using UnitedAccessoriesLimited.Models;
using UAL.BLL.Models;

namespace UnitedAccessoriesLimited.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create
        public ActionResult Mailing()
        {
            return View();
        }
        public ActionResult MailOutput()
        {
            GMailer.GmailUsername = "mahbub.moon6@gmail.com";
            GMailer.GmailPassword = "PasswordHere";

            GMailer mailer = new GMailer();

            mailer.ToEmail = Request.Form["UserNameF"];
            string link = mailer.linkGen(mailer.ToEmail);
            mailer.Subject = "Password Reset";
            mailer.Body = "Reset your password by clicking on the link: <br> <a href='192.168.1.3/User/PasswordReset?" + link + "'>Password Reset</a>";
            mailer.IsHtml = true;
            mailer.Send();
            Session["PasswordReset"] = "Yes";
            return View();
        }
        public ActionResult PasswordReset(int role, string user, int id)
        {
            Session["PasswordReset"] = null;
            GMailer m = new GMailer();
            User u = m.GetUser(role, user, id);
            Session["UserID"] = u.UserID;
            Session["UserNameForPR"] = u.UserName;
            if (m.findUser(role, user, id) != 1)
                return RedirectToAction("Login", "Home");
            return View(u);
        }
        [HttpPost]
        public ActionResult PasswordReset()
        {
            GMailer m = new GMailer();
            string password = Request.Form["pw"];
            m.changePassword(Convert.ToInt32(Session["UserID"]), password);
            return RedirectToAction("Login", "Home");
        }
        [HttpPost]
        public ActionResult Create(User u)
        {
            try
            {
                UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
                //User us = new User();
                //us.Password = u.Password;
                //us.UserName = u.UserName;
                u.Role = (from a in ual.Roles
                          where a.RoleName == u.Role.RoleName
                          select a).FirstOrDefault();
                u.RoleID = (from a in ual.Roles
                            where a.RoleName == u.Role.RoleName
                            select a.RoleID).FirstOrDefault();
                ual.Users.Add(u);
                ual.SaveChanges();
                Session["UserCreated"] = "User " + u.UserName + " has been created Successfully.";

                // TODO: Add insert logic here

                return RedirectToAction("Create");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /User/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult SendMail()
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add("mahbub.rahman@gits-bd.com");
                mailMessage.From = new MailAddress("mahbub.moon6@gmail.com");
                mailMessage.Subject = "ASP.NET e-mail test";
                mailMessage.Body = "Hello world,\n\nThis is an ASP.NET test e-mail!";
                SmtpClient smtpClient = new SmtpClient("smtp.your-isp.com", 465);
                smtpClient.Send(mailMessage);
                Response.Write("E-mail sent!");
            }
            catch (Exception ex)
            {
                Response.Write("Could not send the e-mail - error: " + ex.Message);
            }
            return View();
        }
    }
}
