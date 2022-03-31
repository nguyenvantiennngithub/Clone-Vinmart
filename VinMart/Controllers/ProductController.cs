using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using VinMart.Models.DAO;
using VinMart.Models.DTO;
using VinMart.Models.ViewModel;
using VinMart.utils;

namespace VinMart.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            UserSession userSession = Session[Constants.Instance.UserSession] as UserSession;

            List<MenuCategoryDTO> menuCategory = CategoryDAO.Instance.GetMenuCategory();
            List<ProductGroupByBigCategoryDTO> productCategory = ProductDAO.Instance.GetFirstPageAllBigCategory();
            List<CartDetailDTO> cartDetails = new List<CartDetailDTO>() { };//use id of prodcut
            List<CartDetailProductDTO> cartDetailProducts = new List<CartDetailProductDTO>() { };//has product detail
            if (userSession != null)
            {
                cartDetails = CartDAO.Instance.GetAllCart(userSession.Id);
                cartDetailProducts = CartDAO.Instance.GetAllCardDetailProduct(userSession.Id);
            }

            ProductIndex productIndex = new ProductIndex() { 
                MenuCategory = menuCategory, 
                ProductCategory = productCategory,
                CartDetails = cartDetails,
                CartDetailProducts = cartDetailProducts,
            };
            return View(productIndex);
        }
        
        public ActionResult Detail(int id)
        {
            UserSession userSession = Session[Constants.Instance.UserSession] as UserSession;
            ProductDetailVM productDetail = ProductDAO.Instance.GetProductDetail(id);
            if (productDetail == null)
            {
                return Redirect("/Product/Index");
            }
            List<CartDetailProductDTO> cartDetailProducts = new List<CartDetailProductDTO>() { };//has product detail
            if (userSession != null)
            {
                cartDetailProducts = CartDAO.Instance.GetAllCardDetailProduct(userSession.Id);
            }

            productDetail.CartDetailProducts = cartDetailProducts;
            return View(productDetail);
        }


        public ActionResult GetProductByBigCateogryAndPage(int bigCategory, int page)
        {
            var result = ProductDAO.Instance.GetProductBigCategoryByPage(bigCategory, page);
            return Json(new
            {
                data = result[0],
            }, JsonRequestBehavior.AllowGet);
        }
    }
}