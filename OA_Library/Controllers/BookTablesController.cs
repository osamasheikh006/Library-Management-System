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
    public class BookTablesController : Controller
    {
        private OnlineLibraryMgtSystemDbEntities db = new OnlineLibraryMgtSystemDbEntities();

        // GET: BookTables
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var bookTables = db.BookTables.Include(b => b.BookTypeTable).Include(b => b.DepartmentTable).Include(b => b.UserTable);
            return View(bookTables.ToList());
        }

        // GET: BookTables/Details/5
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
            BookTable bookTable = db.BookTables.Find(id);
            if (bookTable == null)
            {
                return HttpNotFound();
            }
            return View(bookTable);
        }

        // GET: BookTables/Create
        public ActionResult Create()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.BookTypeID = new SelectList(db.BookTypeTables, "BookTypeID", "Name");
            ViewBag.DepartmentID = new SelectList(db.DepartmentTables, "DepartmentID", "Name");
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "UserName");
            return View();
        }

        // POST: BookTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookID,UserID,DepartmentID,BookTypeID,BookTitle,ShortDescription,Author,BookName,Edition,TotalCopies,RegDate,Price,Description")] BookTable bookTable)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.BookTables.Add(bookTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookTypeID = new SelectList(db.BookTypeTables, "BookTypeID", "Name", bookTable.BookTypeID);
            ViewBag.DepartmentID = new SelectList(db.DepartmentTables, "DepartmentID", "Name", bookTable.DepartmentID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "UserName", bookTable.UserID);
            return View(bookTable);
        }

        // GET: BookTables/Edit/5
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
            BookTable bookTable = db.BookTables.Find(id);
            if (bookTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookTypeID = new SelectList(db.BookTypeTables, "BookTypeID", "Name", bookTable.BookTypeID);
            ViewBag.DepartmentID = new SelectList(db.DepartmentTables, "DepartmentID", "Name", bookTable.DepartmentID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "UserName", bookTable.UserID);
            return View(bookTable);
        }

        // POST: BookTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookID,UserID,DepartmentID,BookTypeID,BookTitle,ShortDescription,Author,BookName,Edition,TotalCopies,RegDate,Price,Description")] BookTable bookTable)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(bookTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookTypeID = new SelectList(db.BookTypeTables, "BookTypeID", "Name", bookTable.BookTypeID);
            ViewBag.DepartmentID = new SelectList(db.DepartmentTables, "DepartmentID", "Name", bookTable.DepartmentID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "UserName", bookTable.UserID);
            return View(bookTable);
        }

        // GET: BookTables/Delete/5
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
            BookTable bookTable = db.BookTables.Find(id);
            if (bookTable == null)
            {
                return HttpNotFound();
            }
            return View(bookTable);
        }

        // POST: BookTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            BookTable bookTable = db.BookTables.Find(id);
            db.BookTables.Remove(bookTable);
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
