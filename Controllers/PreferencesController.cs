using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EngineersMatrimony.Models;
using EngineersMatrimony.Filters;

namespace EngineersMatrimony.Controllers
{
    [UserAuth]
    public class PreferencesController : Controller
    {
        private EngineersMatrimonyEntities db = new EngineersMatrimonyEntities();

        // GET: Preferences
        public ActionResult Index()
        {
            var preferences = db.Preferences.Include(p => p.Account);
            return View(preferences.ToList());
        }

        // GET: Preferences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Preference preference = db.Preferences.Find(id);
            if (preference == null)
            {
                return HttpNotFound();
            }
            return View(preference);
        }

        // GET: Preferences/Create
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

        // POST: Preferences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PrefID,MID,AgeFrom,AgeTo,HeightFrom,HeightTo,WeightFrom,WeightTo,Job,MaritalStatus,FamilyType,IncomeFrom,IncomeTo,District,Gender")] Preference preference)
        {
            int id = Convert.ToInt32(Session["MID"].ToString());

            Account acc = db.Accounts.SingleOrDefault(s => s.MID == id);
            //if (String.IsNullOrEmpty(preference.AgeFrom.ToString().Trim())|| String.IsNullOrEmpty(preference.AgeTo.ToString().Trim())|| String.IsNullOrEmpty(preference.HeightFrom.ToString().Trim())|| String.IsNullOrEmpty(preference.HeightTo.ToString().Trim())
            //    || String.IsNullOrEmpty(preference.WeightFrom.ToString().Trim())|| String.IsNullOrEmpty(preference.WeightTo.ToString().Trim())|| String.IsNullOrEmpty(preference.Job.Trim())
            //    || String.IsNullOrEmpty(preference.MaritalStatus.Trim())|| String.IsNullOrEmpty(preference.FamilyType.Trim())|| String.IsNullOrEmpty(preference.Gender.Trim())||
            //     String.IsNullOrEmpty(preference.IncomeFrom.ToString().Trim()) || String.IsNullOrEmpty(preference.IncomeTo.ToString().Trim())
            //    )
            //{
            //    ModelState.AddModelError("", "All Fields Required");
            //}

             if (ModelState.IsValid)
            {
                preference.MID = id;

                db.Preferences.Add(preference);
                acc.Status = 3;
                db.Entry(acc).State = EntityState.Modified;
                db.SaveChanges();
                Response.Write("<script>Awaiting Activation! Signin Later.</script>");
                return RedirectToAction("SignIn","Accounts");
            }

            ViewBag.MID = new SelectList(db.Accounts, "MID", "Username", preference.MID);
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
            return View(preference);
        }

        // GET: Preferences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Preference preference = db.Preferences.Find(id);
            if (preference == null)
            {
                return HttpNotFound();
            }
            ViewBag.MID = new SelectList(db.Accounts, "MID", "Username", preference.MID);
            return View(preference);
        }

        // POST: Preferences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PrefID,MID,AgeFrom,AgeTo,HeightFrom,HeightTo,WeightFrom,WeightTo,Job,MaritalStatus,FamilyType,IncomeFrom,IncomeTo,District,Gender")] Preference preference)
        {
            if (ModelState.IsValid)
            {
                db.Entry(preference).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MID = new SelectList(db.Accounts, "MID", "Username", preference.MID);
            return View(preference);
        }

        // GET: Preferences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Preference preference = db.Preferences.Find(id);
            if (preference == null)
            {
                return HttpNotFound();
            }
            return View(preference);
        }

        // POST: Preferences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Preference preference = db.Preferences.Find(id);
            db.Preferences.Remove(preference);
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

