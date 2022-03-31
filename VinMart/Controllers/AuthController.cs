using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VinMart.Models;
using VinMart.Models.RES;
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
                UserSession userSession = new UserSession(account.username, account.displayName, (int)account.position, account.id, account.email);
                Session[Constants.Instance.UserSession] = userSession;
                return RedirectToAction("Index", "Product");
            }
            TempData["ErrorMessage"] = "Sai tài khoảng hoặc mật khẩu";
            return RedirectToAction("Login");

        }

        public ActionResult Register()
        {
            return View();
        }
        //mothod xử lý model
        [HttpPost]
        public ActionResult Register(AccountRES account)
        {
            VinMartEntities db = new VinMartEntities();//kêt nối cơ sở dữ liệu

            //kiểm tra dữ liệu truyền vào model có hợp lệ không
            if (account.DisplayName != null && account.Email != null && account.Password != null && account.Username != null && account.Password == account.Confirmpasword)
            {
                //kiểm tra email đã tồn tại chưa còn nếu chưa sửa dụng thì add cái user vào bảng
                var check = db.Accounts.FirstOrDefault(s => s.email == account.Email); // kiểm tra trong cái textbox có trùng với trong bảng không
                if (check == null)
                {
                    AccountDetail accountDetail = new AccountDetail();
                    db.AccountDetails.Add(accountDetail);


                    Account acc = new Account()
                    {
                        displayName = account.DisplayName,
                        email = account.Email,
                        idAccountDetail = accountDetail.id,
                        password = account.Password,
                        position = 2,
                        username = account.Username,
                    };
                    db.Accounts.Add(acc);// thêm vào bảng
                    db.SaveChanges();// lưu thay đổi
                    return RedirectToAction("Login"); // nếu mà tạo thành công thì điều hướng sang trang khác
                }
                else
                {
                    // nếu mà đã sử dụng r thì hiện thông báo email đã tồn tại
                    ViewBag.error = "Email already exists!Use another email please!";
                    return View(account);
                }
            }
            return View(account);
        }
    }
}