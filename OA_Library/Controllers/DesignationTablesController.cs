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
    public class DesignationTablesController : Controller
    {
        private OnlineLibraryMgtSystemDbEntities db = new OnlineLibraryMgtSystemDbEntities();

        // GET: DesignationTables
        public ActionResult Index()
        {

            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var designationTables = db.DesignationTables.Include(d => d.UserTable);
            return View(designationTables.ToList());
        }

        // GET: DesignationTables/Details/5
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
            DesignationTable designationTable = db.DesignationTables.Find(id);
            if (designationTable == null)
            {
                return HttpNotFound();
            }
            return View(designationTable);
        }

        // GET: DesignationTables/Create
        public ActionResult Create()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "UserName");
            return View();
        }

        // POST: DesignationTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DesignationID,Name,UserID,Scale")] DesignationTable designationTable)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.DesignationTables.Add(designationTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "UserName", designationTable.UserID);
            return View(designationTable);
        }

        // GET: DesignationTables/Edit/5
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
            DesignationTable designationTable = db.DesignationTables.Find(id);
            if (designationTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "UserName", designationTable.UserID);
            return View(designationTable);
        }

        // POST: DesignationTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DesignationID,Name,UserID,Scale")] DesignationTable designationTable)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(designationTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "UserName", designationTable.UserID);
            return View(designationTable);
        }

        // GET: DesignationTables/Delete/5
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
            DesignationTable designationTable = db.DesignationTables.Find(id);
            if (designationTable == null)
            {
                return HttpNotFound();
            }
            return View(designationTable);
        }

        // POST: DesignationTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            DesignationTable designationTable = db.DesignationTables.Find(id);
            db.DesignationTables.Remove(designationTable);
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
