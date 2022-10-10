using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using EngineersMatrimony.Models;
using EngineersMatrimony.Filters;

namespace EngineersMatrimony.Controllers
{
    [UserAuth]
    public class Basic_ProfileController : Controller

    {
        private EngineersMatrimonyEntities db = new EngineersMatrimonyEntities();
        static Dictionary<int, int> scoreDict = new Dictionary<int, int>();

        // GET: Basic_Profile
        public ActionResult Intrested(Basic_Profile BP)
        {
            int id = Convert.ToInt32(Session["MID"].ToString());

            Interest intrest = new Interest();
            intrest.FromId = id;
            intrest.ToId = BP.MID;
            intrest.MatchScore = scoreDict[(int)BP.MID];
            //if (scoreDict.Contains(100003))
            //{
            //    intrest.MatchScore = scoreDict[100003];
            //}
            Interest existinterest = db.Interests.SingleOrDefault(ints => ints.ToId == BP.MID && ints.FromId==id);
            if (existinterest == null)
            {
                db.Interests.Add(intrest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
         



        
            return RedirectToAction("Index");


        }
        public ActionResult UnIntrested(Basic_Profile BP)
        {
            int id = Convert.ToInt32(Session["MID"].ToString());
            Interest interest = db.Interests.SingleOrDefault(ints => ints.ToId == BP.MID && ints.FromId == id);
            if (interest != null)
            {
                db.Interests.Remove(interest);
            }
            db.SaveChanges();
            return RedirectToAction("IntrestedProfilesView");
        }
        public ActionResult Decline(Basic_Profile BP)
        {
            int id = Convert.ToInt32(Session["MID"].ToString());
            Interest interest = db.Interests.SingleOrDefault(ints => ints.FromId == BP.MID && ints.ToId == id);
            if (interest != null)
            {
                db.Interests.Remove(interest);
            }
            db.SaveChanges();
            return RedirectToAction("IncommingIntrest");
        }
        public ActionResult Accept(Basic_Profile BP)
        {
            int id = Convert.ToInt32(Session["MID"].ToString());

            Interest intrest = new Interest();
            Interest interest = db.Interests.SingleOrDefault(ints => ints.FromId == BP.MID && ints.ToId==id);
            if (interest != null)
            {
                interest.Status = 1;
            }
            db.SaveChanges();
            return RedirectToAction("IncommingIntrest");
        }
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["MID"].ToString());


            Preference preference = db.Preferences.SingleOrDefault(pref => pref.MID == id);
            List<Basic_Profile> prefedBPs = new List<Basic_Profile>();
            int count = 0;
            scoreDict = new Dictionary<int, int>();
            foreach (Profile p in db.Profiles.ToList())
            {
                Account acc = db.Accounts.SingleOrDefault(a => a.MID == p.MID);
                count += 1;
                if (p.Gender == preference.Gender)
                {
                    int score = 0;
                    var date = DateTime.Now.Date.Subtract(p.DOB);
                    int age = (int)date.TotalDays / 365;
                    if (age >= preference.AgeFrom && age <= preference.AgeTo)
                    {
                        score += 1;
                    }
                    if (p.Height >= preference.HeightFrom && p.Height <= preference.HeightFrom)
                    {
                        score += 1;
                    }
                    if (p.Weight >= preference.AgeFrom && p.Weight <= preference.AgeTo)
                    {
                        score += 1;
                    }
                    if (p.Occupation == preference.Job)
                    {
                        score += 1;
                    }
                    if (preference.MaritalStatus == p.MStatus)
                    {
                        score += 1;
                    }
                    if (preference.FamilyType == p.FamilyType)
                    {
                        score += 1;
                    }
                    if (p.AnnualIncome >= preference.IncomeFrom && p.AnnualIncome <= preference.IncomeTo)
                    {
                        score += 1;
                    }
                    if (preference.District == p.District)
                    {
                        score += 1;
                    }
                    if (score >= 3 && acc.Status==4)
                    {
                        scoreDict.Add((int)p.MID, score);
                        RedirectToAction("<script>alert('Unseccessful. Try again.');</script>");
                        prefedBPs.Add(db.Basic_Profile.SingleOrDefault(bp => bp.MID == p.MID));
                       

                    }
                    
                }

            }
            //return View(db.Basic_Profile.ToList());
            List<int> intrestedIds = new List<int>();
            foreach (Interest interest in db.Interests.ToList())
            {
                if (interest.FromId == id)
                {
                    intrestedIds.Add((int)interest.ToId);
                }
                if (interest.ToId == id)
                {
                    intrestedIds.Add((int)interest.FromId);
                }
            }
            ViewBag.intrestedIds = intrestedIds;




            





            return View(prefedBPs);
        }

        public ActionResult IntrestedProfilesView()

        {
            int id = Convert.ToInt32(Session["MID"].ToString());
            
          

            Preference preference = db.Preferences.SingleOrDefault(pref => pref.MID == id);

            Dictionary<int, int> scoreDict1 = new Dictionary<int, int>();
            List<int> intrestedIds = new List<int>();
            foreach (Interest interest in db.Interests.ToList())
            {

                if (interest.FromId == id && interest.Status == 0)
                {
                    intrestedIds.Add((int)interest.ToId);
                }
                scoreDict1[(int)interest.ToId] = (int)interest.MatchScore;
            }
            ViewBag.intrestedIds = intrestedIds;
            ViewBag.MidScoreMap = scoreDict1;
            List<Basic_Profile> basic_pro = new List<Basic_Profile>();
           
            foreach (int i in intrestedIds)
            {
                Account acc= db.Accounts.SingleOrDefault(a => a.MID == i);
                if (acc.Status == 4)
                {
                    basic_pro.Add(db.Basic_Profile.SingleOrDefault(bp => bp.MID == i));
                }
            }

            return View(basic_pro);
   
        }
        public ActionResult IncommingIntrest()
        {
            int id = Convert.ToInt32(Session["MID"].ToString());

            Preference preference = db.Preferences.SingleOrDefault(pref => pref.MID == id);

            Dictionary<int, int> scoreDict1 = new Dictionary<int, int>();
            List<Basic_Profile> basic_pro = new List<Basic_Profile>();

            List<int> intrestedIds = new List<int>();
            foreach (Interest interest in db.Interests.ToList())
            {
                if (interest.ToId == id && interest.Status == 0)
                {
                    intrestedIds.Add((int)interest.FromId);
                }
                scoreDict1[(int)interest.FromId] = (int)interest.MatchScore;
            }
            ViewBag.intrestedIds = intrestedIds;
            ViewBag.MidScoreMap = scoreDict1;
            foreach (int i in intrestedIds)
            {
                Account acc = db.Accounts.SingleOrDefault(a => a.MID == i);
                if (acc.Status == 4)
                {
                    basic_pro.Add(db.Basic_Profile.SingleOrDefault(bp => bp.MID == i));
                }
            }

            return View(basic_pro);
        }

        public ActionResult Mutual()
        {
            int id = Convert.ToInt32(Session["MID"].ToString());

            Preference preference = db.Preferences.SingleOrDefault(pref => pref.MID == id);

            Dictionary<int, int> scoreDict1 = new Dictionary<int, int>();
            List<int> intrestedIds = new List<int>();
            List<Basic_Profile> basic_pro = new List<Basic_Profile>();

            foreach (Interest interest in db.Interests.ToList())
            {
                if ((interest.FromId == id||interest.ToId==id) && interest.Status == 1)
                {
                    if (interest.FromId == id) {
                        intrestedIds.Add((int)interest.ToId);
                    }
                    else
                    {
                        intrestedIds.Add((int)interest.FromId);
                    }
                    
                }
                scoreDict1[(int)interest.FromId] = (int)interest.MatchScore;
            }
            ViewBag.intrestedIds = intrestedIds;
            ViewBag.MidScoreMap = scoreDict1;
            // return View(db.Basic_Profile.ToList());
            foreach (int i in intrestedIds)
            {
                Account acc = db.Accounts.SingleOrDefault(a => a.MID == i);
                if (acc.Status == 4)
                {
                    basic_pro.Add(db.Basic_Profile.SingleOrDefault(bp => bp.MID == i));
                }
            }

            return View(basic_pro);


        }
        public ActionResult ViewProfile(Basic_Profile BP)
        {
            Session["PID"] = BP.MID;

            return RedirectToAction("Details", "Profiles");
        }

        // GET: Basic_Profile/Details/5
        public ActionResult Details(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Basic_Profile basic_Profile = db.Basic_Profile.Find(id);
            if (basic_Profile == null)
            {
                return HttpNotFound();
            }
            return View(basic_Profile);
        }

        // GET: Basic_Profile/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Basic_Profile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MID,DOB,District,Photo1")] Basic_Profile basic_Profile)
        {
            if (ModelState.IsValid)
            {
                db.Basic_Profile.Add(basic_Profile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(basic_Profile);
        }

        // GET: Basic_Profile/Edit/5
        public ActionResult Edit(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Basic_Profile basic_Profile = db.Basic_Profile.Find(id);
            if (basic_Profile == null)
            {
                return HttpNotFound();
            }
            return View(basic_Profile);
        }

        // POST: Basic_Profile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MID,DOB,District,Photo1")] Basic_Profile basic_Profile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(basic_Profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(basic_Profile);
        }

        // GET: Basic_Profile/Delete/5
        public ActionResult Delete(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Basic_Profile basic_Profile = db.Basic_Profile.Find(id);
            if (basic_Profile == null)
            {
                return HttpNotFound();
            }
            return View(basic_Profile);
        }

        // POST: Basic_Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime id)
        {
            Basic_Profile basic_Profile = db.Basic_Profile.Find(id);
            db.Basic_Profile.Remove(basic_Profile);
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

