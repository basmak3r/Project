using EngineersMatrimony.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using EngineersMatrimony.Filters;

namespace EngineersMatrimony.Controllers
{
    [UserAuth]
    public class UploadController : Controller
    {

        // GET: Upload
        private EngineersMatrimonyEntities db = new EngineersMatrimonyEntities();
        
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet] public ActionResult UploadFile() { return View(); }
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            int id = Convert.ToInt32(Session["MID"].ToString());
            Account acc = db.Accounts.SingleOrDefault(s => s.MID == id);

            try
            {
                if (file == null)
                {
                    ViewBag.Message = "Photo Required!";

                }
                else if (file.ContentLength > 0)
                {



                    string _FileName = Path.GetFileName(file.FileName);

                    System.Diagnostics.Debug.WriteLine(_FileName);
                    string _path = Path.Combine(Server.MapPath("~/Images"), id+".jpg");



                    file.SaveAs(_path);
                    Profile p1 = db.Profiles.SingleOrDefault(p => p.MID == id);
                    p1.Photo1 = "/Images/"+id+".jpg";
                    acc.Status = 2;
                    db.Entry(acc).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Message = "File Uploaded Successfully!!";
                    return RedirectToAction("Create", "Preferences");
                }
                ViewBag.Message = "Photo Required!";
                return View();






            }
            catch
            {
                ViewBag.Message = "File upload failed!!"; return View();
            }
        }
    }
}