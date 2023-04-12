using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Infectioncontrol.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //public ActionResult Index()
        //{
        //    ViewBag.Title = "Home Page";

        //    return View();
        //}

        public ActionResult Index()
        {
            
            return View();
        }
        [AllowAnonymous]
        public ActionResult Create()
        {

            return View();
        }

        public ActionResult EditAndDelete()
        {

            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            int account = 1;
            string[] username = new string[] { "3732" };
            string[] password = new string[] { "yrh3732" };
            int check = -1;
            try
            {
                for(int i = 0; i <= account; i++)
                    {
                        if(username[i] == Username && password[i] == Password)
                        {
                            check = 1;
                            break;
                        }
                    }
                    if(check == -1)
                    {
                        ViewBag.error = "帳密錯誤";
                    }
                    else
                    {
                        FormsAuthentication.RedirectFromLoginPage(Username, false);
                        return RedirectToAction("Index");
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ;
            }
            
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
