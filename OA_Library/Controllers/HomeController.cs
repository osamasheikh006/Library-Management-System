using OA_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA_Library.Controllers
{
    public class HomeController : Controller

    {
        private OnlineLibraryMgtSystemDbEntities db = new OnlineLibraryMgtSystemDbEntities();

        public ActionResult Login()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult LoginUser(string username, string password)
        {
            try
            {

                if (username != null && password != null)
                {
                    var finduser = db.UserTables.Where(u => u.UserName == username && u.Password == password && u.IsActive == true).ToList();
                    if (finduser.Count() == 1)
                    {
                        Session["UserID"] = finduser[0].UserID;

                        Session["UserTypeID"] = finduser[0].UserTypeID;

                        Session["UserName"] = finduser[0].UserName;

                        Session["Password"] = finduser[0].Password;
                        Session["EmployeeID"] = finduser[0].EmployeeID;

                        //UserTypeID UserTypellame

                        // 1 Admin

                        // 2 Operator 
                        // 3 Teacher

                        // 4 Student

                        string url = string.Empty;

                        if (finduser[0].UserTypeID == 2)
                        {
                            return RedirectToAction("About");
                        }
                        else if (finduser[0].UserTypeID == 3)
                        {
                            return RedirectToAction("About");
                        }
                        else if (finduser[0].UserTypeID == 4)
                        {
                            return RedirectToAction("About");
                        }
                        else if (finduser[0].UserTypeID == 1)
                        {
                            url = "About";
                        }
                        else
                        {
                            url = "About";
                        }

                        return RedirectToAction(url);
                    }
                    else
                    {
                        Session["UserID"] = string.Empty;
                        Session["UserTypeID"] = string.Empty;
                        Session["UserName"] = string.Empty;
                        Session["Password"] = string.Empty;
                        Session["EmployeeID"] = string.Empty;
                        ViewBag.message = "User Name and Password is incorrect!";
                    }
                }
                else
                {
                    Session["UserID"] = string.Empty;

                    Session["UserTypeID"] = string.Empty;

                    Session["UserName"] = string.Empty;

                    Session["Password"] = string.Empty;
                    Session["EmployeeID"] = string.Empty;
                    ViewBag.message = "Some unexpected issue is occure please try again!";
                }
            }
            catch (Exception ex)
            {
                Session["UserID"] = string.Empty;

                Session["UserTypeID"] = string.Empty;

                Session["UserName"] = string.Empty;

                Session["Password"] = string.Empty;
                Session["EmployeeID"] = string.Empty;

                ViewBag.message = "Some unexpected issue is occure please try again!";
            }
            return View("Login");
        }


        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult About()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            int usertypeid = 0;
            if (Session["UserTypeID"] != null)
            {
                usertypeid = Convert.ToInt32(Convert.ToString(Session["UserTypeID"]));
            }
            if (usertypeid > 1)
            {
                return RedirectToAction("Index", "ReserveBook", null);
            }

            return View();
        }

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
        public ActionResult Logout()
        {

            Session["UserID"] = string.Empty;

            Session["UserTypeID"] = string.Empty;

            Session["UserName"] = string.Empty;

            Session["Password"] = string.Empty;
            Session["EmployeeID"] = string.Empty;
            return RedirectToAction("Login");
        }
    }
}