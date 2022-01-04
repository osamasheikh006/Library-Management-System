﻿using System;
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
    public class BookReturnTablesController : Controller
    {
        private OnlineLibraryMgtSystemDbEntities db = new OnlineLibraryMgtSystemDbEntities();

        // GET: BookReturnTables
        public ActionResult Index()
        {

            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var bookReturnTables = db.BookReturnTables.Include(b => b.BookTable).Include(b => b.EmployeeTable).Include(b => b.UserTable);
            return View(bookReturnTables.ToList());
        }

        // GET: BookReturnTables/Details/5
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
            BookReturnTable bookReturnTable = db.BookReturnTables.Find(id);
            if (bookReturnTable == null)
            {
                return HttpNotFound();
            }
            return View(bookReturnTable);
        }

        // GET: BookReturnTables/Create
        public ActionResult Create()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.BookID = new SelectList(db.BookTables, "BookID", "BookTitle");
            ViewBag.EmployeeID = new SelectList(db.EmployeeTables, "EmployeeID", "FullName");
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "UserName");
            return View();
        }

        // POST: BookReturnTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookReturnID,BookID,EmployeeID,IssueDate,ReturnDate,CurrentDate,UserID")] BookReturnTable bookReturnTable)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.BookReturnTables.Add(bookReturnTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookID = new SelectList(db.BookTables, "BookID", "BookTitle", bookReturnTable.BookID);
            ViewBag.EmployeeID = new SelectList(db.EmployeeTables, "EmployeeID", "FullName", bookReturnTable.EmployeeID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "UserName", bookReturnTable.UserID);
            return View(bookReturnTable);
        }

        // GET: BookReturnTables/Edit/5
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
            BookReturnTable bookReturnTable = db.BookReturnTables.Find(id);
            if (bookReturnTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookID = new SelectList(db.BookTables, "BookID", "BookTitle", bookReturnTable.BookID);
            ViewBag.EmployeeID = new SelectList(db.EmployeeTables, "EmployeeID", "FullName", bookReturnTable.EmployeeID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "UserName", bookReturnTable.UserID);
            return View(bookReturnTable);
        }

        // POST: BookReturnTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookReturnID,BookID,EmployeeID,IssueDate,ReturnDate,CurrentDate,UserID")] BookReturnTable bookReturnTable)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(bookReturnTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookID = new SelectList(db.BookTables, "BookID", "BookTitle", bookReturnTable.BookID);
            ViewBag.EmployeeID = new SelectList(db.EmployeeTables, "EmployeeID", "FullName", bookReturnTable.EmployeeID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "UserName", bookReturnTable.UserID);
            return View(bookReturnTable);
        }

        // GET: BookReturnTables/Delete/5
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
            BookReturnTable bookReturnTable = db.BookReturnTables.Find(id);
            if (bookReturnTable == null)
            {
                return HttpNotFound();
            }
            return View(bookReturnTable);
        }

        // POST: BookReturnTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            BookReturnTable bookReturnTable = db.BookReturnTables.Find(id);
            db.BookReturnTables.Remove(bookReturnTable);
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
