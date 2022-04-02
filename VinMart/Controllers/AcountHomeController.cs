using VinMart.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VinMart.Models.DTO;
using VinMart.Models.ViewModel;
using VinMart.utils;
using VinMart.Models.DAO;

namespace DoAnWeb3.Controllers
{
    public class AcountHomeController : Controller
    {
         VinMartEntities db = new VinMartEntities();
        
        public ActionResult Home()
        {
           
            return View();
        }

        public ActionResult Acount()
        {

            //Session[UserInfo.UserSession] = data.Accounts.Single(p => p.id == 1);
            //var acount = (Account)Session[UserInfo.UserSession];
            //System.Diagnostics.Debug.WriteLine(acount.id);
            ///var acountDetail = data.AccountDetails.Single(p => p.id == acount.id);
            //AcountFullDetail a = new AcountFullDetail();
            
            UserSession userSession = Session[Constants.Instance.UserSession] as UserSession;
            if (userSession == null)
            {
                return Redirect("/Product/Index");
            }

            var acountDetail = db.AccountDetails.FirstOrDefault(p => p.id == userSession.Id);
            AcountFullDetail a = new AcountFullDetail();
            List<CartDetailProductDTO> cartDetailProducts = new List<CartDetailProductDTO>() { };//has product detail
            cartDetailProducts = CartDAO.Instance.GetAllCardDetailProduct(userSession.Id);

            a.Id = userSession.Id;
            a.DisplayName = userSession.DisplayName;
            a.Email = userSession.Email;
            a.BirthDay = acountDetail.birthDay;
            a.PhoneNumber = acountDetail.phoneNumber;
            a.Gender = acountDetail.gender;
            a.Province = acountDetail.province;
            a.District = acountDetail.district;
            a.Commune = acountDetail.Commune;
            a.Detail = acountDetail.detail;
            a.ApartmentNumber = acountDetail.apartmentNumber;

            AddAccountVM addAccountVM = new AddAccountVM(a, cartDetailProducts);

            return View(addAccountVM);
        }

        [HttpPost, ActionName("Acount")]
        public ActionResult Acount(FormCollection collection)
        {
            //var acount = (Account)Session[UserInfo.UserSession];
            UserSession userSession = Session[Constants.Instance.UserSession] as UserSession;
            if (userSession == null)
            {
                return Redirect("/Product/Index");
            }
            AccountDetail acountDetail = db.AccountDetails.FirstOrDefault(p => p.id == userSession.Id);
            if (acountDetail == null)
            {
                return HttpNotFound();
            }
            userSession.DisplayName = collection["AcountFullDetail.DisplayName"];
            acountDetail.phoneNumber = collection["AcountFullDetail.PhoneNumber"];
            userSession.Email = collection["AcountFullDetail.Email"];
            acountDetail.gender = collection["AcountFullDetail.Gender"];
            //acountDetail.birthDay = DateTime.Parse(String.Format("{mm/dd/yyyy}", collection["AcountFullDetail.BirthDay"]));
            acountDetail.province = collection["AcountFullDetail.Province"];
            acountDetail.district = collection["AcountFullDetail.District"];
            acountDetail.Commune = collection["AcountFullDetail.Commune"];
            acountDetail.apartmentNumber = collection["AcountFullDetail.ApartmentNumber"];

            AcountFullDetail a = new AcountFullDetail();
            a.Id = userSession.Id;
            a.DisplayName = userSession.DisplayName;
            a.Email = userSession.Email;
            a.BirthDay = acountDetail.birthDay;
            a.PhoneNumber = acountDetail.phoneNumber;
            a.Gender = acountDetail.gender;
            a.Province = acountDetail.province;
            a.District = acountDetail.district;
            a.Commune = acountDetail.Commune;
            a.Detail = acountDetail.detail;
            a.ApartmentNumber = acountDetail.apartmentNumber;
            db.SaveChanges();

            List<CartDetailProductDTO> cartDetailProducts = new List<CartDetailProductDTO>() { };//has product detail
            cartDetailProducts = CartDAO.Instance.GetAllCardDetailProduct(userSession.Id);
            AddAccountVM addAccountVM = new AddAccountVM(a, cartDetailProducts);
            return View(addAccountVM);
        }

        [HttpGet]
        public ActionResult Address()
        {
            UserSession userSession = Session[Constants.Instance.UserSession] as UserSession;
            if (userSession == null)
            {
                return Redirect("/Product/Index");
            }
            //List<Address> list = db.Addresses.Where(p => p.id == 1).ToList();
            List<Address> list = db.AddressUsers.Where(a => a.idUser == userSession.Id)
                .Join(db.Addresses,
                a => a.idAddress,
                b => b.id,
                (a, b) => b).ToList();
            List<CartDetailProductDTO> cartDetailProducts = new List<CartDetailProductDTO>() { };//has product detail
            cartDetailProducts = CartDAO.Instance.GetAllCardDetailProduct(userSession.Id);
            AccountListAddressVM accountListAddress = new AccountListAddressVM(cartDetailProducts, list);
            
            return View(accountListAddress);
        }

        [HttpGet]
        public ActionResult AddressAdd()
        {
            UserSession userSession = Session[Constants.Instance.UserSession] as UserSession;
            if (userSession == null)
            {
                return Redirect("/Product/Index");
            }
            List<CartDetailProductDTO> cartDetailProducts = new List<CartDetailProductDTO>() { };//has product detail
            cartDetailProducts = CartDAO.Instance.GetAllCardDetailProduct(userSession.Id);
            Address address = new Address();
            AddAddressVM accountListAddress = new AddAddressVM(cartDetailProducts, address);
            return View(accountListAddress);
        }

        [HttpPost, ActionName("AddressAdd")]
        public ActionResult AddressAdd(FormCollection collection)
        {
            UserSession userSession = Session[Constants.Instance.UserSession] as UserSession;
            if (userSession == null)
            {
                return Redirect("/Product/Index");
            }
            //var acount = (Account)Session[UserInfo.UserSession];
            Address a = new Address();
            a.id = userSession.Id;
            a.name = collection["Address.name"];
            a.phoneNumber = collection["Address.phoneNumber"];
            a.email = collection["Address.email"];
            a.province = collection["Address.province"];
            a.district = collection["Address.district"];
            a.commune = collection["Address.commune"];
            a.apartmentNumber = collection["Address.apartmentNumber"];
            db.Addresses.Add(a);

            AddressUser addressUser = new AddressUser() {
                idAddress = a.id,
                idUser = userSession.Id,
            };
            db.AddressUsers.Add(addressUser);
            db.SaveChanges();
            return RedirectToAction("Address");
        }


        public ActionResult LogOut()
        {
            Session[Constants.Instance.UserSession] = null;
            return Redirect("/Product/Index");
        }

        public ActionResult TransactionHistory()
        {
            //var acount = (Account)Session[UserInfo.UserSession];
            UserSession userSession = Session[Constants.Instance.UserSession] as UserSession;
            if (userSession == null)
            {
                return Redirect("/Product/Index");
            }

            List<Purchase> listPurchase = db.Purchases.Where(p => p.idUser == userSession.Id).OrderByDescending(p => p.createAt).ToList();
            List<PurchaseFullDetail> list = new List<PurchaseFullDetail>();

            List<CartDetailProductDTO> cartDetailProducts = new List<CartDetailProductDTO>() { };//has product detail
            cartDetailProducts = CartDAO.Instance.GetAllCardDetailProduct(userSession.Id);

            foreach (var item in listPurchase)
            {
                List<PurchaseDetail> listDetail = db.PurchaseDetails.Where(p => p.idPurchase == item.id).ToList();
                foreach (var item2 in listDetail)
                {
                    PurchaseFullDetail a = new PurchaseFullDetail();
                    a.Id = item.id;
                    a.IdUser = userSession.Id;
                    a.IdDetail = item2.id;
                    a.CreateAt = item.createAt;
                    a.IdProduct = item2.idProduct;
                    a.Count = item2.count;
                    a.Price = item2.price;
                    a.IdAddress = item.idAddress;
                    Address address = db.Addresses.Where(p => p.id == item.idAddress).FirstOrDefault();

                    a.Province = address.province;
                    a.District = address.district;
                    a.Commune = address.commune;
                    a.ApartmentNumber = address.apartmentNumber;
                    a.DisplayName = db.Products.Where(p => p.id == item2.id).Select(p => p.displayName).FirstOrDefault();

                    list.Add(a);
                }
            }
            HistoryTransactionVM historyTransaction = new HistoryTransactionVM(cartDetailProducts, list);
            return View(historyTransaction);
        }
    }
}