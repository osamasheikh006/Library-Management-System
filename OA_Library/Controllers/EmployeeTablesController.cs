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
    public class EmployeeTablesController : Controller
    {
        private OnlineLibraryMgtSystemDbEntities db = new OnlineLibraryMgtSystemDbEntities();

        // GET: EmployeeTables
        public ActionResult Index()
        {

            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var employeeTables = db.EmployeeTables.Include(e => e.DepartmentTable).Include(e => e.DesignationTable);
            return View(employeeTables.ToList());
        }

        // GET: EmployeeTables/Details/5
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
            EmployeeTable employeeTable = db.EmployeeTables.Find(id);
            if (employeeTable == null)
            {
                return HttpNotFound();
            }
            return View(employeeTable);
        }

        // GET: EmployeeTables/Create
        public ActionResult Create()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.DepartmentID = new SelectList(db.DepartmentTables, "DepartmentID", "Name");
            ViewBag.DesignationID = new SelectList(db.DesignationTables, "DesignationID", "Name");
            return View();
        }

        // POST: EmployeeTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,UserID,FullName,FatherName,ContactNo,Email,Address,DesignationID,DepartmentID,IsActive,Description")] EmployeeTable employeeTable)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.EmployeeTables.Add(employeeTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.DepartmentTables, "DepartmentID", "Name", employeeTable.DepartmentID);
            ViewBag.DesignationID = new SelectList(db.DesignationTables, "DesignationID", "Name", employeeTable.DesignationID);
            return View(employeeTable);
        }

        // GET: EmployeeTables/Edit/5
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
            EmployeeTable employeeTable = db.EmployeeTables.Find(id);
            if (employeeTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.DepartmentTables, "DepartmentID", "Name", employeeTable.DepartmentID);
            ViewBag.DesignationID = new SelectList(db.DesignationTables, "DesignationID", "Name", employeeTable.DesignationID);
            return View(employeeTable);
        }

        // POST: EmployeeTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,UserID,FullName,FatherName,ContactNo,Email,Address,DesignationID,DepartmentID,IsActive,Description")] EmployeeTable employeeTable)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(employeeTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.DepartmentTables, "DepartmentID", "Name", employeeTable.DepartmentID);
            ViewBag.DesignationID = new SelectList(db.DesignationTables, "DesignationID", "Name", employeeTable.DesignationID);
            return View(employeeTable);
        }

        // GET: EmployeeTables/Delete/5
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
            EmployeeTable employeeTable = db.EmployeeTables.Find(id);
            if (employeeTable == null)
            {
                return HttpNotFound();
            }
            return View(employeeTable);
        }

        // POST: EmployeeTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            EmployeeTable employeeTable = db.EmployeeTables.Find(id);
            db.EmployeeTables.Remove(employeeTable);
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
