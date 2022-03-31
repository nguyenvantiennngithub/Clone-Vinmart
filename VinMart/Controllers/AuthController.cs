using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VinMart.Models;
using VinMart.utils;

namespace VinMart.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        VinMartEntities db = new VinMartEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {   
            if (Session[Constants.Instance.UserSession] != null) return RedirectToAction("Index", "Product");
            return View();
        }
        [HttpPost]
        public ActionResult Login(Account _acc)
        {

            Account account = db.Accounts.Where(a => a.username == _acc.username && a.password == _acc.password).FirstOrDefault();
            
            if (account != null)
            {
                UserSession userSession = new UserSession(account.username, account.displayName, (int)account.position, account.id);
                Session[Constants.Instance.UserSession] = userSession;
                return RedirectToAction("Index", "Product");
            }
            TempData["ErrorMessage"] = "Sai tài khoảng hoặc mật khẩu";
            return RedirectToAction("Login");

        }
    }
}