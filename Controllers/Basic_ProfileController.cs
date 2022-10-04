using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EngineersMatrimony.Models;

namespace EngineersMatrimony.Controllers
{
    public class Basic_ProfileController : Controller
    {
        private EngineersMatrimonyEntities db = new EngineersMatrimonyEntities();

        // GET: Basic_Profile
        public ActionResult Index()
        {
            return View(db.Basic_Profile.ToList());
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
