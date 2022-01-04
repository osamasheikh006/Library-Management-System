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
    public class IssueBookTablesController : Controller
    {
        private OnlineLibraryMgtSystemDbEntities db = new OnlineLibraryMgtSystemDbEntities();
        
        public ActionResult IssueBooks()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Message = Message;
            Message = string.Empty;
            var issueBookTables = db.IssueBookTables.Include(i => i.UserTable).Include(i => i.BookTable).Include(i => i.EmployeeTable).Where(b => b.Status == true && b.ReserveNoOfCopies == false);
            return View(issueBookTables.ToList());
        }

        public ActionResult ReserveBook()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var issueBookTables = db.IssueBookTables.Include(i => i.UserTable).Include(i => i.BookTable).Include(i => i.EmployeeTable).Where(b => b.Status == false && b.ReserveNoOfCopies == true && b.ReturnDate > DateTime.Now);
            return View(issueBookTables.ToList());
        }

        public ActionResult ReturnPendingBOOKS()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            //var issueBookTables = db.IssueBookTables.Include(i => i.UserTable).Include(i => i.BookTable).Include(i => i.EmployeeTable).Where(b => b.Status == true && b.ReserveNoOfCopies == false && b.ReturnDate > DateTime.Now);
            //return View(issueBookTables.ToList());
            List<IssueBookTable> list = new List<IssueBookTable>();
            var issueBookTables = db.IssueBookTables.Where(b => b.Status == true && b.ReserveNoOfCopies == false).ToList();
            foreach (var item in issueBookTables)
            {
                var returndate = item.ReturnDate;
                int noofday = (returndate - DateTime.Now).Days;
                if (noofday <= 3)
                {
                    list.Add(new IssueBookTable
                    {
                        BookID = item.BookID,
                        BookTable = item.BookTable,
                        Description = item.Description,
                        EmployeeID = item.EmployeeID,
                        EmployeeTable = item.EmployeeTable,
                        IssueBookID = item.IssueBookID,
                        IssueCopies = item.IssueCopies,
                        IssueDate = item.IssueDate,
                        ReserveNoOfCopies = item.ReserveNoOfCopies,
                        ReturnDate = item.ReturnDate,
                        Status = item.Status,
                        UserID = item.UserID,
                        UserTable = item.UserTable


                    });
                }
            }
            return View(list.ToList());
        }


        // GET: IssueBookTables
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var issueBookTables = db.IssueBookTables.Include(i => i.BookTable).Include(i => i.EmployeeTable).Include(i => i.UserTable);
            return View(issueBookTables.ToList());
        }

        // GET: IssueBookTables/Details/5
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
            IssueBookTable issueBookTable = db.IssueBookTables.Find(id);
            if (issueBookTable == null)
            {
                return HttpNotFound();
            }
            return View(issueBookTable);
        }
        public static string Message { get; set; }
        // GET: IssueBookTables/Create
        public ActionResult Create()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Message = Message;
            Message = string.Empty;
            ViewBag.BookID = new SelectList(db.BookTables, "BookID", "BookTitle","0");
            ViewBag.EmployeeID = new SelectList(db.EmployeeTables, "EmployeeID", "FullName", "0");
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "UserName", "0");
            return View();
        }

        // POST: IssueBookTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598. [Bind(Include = "IssueBookID,UserID,BookID,EmployeeID,IssueCopies,IssueDate,ReturnDate,Status,Description,ReserveNoOfCopies")] 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IssueBookTable issueBookTable)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var find = db.IssueBookTables.Where(b => b.ReturnDate >= DateTime.Now && b.BookID == issueBookTable.BookID && (b.Status == true || b.ReserveNoOfCopies == true)).ToList();
                int issuebooks = 0;
                foreach (var item in find)
                {
                    issuebooks = issuebooks + item.IssueCopies;
                }
                var stockbook = db.BookTables.Where(b =>b.BookID == issueBookTable.BookID).FirstOrDefault();
                if ((issuebooks == stockbook.TotalCopies) || (issuebooks + issueBookTable.IssueCopies > stockbook.TotalCopies))
                {
                    Message = "Stock is Empty";
                   // return View(issueBookTable);
                    return RedirectToAction("IssueBooks");
                }
                db.IssueBookTables.Add(issueBookTable);
                db.SaveChanges();
                Message = "Book Issue Successfully!";
                return RedirectToAction("IssueBooks");

            }

            //if (ModelState.IsValid)
            //{
            //    db.IssueBookTables.Add(issueBookTable);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            ViewBag.BookID = new SelectList(db.BookTables, "BookID", "BookTitle", issueBookTable.BookID);
            ViewBag.EmployeeID = new SelectList(db.EmployeeTables, "EmployeeID", "FullName", issueBookTable.EmployeeID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "UserName", issueBookTable.UserID);
            return View(issueBookTable);
        }
        public ActionResult ApproveRequest(int? id)
        {
            var request = db.IssueBookTables.Find(id);
            request.ReserveNoOfCopies = false;
            request.Status = true;
            request.Description = "Approve";
            db.Entry(request).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ReserveBook");
        }

        public ActionResult BookReturn(int? id)
        {

            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var book = db.IssueBookTables.Find(id);
            int fine=0;
            var returndate = book.ReturnDate;

            int noofday = (DateTime.Now- returndate).Days;
            
            if (book.Status == true && book.ReserveNoOfCopies == false)
            {
                if (noofday > 0)
                {
                    fine = 20 * noofday;
                }
                var returnbook = new BookReturnTable()
                {
                    UserID = book.UserID,
                    BookID = book.BookID,
                    EmployeeID = book.EmployeeID,
                    IssueDate = book.IssueDate,
                    ReturnDate = book.ReturnDate,
                    CurrentDate = DateTime.Now,

                };
                db.BookReturnTables.Add(returnbook);
                db.SaveChanges();
            }
            
            book.Status = false;
            book.ReserveNoOfCopies = false;
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
            if (fine > 0)
            {
                var addfine = new BookFineTable()
                {
                    UserID = book.UserID,
                    BookID = book.BookID,
                    EmployeeID = book.EmployeeID,
                    FineAmount = fine,
                    FineDate = DateTime.Now,
                    NoOfDays = noofday,
                    ReceiveAmount = 0,
                    
                };
                db.BookFineTables.Add(addfine);
                db.SaveChanges();
            }
            return RedirectToAction("IssueBooks");
        }
        public ActionResult PendindFine()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var pendingfine = db.BookFineTables.Where(f => f.ReceiveAmount == 0);
            return View(pendingfine.ToList());
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
