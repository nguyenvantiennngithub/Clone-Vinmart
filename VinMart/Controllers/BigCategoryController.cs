using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VinMart.Models;
using System.Data;
using System.Data.Entity;
using System.IO;
using VinMart.Models.RES;
using VinMart.utils;

namespace VinMart.Controllers
{
    public class BigCategoryController : Controller
    {
        VinMartEntities db = new VinMartEntities();//kêt nối cơ sở dữ liệu
        // GET: BigCatagory
        public ActionResult Index()
        {
            UserSession userSession = Session[Constants.Instance.UserSession] as UserSession;
            if (userSession == null) return Redirect("/Auth/Login");
            else if (userSession.Position != 1) return HttpNotFound();

            List<BigCategory> ds = db.BigCategories.ToList();// tạo danh sách
            return View(ds);
        }
        public ActionResult Create()
        {
            UserSession userSession = Session[Constants.Instance.UserSession] as UserSession;
            if (userSession == null) return Redirect("/Auth/Login");
            else if (userSession.Position != 1) return HttpNotFound();
            return View();
        }
        //[HttpPost]
        //public ActionResult Create(BigCategory bc)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //kiểm tra email đã tồn tại chưa còn nếu chưa sửa dụng thì add cái user vào bảng
        //        var check = db.BigCategories.FirstOrDefault(s => s.displayName == bc.displayName); // kiểm tra trong cái textbox có trùng với trong bảng không
        //        if (check == null)
        //        {
        //            db.Configuration.ValidateOnSaveEnabled = false; // thiết lập chế độ tự động kiểm tra giá trị lưu
        //            db.BigCategories.Add(bc);// thêm vào bảng
        //            db.SaveChanges();// lưu thay đổi
        //            return RedirectToAction("Index"); // nếu mà tạo thành công thì điều hướng sang trang khác
        //        }
        //        else
        //        {
        //            //nếu mà đã sử dụng r thì hiện thông báo email đã tồn tại
        //            ViewBag.error = "Email already exists!Use another email please!";
        //            return View();
        //        }
        //    }
        //    return View();
        //}

        [HttpPost]
        public ActionResult Create(BigCategoryRES bigCategory)
        {
            UserSession userSession = Session[Constants.Instance.UserSession] as UserSession;
            if (userSession == null) return Redirect("/Auth/Login");
            else if (userSession.Position != 1) return HttpNotFound();
            if (bigCategory.DisplayName != null && bigCategory.MediaURL != null)
            {
                var uploadDir = "/public/images/BigCategory";
                var imagePath = Path.Combine(Server.MapPath(uploadDir), bigCategory.MediaURL.FileName);
                var imageUrl = Path.Combine(uploadDir, bigCategory.MediaURL.FileName);
                bigCategory.MediaURL.SaveAs(imagePath);

                BigCategory bigCategory1 = new BigCategory()
                {
                    mediaURL = imageUrl,
                    displayName = bigCategory.DisplayName,
                };


                db.BigCategories.Add(bigCategory1);// thêm vào bảng
                db.SaveChanges();// lưu thay đổi
                return Redirect("/BigCategory/Index"); // nếu mà tạo thành công thì điều hướng sang trang khác
            }
            else
            {
                return View();
            }
        }
        //public string ProcessUpload(HttpPostedFileBase file)
        //{
        //    if (file == null)
        //    {
        //        return "";
        //    }

        //    file.SaveAs(Server.MapPath("~/Content/img/" + file.FileName));
        //    return "/Content/img/" + file.FileName;
        //}
        public ActionResult Delete(int id)
        {
            UserSession userSession = Session[Constants.Instance.UserSession] as UserSession;
            if (userSession == null) return Redirect("/Auth/Login");
            else if (userSession.Position != 1) return HttpNotFound();
            BigCategory bc = db.BigCategories.Find(id);
            db.BigCategories.Remove(bc);
            db.SaveChanges();
            return RedirectToAction("Index");
        }        
        public ActionResult Edit(int id)
        {
            UserSession userSession = Session[Constants.Instance.UserSession] as UserSession;
            if (userSession == null) return Redirect("/Auth/Login");
            else if (userSession.Position != 1) return HttpNotFound();
            BigCategory bc = db.BigCategories.Find(id);
            return View(bc);
        }
        [HttpPost]
        public ActionResult Edit(BigCategoryRES bigCategory)
        {
            UserSession userSession = Session[Constants.Instance.UserSession] as UserSession;
            if (userSession == null) return Redirect("/Auth/Login");
            else if (userSession.Position != 1) return HttpNotFound();
            if (ModelState.IsValid)
            {
                BigCategory big = db.BigCategories.FirstOrDefault(b => b.id == bigCategory.Id);
                if (big == null) return HttpNotFound();
                big.displayName = bigCategory.DisplayName;

                if (bigCategory.MediaURL != null && bigCategory.MediaURL.ContentLength > 0)
                {
                    var uploadDir = "/public/images/BigCategory";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), bigCategory.MediaURL.FileName);
                    var imageUrl = Path.Combine(uploadDir, bigCategory.MediaURL.FileName);
                    bigCategory.MediaURL.SaveAs(imagePath);
                    big.mediaURL = imageUrl;
                }
            }
                
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}