using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UAL.BLL.DataModel;
using UAL.BLL.Data;
using UAL.BLL.Models;
namespace UnitedAccessoriesLimited.Controllers
{
    public class ArtworkController : BaseController
    {
        //
        // GET: /Artwork/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit(string refN)
        {
            DataLayer dl = new DataLayer();
            ArtWorkUpload au = dl.findArtwork(refN);
            return View(au);
        }
        [HttpPost]
        public ActionResult Edit(ArtWorkUpload au){
            DataLayer dl = new DataLayer();
            if(dl.EditArtwork(au)==1){

            }
            return View();
        }
        //
        // GET: /Artwork/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Artwork/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Artwork/Create
        
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Artwork/Edit/5

        public ActionResult DropFiles()
        {
            if (goToLogin() == false)
                return RedirectToAction("Login", "Home");
            return View();

        }
        public ActionResult UploadImageForSample(string label, string refN, string combo, string buyer, DateTime doS)
        {
            string newFileName = label + "_" + refN + "_" + combo + "_" + buyer + "_" + doS.ToString("dd-MM-yyyy");
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var subPath = Server.MapPath("~/NewFolder2/");
                    string ext = Path.GetExtension(fileName);
                    var path = Path.Combine(subPath, newFileName + ext);
                    if (!Directory.Exists(subPath))
                    {
                        Directory.CreateDirectory(subPath);
                    }
                    if (Directory.Exists(path))
                        Directory.Delete(path);

                    file.SaveAs(path);
                    @TempData["ImageSubmitted"] = "Art Work with reference - " + refN + " was Submitted Successfully";
                }
                else
                {
                    @TempData["ImageSubmitted"] = "Failed to upload Art Work for reference - " + refN;
                }
            }
            return RedirectToAction("SampleDetails", new { refN = refN });
        }
        [HttpPost]
        public ActionResult Upload(Sample s)
        {
            DataLayer dl = new DataLayer();

            string referenceGen = "";
            if (s.Reference == null)
            {
                s.Reference = dl.ReferenceGenerator(s);
            }
            referenceGen = s.Reference;
            DateTime x = DateTime.ParseExact(Request.Form["date"], "dd-MMMM-yyyy", CultureInfo.InvariantCulture);
            string doSS = Convert.ToDateTime(x, CultureInfo.GetCultureInfo("en-US")).ToString("dd-MM-yyyy");
            string labelforFile = shortLabel(s.Label);
            string newFileName = labelforFile + "_" + referenceGen + "_" + s.Combo + "_" + s.Buyer + "_" + doSS;
            s.comments = Request.Form["comments"];

            if (dl.submitSample(s, x, Session["UserName"].ToString()) == 1)
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var subPath = Server.MapPath("~/NewFolder2/");
                        string ext = Path.GetExtension(fileName);
                        var path = Path.Combine(subPath, newFileName + ext);
                        if (!Directory.Exists(subPath))
                        {
                            Directory.CreateDirectory(subPath);
                        }
                        file.SaveAs(path);
                        @TempData["Submitted"] = "Art Work with reference - " + s.Reference + " was Submitted Successfully";
                    }
                    else
                    {
                        @TempData["Submitted"] = "Failed to upload Art Work for reference - " + s.Reference;
                    }
                }
            }
            else { @TempData["Submitted"] = "Art Work with reference - " + s.Reference + " exists."; }
            return RedirectToAction("DropFiles");
        }
        public ActionResult PendingSamples()
        {
            if (goToLogin() == false)
                return RedirectToAction("Login", "Home");

            DataLayer dl = new DataLayer();
            List<Sample> ls = new List<Sample>();
            ls = dl.getSamples();
            return View(ls);
        }
        public ActionResult Profile()
        {
            if (goToLogin() == false)
                return RedirectToAction("Login", "Home");
            DataLayer dl = new DataLayer();
            List<ArtWorkUpload> ls = new List<ArtWorkUpload>();
            ls = dl.getSamples(Session["UserName"].ToString());
            return View(ls);
        }
        public ActionResult sendToFactory(string refN)
        {
            DataLayer dl = new DataLayer();
            if (dl.updateSample(refN, "Pending In Factory") == 1)
            {
                @TempData["sendToFactory"] = "Label " + refN + " is sent to UAL factory..";
            }
            return RedirectToAction("PendingSamples");
        }
        public FileStreamResult viewSample(string label, string refN, string combo, string Buyer, DateTime doS)
        {
            if (goToLogin() == false)
                RedirectToAction("Login", "Home");
            label = shortLabel(label);
            string doSS = Convert.ToDateTime(doS, CultureInfo.GetCultureInfo("en-US")).ToString("dd-MM-yyyy");
            string fileName = label + "_" + refN + "_" + combo + "_" + Buyer + "_" + doSS;
            if (Session["UserName"] != null)
            {
                try
                {
                    @TempData["fileError"] = "";
                    FileStream fs = new FileStream(Server.MapPath("~/NewFolder2/" + fileName + ".pdf"), FileMode.Open, FileAccess.Read);
                    return File(fs, "application/pdf");

                }
                catch (Exception ex)
                {
                    @TempData["fileError"] = ex.ToString();
                }
            }
            return null;

        }
        public string shortLabel(string label)
        {
            string labelforFile = "";
            switch (label)
            {
                case "Woven Department": { labelforFile = "WL"; break; }
                case "Printed Department": { labelforFile = "PL"; break; }
                case "Offset Department": { labelforFile = "OP"; break; }

            }
            return labelforFile;
        }
        public bool goToLogin()
        {
            if (Session["UserName"] == null)
                return false;
            else return true;
        }
        public ActionResult PendingInFactory()
        {
            if (goToLogin() == false)
                return RedirectToAction("Login", "Home");

            DataLayer dl = new DataLayer();
            List<ArtWorkUpload> ls = new List<ArtWorkUpload>();
            ls = dl.getSamplesInFactory("Pending In Factory");
            return View(ls);
        }
        public ActionResult deliveredToUAL(string refN)
        {
            DataLayer dl = new DataLayer();
            if (dl.updateSample(refN, "DeliveredToUAL") == 1)
            {
                @TempData["deliveredToUAL"] = "Sample is ready and sent for Reference:" + refN + "..";
            }
            return RedirectToAction("PendingInFactory");
        }
        public ActionResult ReadyForBuyer(string refN)
        {
            DataLayer dl = new DataLayer();
            List<ArtWorkUpload> ls = new List<ArtWorkUpload>();
            if (refN == null) ls = dl.getSamplesInFactory("DeliveredToUAL");
            else
            {

                List<ArtWorkUpload> tsu = dl.searchArtWorks(refN, "DeliveredToUAL");

                return View(tsu);
            }
            return View(ls);
        }
        public ActionResult submitToBuyer(string refN)
        {
            string price = Request.Form["price"];
            string finishing = Request.Form["finishing"];
            string note = Request.Form["note"];
            DateTime SentForApprovalDate = DateTime.ParseExact(Request.Form["sentForApprovalDate"], "dd-MMM-yy", CultureInfo.InvariantCulture); ;
            DataLayer dl = new DataLayer();
            if (dl.SubmitToBuyer(refN, price, finishing, note, SentForApprovalDate) == 1)
            {
                TempData["Status"] = "Submitted to Buyer...";
            }



            return RedirectToAction("ReadyForBuyer");
        }
        public ActionResult Search()
        {
            string search = Request.Form["search"];
            return RedirectToAction("ReadyForBuyer", new { refN = search });
        }
        public ActionResult Approval()
        {
            DataLayer dl = new DataLayer();
            List<ArtWorkUpload> su = dl.getSamplesInFactory("SendForApproval");
            return View(su);
        }
        public ActionResult PriceList()
        {
            DataLayer dl = new DataLayer();
            List<Pricelist> lpl = dl.generatePriceList();
            dl.getBrands();
            return View(lpl);
        }

        //public ActionResult ExportPriceList()
        //{
        //    UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
        //    GridView gv = new GridView();
        //    var x = from a in ual.ArtWorkUploads
        //            join b in ual.ArtWorkPricings on a.refNumber equals b.refNumber
        //            where a.status == "Approved"
        //            select new { a.itemDescription, a.refNumber, a.combo, b.Finishing, a.brand, b.Price };
        //    gv.DataSource = x.ToList();
        //    gv.DataBind();
        //    Response.ClearContent();
        //    Response.Buffer = true;
        //    string fn = "_Brand" + "_" + DateTime.Now.ToString("dd-MM-yyyy");
        //    Response.AddHeader("content-disposition", "attachment; filename=PriceList" + fn + ".xls");
        //    Response.ContentType = "application/ms-excel";
        //    Response.Charset = "";
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter htw = new HtmlTextWriter(sw);
        //    gv.RenderControl(htw);
        //    Response.Output.Write(sw.ToString());
        //    Response.Flush();
        //    Response.End();

        //    return RedirectToAction("PriceList");
        //}
        public ActionResult ApprovedByBuyer(string refN)
        {
            string Buyer = Request.Form["buyer"];
            DateTime ApprovalDate = DateTime.ParseExact(Request.Form["approvaldate"], "dd-MMM-yy", CultureInfo.InvariantCulture); ;

            DataLayer dl = new DataLayer();
            dl.updateSample(refN, "Approved");
            dl.UpdateApproval(Buyer, ApprovalDate, refN);
            Session["ApprovedByBuyer"] = refN + " Approved by Buyer: " + Buyer;
            return RedirectToAction("Approval");
        }
        public ActionResult RejectedByBuyer(string refN){
            string comments = Request.Form["comments"];
            DataLayer dl = new DataLayer();
            dl.updateSample(refN, "Uploaded");
            dl.updateComments(comments, refN);
            return RedirectToAction("Approval");
        }

        public ActionResult SampleDetails(string refN)
        {
            DataLayer dl = new DataLayer();
            detailedSample ds = dl.getDetails(refN);

            return View(ds);
        }
        public ActionResult ApprovedSamples()
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            List<ArtWorkUpload> lsu = ual.ArtWorkUploads.Where(x => x.status.Equals("Approved")).ToList();
            return View(lsu);
        }
        public ActionResult SampleSheet(string refN)
        {
            DataLayer dl = new DataLayer();
            ArtWorkUpload aw = dl.getSample(refN);
            return View(aw);
        }
    }
}
