using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using UAL.BLL.Models;

namespace UAL.BLL.Data
{
    public class GMailer
    {
        public static string GmailUsername { get; set; }
        public static string GmailPassword { get; set; }
        public static string GmailHost { get; set; }
        public static int GmailPort { get; set; }
        public static bool GmailSSL { get; set; }

        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }

        static GMailer()
        {
            GmailHost = "smtp.gmail.com";
            GmailPort = 587; // Gmail can use ports 25, 465 & 587; but must be 25 for medium trust environment.
            GmailSSL = true;
        }
        public int findUser(int role, string user, int id)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            var u = ual.Users.Where(m => m.RoleID.Equals(role) && m.UserName.Equals(user) && m.UserID.Equals(id)).ToList();
            if (u.Count() == 1)
                return 1;
            return 0;
        }
        public User GetUser(int role, string user, int id)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            User u = ual.Users.Where(m => m.RoleID.Equals(role) && m.UserName.Equals(user) && m.UserID.Equals(id)).ToList().FirstOrDefault();
            return u;
        }
        public int changePassword(int id,string pw)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            User u = ual.Users.Where(m => m.UserID == id).ToList().FirstOrDefault();
            u.Password = pw;
            ual.SaveChanges();
            return 1;
        }
        public string linkGen(string email)
        {
            UnitedAccessoriesDBEntities ua = new UnitedAccessoriesDBEntities();
            User u = ua.Users.Where(m => m.Email == email).ToList().FirstOrDefault();
            string link = "role=" + u.RoleID + "&&user=" + u.UserName + "&&id=" + u.UserID;
            return link;
        }
        public void Send()
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = GmailHost;
            smtp.Port = GmailPort;
            smtp.EnableSsl = GmailSSL;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(GmailUsername, GmailPassword);

            using (var message = new MailMessage(GmailUsername, ToEmail))
            {
                message.Subject = Subject;
                message.Body = Body;
                message.IsBodyHtml = IsHtml;
                smtp.Send(message);
            }
        }
    }
}
