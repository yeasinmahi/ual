using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace UnitedAccessoriesLimited
{
    /// <summary>
    /// Summary description for hn_SimpleFileUploader
    /// </summary>
    public class hn_SimpleFileUploader : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string dirFullPath = System.Web.HttpContext.Current.Server.MapPath("~/UploadedFiles/");
            string[] files;
            int numFiles;
            files = System.IO.Directory.GetFiles(dirFullPath);
            context.Response.Write(files);
            numFiles = files.Length;
            numFiles = numFiles + 1;

            string str_image = "";

            foreach (string s in context.Request.Files)
            {
                HttpPostedFile file = context.Request.Files[s];
                //  int fileSizeInBytes = file.ContentLength;
                string fileName = file.FileName;
                string fileExtension = file.ContentType;

                if (!string.IsNullOrEmpty(fileName))
                {
                    fileExtension = Path.GetExtension(fileName);
                    str_image = "MyPHOTO_" + numFiles.ToString() + fileExtension;
                    string pathToSave_100 = System.Web.HttpContext.Current.Server.MapPath("~/UploadedFiles/") + str_image;
                    file.SaveAs(pathToSave_100);
                }
            }
            context.Response.Write(str_image);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}