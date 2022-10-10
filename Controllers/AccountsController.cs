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
    
    public class AccountsController : Controller
    {
        private EngineersMatrimonyEntities db = new EngineersMatrimonyEntities();

        // GET: Accounts
        public ActionResult Index()
        {
            return View(db.Accounts.ToList());
        }
      
        // GET: Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }
        [HttpGet]
        // GET: SignIn
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(Account acc)
        {
            if (!ModelState.IsValid)
            {
                return View(acc);
            }
            else
            {
                Account acc1 = db.Accounts.SingleOrDefault(s => s.Username == acc.Username);
                if (acc1 != null)
                {
                    if (acc1.Username == acc.Username && acc1.Password == acc.Password)
                    {

                        Session["MID"] = acc1.MID;
                        if (acc1.Status == -1)
                        {
                            ModelState.AddModelError("", "Your Account is Deactivated!!");
                            return View(acc);
                        }
                        else if (acc1.Status == 0)
                        {
                            return RedirectToAction("Create", "Profiles");
                        }
                        else if (acc1.Status==1)
                        {
                            return RedirectToAction("UploadFile", "Upload");

                        }
                        else if(acc1.Status==2)
                        {
                            return RedirectToAction("Create", "Preferences");

                        }
                        else if (acc1.Status==3)
                        {
                            ModelState.AddModelError("","Awaiting Activation! Try again later.");
                            return View(acc);

                        }
                        else if(acc1.Status==4)
                        {
                            return RedirectToAction("Index", "Basic_Profile");

                        }
                        else
                        {
                            ModelState.AddModelError("", "SignIn Failed");
                            return View(acc);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "SignIn Failed");
                        return View(acc);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "SignIn Failed");
                    return View(acc);
                }
            }

        }
        public ActionResult Logout()
        {
            Session["MID"] = "";
            return RedirectToAction("SignIn","Accounts");
        }
        [HttpGet]
        // GET: Accounts/Create
        public ActionResult SignUp()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Include = "MID,Username,Password,Status")] Account account,String Cpassword)
        {
            if (ModelState.IsValid)
            {
                Account acc1 = db.Accounts.SingleOrDefault(s => s.Username == account.Username);
                if (acc1 == null)
                {
                    if (account.Password == Cpassword)
                    {
                        db.Accounts.Add(account);
                        db.SaveChanges();
                        return RedirectToAction("SignIn", "Accounts");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Passwords Mismatch!");
                        return View(account);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username already exists");
                    return View(account);
                }

            }



            return View(account);

            return View(account);
        }

        [HttpGet]
        // GET: Accounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MID,Username,Password,Status")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(account);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MID,Username,Password,Status")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
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
