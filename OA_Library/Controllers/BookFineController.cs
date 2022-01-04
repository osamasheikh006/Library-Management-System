using OA_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA_Library.Controllers
{
    public class BookFineController : Controller
    {
        private OnlineLibraryMgtSystemDbEntities db = new OnlineLibraryMgtSystemDbEntities();
        // GET: BookFine
        public ActionResult PendindFine()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var pendingfine = db.BookFineTables.Where(f => f.ReceiveAmount == 0);
            return View(pendingfine.ToList());

        }

        public ActionResult FineHistory()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var pendingfine = db.BookFineTables.Where(f => f.ReceiveAmount > 0);
            return View(pendingfine.ToList());
        }
        public ActionResult SubmitFine(int? id)
        {
            var fine = db.BookFineTables.Find(id);
            fine.ReceiveAmount = fine.FineAmount;
            fine.FineDate = DateTime.Now;
            db.Entry(fine).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("PendindFine");
        }
    }
}