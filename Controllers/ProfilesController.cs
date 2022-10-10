using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using EngineersMatrimony.Models;

using EngineersMatrimony.Filters;



namespace EngineersMatrimony.Controllers
{
    [UserAuth]
    public class ProfilesController : Controller
    {
        private EngineersMatrimonyEntities db = new EngineersMatrimonyEntities();



        // GET: Profiles
        public ActionResult Index()
        {
            var profiles = db.Profiles.Include(p => p.Account);
            return View(profiles.ToList());
        }



        // GET: Profiles/Details/5
        public ActionResult Details()
        {
            int id = Convert.ToInt32(Session["PID"].ToString());



            Profile profile = db.Profiles.SingleOrDefault(p => p.MID == id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }



        // GET: Profiles/Create
        public ActionResult Create()
        {
            ViewBag.MID = new SelectList(db.Accounts, "MID", "Username");
            ViewBag.Genders = new List<SelectListItem>()
            {
                new SelectListItem{Text="Male" ,Value="Male"},
                new SelectListItem{Text="Female", Value= "Female"},
                new SelectListItem{Text="Others", Value = "Others"}
            };
            ViewBag.Districts = new List<SelectListItem>()
            {
               new SelectListItem{Text="Kasaragod" ,Value="Kasaragod"},
                new SelectListItem{Text="Kannur", Value= "Kannur"},
                new SelectListItem{Text="Kozhikode", Value = "Kozhikode"},
                new SelectListItem{Text="Wayanad", Value = "Wayanad"},
                new SelectListItem{Text="Malappuram", Value = "Malappuram"},
                new SelectListItem{Text="Palakkad", Value = "Palakkad"},
                new SelectListItem{Text="Thrissur", Value = "Thrissur"},
                new SelectListItem{Text="Ernakulam", Value = "Ernakulam"},
                new SelectListItem{Text="Kottayam", Value = "Kottayam"},
                new SelectListItem{Text="Alappuzha", Value = "Alappuzha"},
                new SelectListItem{Text="Pathanamthitta", Value = "Pathanamthitta"},
                new SelectListItem{Text="Idukki", Value = "Idukki"},
                new SelectListItem{Text="Kollam", Value = "Kollam"},
                new SelectListItem{Text="Thiruvananthapuram", Value = "Thiruvananthapuram"},
            };
            ViewBag.FamType = new List<SelectListItem>()
            {
                new SelectListItem{Text = "Joint", Value = "Joint"},
                new SelectListItem{Text = "Nuclear", Value = "Nuclear"}



           };



            ViewBag.MStatus = new List<SelectListItem>()
            {
               new SelectListItem{Text = "Single", Value = "Single"},
               new SelectListItem{Text = "Divorced", Value = "Divorced"},
               new SelectListItem{Text = "Widowed", Value = "Widowed"},
               new SelectListItem{Text = "Any", Value = "Any"},
            };
            return View();
        }



        // POST: Profiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RegID,PName,Email,DOB,Phone,Education,Gender,District,Height,Weight,Occupation,MStatus,FamilyType,AnnualIncome")] Profile profile)
        {

            //Account acc = db.Accounts.SingleOrDefault(s => s.Username == profile.Account.Username);
            int id = Convert.ToInt32(Session["MID"].ToString());
            Account acc = db.Accounts.SingleOrDefault(s => s.MID ==id);



            profile.MID = acc.MID;
            profile.Photo1 = "null";
            //if (String.IsNullOrEmpty(profile.PName.Trim()) || String.IsNullOrEmpty(profile.Email.Trim()) || String.IsNullOrEmpty(profile.DOB.ToString().Trim()) || String.IsNullOrEmpty(profile.Phone.Trim()) || String.IsNullOrEmpty(profile.Education.Trim()) || String.IsNullOrEmpty(profile.Gender.Trim()) || String.IsNullOrEmpty(profile.District.Trim())
            //    || String.IsNullOrEmpty(profile.Height.ToString().Trim()) || String.IsNullOrEmpty(profile.Weight.ToString().Trim()) || String.IsNullOrEmpty(profile.MStatus.Trim()) || String.IsNullOrEmpty(profile.FamilyType.Trim()) || String.IsNullOrEmpty(profile.AnnualIncome.ToString().Trim())){

            //    ModelState.AddModelError("", "All fields are required!!");
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    db.Profiles.Add(profile);
                    acc.Status = 1;
                    db.Entry(acc).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("UploadFile", "Upload");

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "All Fields Required");
                
                }
                return View();
            }




            ViewBag.MID = new SelectList(db.Accounts, "MID", "Username", profile.MID);
            ViewBag.Genders = new List<SelectListItem>()
            {
                new SelectListItem{Text="Male" ,Value="Male"},
                new SelectListItem{Text="Female", Value= "Female"},
                new SelectListItem{Text="Others", Value = "Others"}
            };
            ViewBag.Districts = new List<SelectListItem>()
            {
               new SelectListItem{Text="Kasaragod" ,Value="Kasaragod"},
                new SelectListItem{Text="Kannur", Value= "Kannur"},
                new SelectListItem{Text="Kozhikode", Value = "Kozhikode"},
                new SelectListItem{Text="Wayanad", Value = "Wayanad"},
                new SelectListItem{Text="Malappuram", Value = "Malappuram"},
                new SelectListItem{Text="Palakkad", Value = "Palakkad"},
                new SelectListItem{Text="Thrissur", Value = "Thrissur"},
                new SelectListItem{Text="Ernakulam", Value = "Ernakulam"},
                new SelectListItem{Text="Kottayam", Value = "Kottayam"},
                new SelectListItem{Text="Alappuzha", Value = "Alappuzha"},
                new SelectListItem{Text="Pathanamthitta", Value = "Pathanamthitta"},
                new SelectListItem{Text="Idukki", Value = "Idukki"},
                new SelectListItem{Text="Kollam", Value = "Kollam"},
                new SelectListItem{Text="Thiruvananthapuram", Value = "Thiruvananthapuram"},
            };
            ViewBag.FamType = new List<SelectListItem>()
            {
                new SelectListItem{Text = "Joint", Value = "Joint"},
                new SelectListItem{Text = "Nuclear", Value = "Nuclear"}



           };



            ViewBag.MStatus = new List<SelectListItem>()
            {
               new SelectListItem{Text = "Single", Value = "Single"},
               new SelectListItem{Text = "Divorced", Value = "Divorced"},
               new SelectListItem{Text = "Widowed", Value = "Widowed"},
               new SelectListItem{Text = "Any", Value = "Any"},
            };
            return View(profile);
        }



        // GET: Profiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            ViewBag.MID = new SelectList(db.Accounts, "MID", "Username", profile.MID);
            return View(profile);
        }



        // POST: Profiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RegID,MID,PName,Email,DOB,Phone,Education,Gender,District,Height,Weight,Occupation,MStatus,Photo1,Photo2,Photo3,FamilyType,AnnualIncome")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MID = new SelectList(db.Accounts, "MID", "Username", profile.MID);
            return View(profile);
        }



        // GET: Profiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }



        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Profile profile = db.Profiles.Find(id);
            db.Profiles.Remove(profile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}