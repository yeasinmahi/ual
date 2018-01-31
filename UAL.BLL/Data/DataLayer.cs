using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UAL.BLL.DataModel;
using UAL.BLL.Models;

namespace UAL.BLL.Data
{
    public class DataLayer
    {
        public string findUser(UserInfo u)
        {
            UnitedAccessoriesDBEntities ua = new UnitedAccessoriesDBEntities();
            //UnitedAccessoriesDBEntities3 ua = new UnitedAccessoriesDBEntities3();

            User user = ua.Users
                .Where(x => x.UserName.Equals(u.userName))
                .Where(x => x.Password.Equals(u.Password)).ToList().FirstOrDefault(); //
            try
            {
                string res = (from a in ua.Users
                              join b in ua.Roles on a.RoleID equals b.RoleID
                              where a.UserName == user.UserName
                              select b.RoleName).FirstOrDefault();
                if (!res.Equals(null))
                {
                    return res;
                }
            }
            catch (Exception ex)
            {
                return "";
            }

            return "";

        }
        public ArtWorkUpload findArtwork(string refN)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            ArtWorkUpload au = ual.ArtWorkUploads.Where(m => m.refNumber == refN).ToList().FirstOrDefault();
            return au;
        }
        public int EditArtwork(ArtWorkUpload au)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            ArtWorkUpload awu = ual.ArtWorkUploads.Where(m => m.refNumber == au.refNumber).ToList().FirstOrDefault();
            if (awu != null)
            {
                
               
                awu.itemDescription = au.itemDescription;
                awu.Price = au.Price;
                ual.SaveChanges();
                return 1;
            }
            return 0;
        }
        public int submitSample(Sample s, DateTime x, string createdBy)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            //UnitedAccessoriesDBEntities3 ual = new UnitedAccessoriesDBEntities3();
            ArtWorkUpload tsu = new ArtWorkUpload();
            tsu.version = s.version;
            tsu.refNumber = s.Reference;
            tsu.label = s.Label;
            tsu.itemDescription = s.Description;
            tsu.dateOfSubmission = x;
            tsu.createdBy = createdBy;
            tsu.comments = s.comments;
            tsu.combo = s.Combo;
            tsu.buyer = s.Buyer;
            tsu.status = "Uploaded";
            tsu.brand = s.Brand;
            tsu.ReceivedFrom = s.ReceivedFrom;
            ual.ArtWorkUploads.Add(tsu);
            int ret = 0;

            try
            {
                ual.SaveChanges();
                ret = 1;
            }
            catch (Exception ex)
            {
                ret = 0;
            }
            return ret;

        }
        public int getMax(string refn)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            int ret = ual.ArtWorkUploads.Where(x => x.refNumber.StartsWith(refn)).Count();
            return ret;
        }
        public List<Sample> getSamples()
        {
            List<Sample> ls = new List<Sample>();

            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            List<ArtWorkUpload> tsu = ual.ArtWorkUploads.Where(x => x.status.Equals("Uploaded")).ToList();
            foreach (ArtWorkUpload x in tsu)
            {
                Sample s = new Sample();
                s.Brand = x.brand.ToString();
                s.Buyer = x.buyer;
                s.Combo = x.combo;
                s.comments = x.comments;
                s.dateOfSubmission = x.dateOfSubmission;
                s.Description = x.itemDescription;
                s.Label = x.label;
                s.Reference = x.refNumber;
                s.version = x.version;
                ls.Add(s);
            }

            return ls;
        }
        public List<ArtWorkUpload> getSamples(string userName)
        {
            List<Sample> ls = new List<Sample>();
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            List<ArtWorkUpload> tsu = ual.ArtWorkUploads.Where(x => x.createdBy.Equals(userName)).ToList();

            return tsu;
        }
        public int updateSample(string refN, string stat)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            var updateS = ual.ArtWorkUploads.SingleOrDefault(b => b.refNumber == refN);
            if (updateS != null)
            {
                updateS.status = stat;
                ual.SaveChanges();
                return 1;
            }
            return 0;

        }
        /// <summary>
        /// This sample shows how to specify the <see cref="TestClass(int)"/> constructor as a cref attribute.�
        /// </summary>
        public List<ArtWorkUpload> getSamplesInFactory(string status)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            List<ArtWorkUpload> tsu = ual.ArtWorkUploads.Where(x => x.status.Equals(status)).ToList();

            return tsu;
        }
        public List<Pricelist> generatePriceList()
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            List<Pricelist> lpl = new List<Pricelist>();
            Pricelist pl = new Pricelist();
            var x = from a in ual.ArtWorkUploads
                    where a.status == "Approved"
                    select new { a.itemDescription, a.refNumber, a.combo, a.Finishing, a.brand, a.Price, a.dateOfSubmission, a.DateOfApproval, a.buyer, a.label, a.ApprovedBy, a.ReceivedFrom };
            foreach (var y in x)
            {
                pl = new Pricelist();
                pl.Brand = y.brand;
                pl.Combo = y.combo;
                pl.Description = y.itemDescription;
                pl.Division = "";
                pl.Finishing = y.Finishing;
                pl.Price = y.Price;
                pl.Reference = y.refNumber;
                pl.dateOfSubmission = y.dateOfSubmission;
                pl.label = y.label;
                pl.ApprovalDate = y.DateOfApproval;
                pl.buyer = y.buyer;
                pl.ApprovedBy = y.ApprovedBy;
                pl.ReceivedFrom = y.ReceivedFrom;
                lpl.Add(pl);
            }
            return lpl;
        }
        public List<ArtWorkUpload> searchArtWorks(string refN, string status)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            //List<string> tsp = ual.ArtWorkPricings.Select(x => x.refNumber).ToList();
            List<ArtWorkUpload> tsu = ual.ArtWorkUploads
                    .Where(x => x.status.Equals(status))
                    .Where(x => x.refNumber.Contains(refN))
                    .ToList();
            return tsu;
        }
        public int UpdateApproval(string Buyer, DateTime ad, string refN)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            ArtWorkUpload awu = ual.ArtWorkUploads.SingleOrDefault(m => m.refNumber == refN);
            if (awu != null)
            {
                awu.DateOfApproval = ad;
                awu.isApproved = "Yes";
                awu.ApprovedBy = Buyer;
                ual.SaveChanges();
            }
            return 1;
        }
        public int updateComments(string comments, string refN)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            ArtWorkUpload awu = ual.ArtWorkUploads.SingleOrDefault(m => m.refNumber == refN);
            if (awu != null)
            {
                awu.comments = comments;
                awu.isApproved = "No";
                ual.SaveChanges();
            }
            return 1;
        }
        public List<string> getBrands()
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            List<string> su = ual.ArtWorkUploads.Select(m => m.brand).Distinct().ToList();
            return su;
        }
        public detailedSample getDetails(string refN)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            detailedSample ds = new detailedSample();
            ArtWorkUpload su = ual.ArtWorkUploads.Where(x => x.refNumber.Equals(refN)).ToList().FirstOrDefault();
            ds.Brand = (su.brand == null) ? "" : su.brand;
            ds.Buyer = (su.buyer == null) ? "" : su.buyer;
            ds.Combo = (su.combo == null) ? "" : su.combo;
            ds.comments = (su.comments == null) ? "" : su.comments;
            ds.dateOfSubmission = (su.dateOfSubmission == null) ? default(DateTime) : su.dateOfSubmission;
            ds.dateOfApproval = (su.DateOfApproval == null) ? default(DateTime) : su.DateOfApproval;
            ds.Description = (su.itemDescription == null) ? "" : su.itemDescription;

            ds.Label = (su.label == null) ? "" : su.label;

            ds.Reference = (su.refNumber == null) ? "" : su.refNumber;
            ds.version = (su.version == null) ? "" : su.version;
            ds.Finishing = (su.Finishing == null) ? "" : su.Finishing;
            ds.NoteAtFooter = (su.NoteAtFooter == null) ? "" : su.NoteAtFooter;
            ds.Price = (su.Price == null) ? "" : su.Price;
            ds.ReceivedFrom = (su.ReceivedFrom == null) ? "" : su.ReceivedFrom;
            ds.sentForApprovalDate = (su.SentForApprovalDate == null) ? default(DateTime) : su.SentForApprovalDate;
            return ds;
        }
        public string ReferenceGenerator(Sample s)
        {

            string referenceGen = "";
            switch (s.version)
            {
                case "US Version": { referenceGen += "U"; break; }
                case "Canada Version": { referenceGen += "CAN"; break; }
            }
            switch (s.Label)
            {
                case "Woven Department": { referenceGen += "WL-"; break; }
                case "Printed Department": { referenceGen += "PL-"; break; }
                case "Offset Department": { referenceGen += "OP-"; break; }

            }
            string max = Convert.ToString(getMax(referenceGen) + 1);
            if (max.Length == 1)
            {
                max = "00" + max;
            }
            else if (max.Length == 2)
            {
                max = "0" + max;
            }
            else
            {
                //do nothing
            }
            referenceGen = referenceGen + max;

            return referenceGen;
        }
        public int SubmitToBuyer(string refN, string price, string note, string finishing, DateTime sentForApprovalDate)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            ArtWorkUpload tsu = ual.ArtWorkUploads.SingleOrDefault(m => m.refNumber == refN);
            //var result = db.Books.SingleOrDefault(b => b.BookNumber == bookNumber);
            if (tsu != null)
            {
                tsu.Price = price;
                tsu.NoteAtFooter = note;
                tsu.Finishing = finishing;
                tsu.SentForApprovalDate = sentForApprovalDate;
            }
            try
            {
                // HttpContext.Current.Session["PriceUpdate"] = "";
                //ual.ArtWorkPricings.Add(tsu);
                ual.SaveChanges();
                try
                {
                    updateSample(refN, "SendForApproval");
                    //    HttpContext.Current.Session["PriceUpdate"] = "Reference " + refN + " has been updated.";
                    return 1;
                }
                catch (Exception ex)
                {
                    throw ex;
                    //return 2;
                }
            }
            catch (Exception ex)
            {
                //  HttpContext.Current.Session["PriceUpdate"] = "Update Failed" + ex.ToString();
                return 0;
            }
        }
        public ArtWorkUpload getSample(string refN)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            ArtWorkUpload awu = ual.ArtWorkUploads.Where(m => m.refNumber == refN).ToList().FirstOrDefault();
            return awu;
        }


    }
}