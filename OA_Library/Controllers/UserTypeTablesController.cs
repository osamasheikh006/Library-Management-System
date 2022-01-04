using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OA_Library.Models;

namespace OA_Library.Controllers
{
    public class UserTypeTablesController : Controller
    {
        private OnlineLibraryMgtSystemDbEntities db = new OnlineLibraryMgtSystemDbEntities();

        // GET: UserTypeTables
        public ActionResult Index()
        {

            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(db.UserTypeTables.ToList());
        }

        // GET: UserTypeTables/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTypeTable userTypeTable = db.UserTypeTables.Find(id);
            if (userTypeTable == null)
            {
                return HttpNotFound();
            }
            return View(userTypeTable);
        }

        // GET: UserTypeTables/Create
        public ActionResult Create()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        // POST: UserTypeTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserTypeID,UserType")] UserTypeTable userTypeTable)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.UserTypeTables.Add(userTypeTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userTypeTable);
        }

        // GET: UserTypeTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTypeTable userTypeTable = db.UserTypeTables.Find(id);
            if (userTypeTable == null)
            {
                return HttpNotFound();
            }
            return View(userTypeTable);
        }

        // POST: UserTypeTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserTypeID,UserType")] UserTypeTable userTypeTable)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(userTypeTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userTypeTable);
        }

        // GET: UserTypeTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTypeTable userTypeTable = db.UserTypeTables.Find(id);
            if (userTypeTable == null)
            {
                return HttpNotFound();
            }
            return View(userTypeTable);
        }

        // POST: UserTypeTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            UserTypeTable userTypeTable = db.UserTypeTables.Find(id);
            db.UserTypeTables.Remove(userTypeTable);
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
