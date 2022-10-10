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
    
    public class AdminsController : Controller
    {
        private EngineersMatrimonyEntities db = new EngineersMatrimonyEntities();




        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Login(Admin admin)
        {


            Admin s1 = db.Admins.SingleOrDefault(s => s.Username == admin.Username);
            if (s1 != null)
            {
                if (s1.Username == admin.Username && admin.Password == s1.Password)
                {
                    Session["Uname"] = s1.Username;
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["Uname"] = "";
            return RedirectToAction("Login", "Admins");
        }

        // GET: Admins
        [AdminAuth]
        public ActionResult Index()
        {
            return View(db.Profiles.ToList().OrderByDescending(p=>p.MID).ToList());
        }



        public ActionResult Accept(Account acc)
        {
            Account s1 = db.Accounts.SingleOrDefault(s => s.Username == acc.Username);
            s1.Status = 4;
            db.Entry(s1).State = EntityState.Modified;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Decline(Account acc)
        {
            Account s1 = db.Accounts.SingleOrDefault(s => s.Username == acc.Username);
            s1.Status = -1;
            db.Entry(s1).State = EntityState.Modified;

            db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}