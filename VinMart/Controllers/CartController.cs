using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VinMart.Models;
using VinMart.Models.DAO;
using VinMart.Models.DTO;
using VinMart.Models.RES;
using VinMart.Models.ViewModel;
using VinMart.utils;

namespace VinMart.Controllers
{
    public class CartController : Controller
    {

        // GET: Cart
        public ActionResult Index()
        {
            UserSession userSession = Session[Constants.Instance.UserSession] as UserSession;

            List<CartDetailProductDTO> cartDetailProducts = new List<CartDetailProductDTO>() { };
            if (userSession != null)
            {
                cartDetailProducts = CartDAO.Instance.GetAllCardDetailProduct(userSession.Id);
            }
            CartIndex cartIndex = new CartIndex()
            {
                CartDetailProducts = cartDetailProducts,
            };
            return View(cartIndex);
        }
        [HttpPost]
        public JsonResult Add(AddCartRES res)
        {
            UserSession userSession = Session[Constants.Instance.UserSession] as UserSession;
            if (userSession == null)
            {
                return Json(new
                {
                    status = false,
                }, JsonRequestBehavior.AllowGet);
            }
            CartDetailProductDTO cartDetailProduct = CartDAO.Instance.AddCart(res.IdProduct, userSession.Id);
            bool status = cartDetailProduct != null;
            return Json(new
            {
                status = status,
                data = cartDetailProduct,
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Delete(int id)
        {
            UserSession userSession = Session[Constants.Instance.UserSession] as UserSession;
            if (userSession == null)
            {
                return Json(new
                {
                    status = false,
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                bool status = CartDAO.Instance.DeleteCart(id, userSession.Id);
                return Json(new
                {
                    status = status,
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteAll()
        {
            UserSession userSession = Session[Constants.Instance.UserSession] as UserSession;
            if (userSession == null)
            {
                return Json(new
                {
                    status = false,
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                bool status = CartDAO.Instance.DeleteAll(userSession.Id);
                return Json(new
                {
                    status = status,
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Update(AddCartRES res)
        {
            UserSession userSession = Session[Constants.Instance.UserSession] as UserSession;
            if (userSession == null)
            {
                return Json(new
                {
                    status = false,
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                CartDAO.Instance.UpdateCart(res.IdProduct, userSession.Id, res.Value);
                return Json(new
                {
                    status = true,
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AllCart()
        {
            UserSession userSession = Session[Constants.Instance.UserSession] as UserSession;
            if (userSession == null)
            {
                return Json(new
                {
                    status = false,
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                status = true,
                data = CartDAO.Instance.GetAllCart(userSession.Id),
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddUpdate(AddCartRES res)
        {
            UserSession userSession = Session[Constants.Instance.UserSession] as UserSession;
            if (userSession == null)
            {
                return Json(new
                {
                    status = false,
                }, JsonRequestBehavior.AllowGet);
            }
            bool isAdd = CartDAO.Instance.AddUpdate(res.IdProduct, userSession.Id, res.Value);
            if (isAdd == true) {
                CartDetailProductDTO product = ProductDAO.Instance.GetProduct(res.IdProduct, userSession.Id);
                return Json(new
                {
                    status = true,
                    isAdd = true,
                    data = product
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    status = true,
                    isAdd = false,
                }, JsonRequestBehavior.AllowGet);
            }
            
        }
        public ActionResult Payment()
        {
            UserSession userSession = Session[Constants.Instance.UserSession] as UserSession;
            if (userSession == null)
            {
                return Redirect("/Auth/Login");
            }

            CartDAO.Instance.Payment(userSession.Id);
            return Redirect("/Product/Index");
        }
    }
}