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
    public class InterestsController : Controller
    {
        private EngineersMatrimonyEntities db = new EngineersMatrimonyEntities();

        // GET: Interests
        public ActionResult Index()
        {
            var interests = db.Interests.Include(i => i.Account).Include(i => i.Account1);
            return View(interests.ToList());
        }

        // GET: Interests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interest interest = db.Interests.Find(id);
            if (interest == null)
            {
                return HttpNotFound();
            }
            return View(interest);
        }

        // GET: Interests/Create
        public ActionResult Create()
        {
            ViewBag.FromId = new SelectList(db.Accounts, "MID", "Username");
            ViewBag.ToId = new SelectList(db.Accounts, "MID", "Username");
            return View();
        }

        // POST: Interests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IntID,FromId,ToId,MatchScore,Status")] Interest interest)
        {
            if (ModelState.IsValid)
            {
                db.Interests.Add(interest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FromId = new SelectList(db.Accounts, "MID", "Username", interest.FromId);
            ViewBag.ToId = new SelectList(db.Accounts, "MID", "Username", interest.ToId);
            return View(interest);
        }

        // GET: Interests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interest interest = db.Interests.Find(id);
            if (interest == null)
            {
                return HttpNotFound();
            }
            ViewBag.FromId = new SelectList(db.Accounts, "MID", "Username", interest.FromId);
            ViewBag.ToId = new SelectList(db.Accounts, "MID", "Username", interest.ToId);
            return View(interest);
        }

        // POST: Interests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IntID,FromId,ToId,MatchScore,Status")] Interest interest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(interest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FromId = new SelectList(db.Accounts, "MID", "Username", interest.FromId);
            ViewBag.ToId = new SelectList(db.Accounts, "MID", "Username", interest.ToId);
            return View(interest);
        }

        // GET: Interests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interest interest = db.Interests.Find(id);
            if (interest == null)
            {
                return HttpNotFound();
            }
            return View(interest);
        }

        // POST: Interests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Interest interest = db.Interests.Find(id);
            db.Interests.Remove(interest);
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
