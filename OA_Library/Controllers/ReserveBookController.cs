using OA_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA_Library.Controllers
{
    public class ReserveBookController : Controller
    {
        private OnlineLibraryMgtSystemDbEntities db = new OnlineLibraryMgtSystemDbEntities();
        public static string Message { get; set; }
        // GET: ReserveBook
        public ActionResult Index(string Search)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Message = Message;
            Message = string.Empty;
            var books = db.BookTables.Where(x => x.BookTitle.Contains(Search) || Search == null).ToList();

            return View(books);
        }
        public ActionResult ReserveBook(int? id)
        {
            var book = db.BookTables.Find(id);
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            int employeeid = Convert.ToInt32(Convert.ToString(Session["EmployeeID"]));
            var issueBookTable = new IssueBookTable()
            {
                BookID = book.BookID,
                Description = "Reserve Request",
                EmployeeID = employeeid,
                IssueCopies = 1,
                IssueDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddDays(2),
                Status = false,
                ReserveNoOfCopies = true,
                UserID = book.UserID

            };
            if (ModelState.IsValid)
            {
                var find = db.IssueBookTables.Where(b => b.ReturnDate >= DateTime.Now && b.BookID == issueBookTable.BookID && (b.Status == true || b.ReserveNoOfCopies == true)).ToList();
                int count = 0;
                foreach (var item in find)
                {
                    count = count + item.IssueCopies;
                }
                var stockbook = db.BookTables.Where(b => b.BookID == issueBookTable.BookID).FirstOrDefault();
                if ((count == stockbook.TotalCopies) || (count + issueBookTable.IssueCopies > stockbook.TotalCopies))
                {
                    Message = "Stock is Empty";
                    return RedirectToAction("Index");

                }
                db.IssueBookTables.Add(issueBookTable);
                db.SaveChanges();
                Message = "Book Issue Successfully!";
                return RedirectToAction("Index");

            }

            return RedirectToAction("Index");

        }
    }
}