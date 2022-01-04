using OA_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA_Library.Controllers
{
    public class ReturnBooksController : Controller
    {
        private OnlineLibraryMgtSystemDbEntities db = new OnlineLibraryMgtSystemDbEntities();
        // GET: ReturnBook
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var returnbooks = db.BookReturnTables.ToList();
            return View(returnbooks);
        }
    }
}