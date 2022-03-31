using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VinMart.Models.DTO;

namespace VinMart.Models.DAO
{
    public class CartDAO
    {
        private static CartDAO instance;
        internal static CartDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CartDAO();
                }
                return instance;
            }
            set => instance = value;
        }

        public void CheckAndAddToCart(int idProduct, int idUser, int value)
        {
            using (VinMartEntities db = new VinMartEntities())
            {
                CartDetail cartDetail = db.CartDetails.Where(cart => cart.idProduct == idProduct && cart.idUser == idUser).FirstOrDefault();
                if (cartDetail == null)
                {
                    CartDetail cart = new CartDetail()
                    {
                        count = value,
                        idProduct = idProduct,
                        idUser = idUser,
                    };
                    db.CartDetails.Add(cart);
                }
                else
                {
                    cartDetail.count = value;
                }
                db.SaveChanges();
            }

        }
        public CartDetail CheckExistProductInCart(int idProduct, int idUser)
        {
            using (VinMartEntities db = new VinMartEntities())
            {
                return db.CartDetails.Where(cart => cart.idProduct == idProduct && cart.idUser == idUser).FirstOrDefault();
            }
        }
        public CartDetailProductDTO AddCart(int idProduct, int idUser)
        {
            using (VinMartEntities db = new VinMartEntities())
            {
                var cartDetail = db.CartDetails.FirstOrDefault(cart => cart.idProduct == idProduct && cart.idUser == idUser);
                if (cartDetail != null) return null;
                CartDetail cartDetailAdd = new CartDetail()
                {
                    count = 1,
                    idProduct = idProduct,
                    idUser = idUser,
                };
                db.CartDetails.Add(cartDetailAdd);
                db.SaveChanges();
                Product product = db.Products.Where(pro => pro.id == idProduct).FirstOrDefault();
                ProductDTO productDTO = new ProductDTO(product);
                return new CartDetailProductDTO()
                {
                    Count = 1,
                    Product = productDTO,
                    IdUser = idUser,
                };
            }
        }

        public void UpdateCart(int idProduct, int idUser, int value) {
            using (VinMartEntities db = new VinMartEntities())
            {
                CartDetail cartDetail = db.CartDetails.Where(cart => cart.idProduct == idProduct && cart.idUser == idUser).FirstOrDefault();
                if (cartDetail == null) return;
                cartDetail.count = value;
                db.SaveChanges();
            }
        }
        public bool DeleteCart(int idProduct, int idUser)
        {
            using (VinMartEntities db = new VinMartEntities())
            {
                try
                {
                    var cartDelete = db.CartDetails.Where(cart => cart.idProduct == idProduct && cart.idUser == idUser).FirstOrDefault();
                    db.CartDetails.Remove(cartDelete);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public List<CartDetailDTO> GetAllCart(int idUser)
        {
            using (VinMartEntities db = new VinMartEntities())
            {
                try
                {
                    return db.CartDetails.Where(cart => cart.idUser == idUser)
                        .Select(cart => new CartDetailDTO() {
                            Count = cart.count,
                            IdProduct = cart.idProduct,
                            IdUser = cart.idUser,
                        })
                        .ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public List<CartDetailProductDTO> GetAllCardDetailProduct(int idUser)
        {
            using (VinMartEntities db = new VinMartEntities())
            {
                try
                {
                    return db.CartDetails.Where(cart => cart.idUser == idUser)
                        .Join(db.Products,
                            cart => cart.idProduct,
                            product => product.id,
                            (cart, product) => new CartDetailProductDTO()
                            {
                                Count = cart.count,
                                IdUser = cart.idUser,
                                Product = new ProductDTO()
                                {
                                    DisplayName = product.displayName,
                                    Id = product.id,
                                    MediaURL = product.mediaURL,
                                    Price = product.price,
                                    SalePrice = product.salePrice,
                                    Status = (int)product.status,
                                    IdCategory = (int)product.idCategory,
                                }
                            }).ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public bool DeleteAll(int idUser)
        {
            using (VinMartEntities db = new VinMartEntities())
            {
                try
                {
                    List<CartDetail> cartDetails = db.CartDetails.Where(cart => cart.idUser == idUser).ToList();
                    foreach (var item in cartDetails)
                    {
                        db.CartDetails.Remove(item);
                    }
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool AddUpdate(int idProduct, int idUser, int value)
        {
            bool isAdd;
            using (VinMartEntities db = new VinMartEntities())
            {
                CartDetail cartDetails = db.CartDetails.Where(cart => cart.idUser == idUser && cart.idProduct == idProduct ).FirstOrDefault();
                if (cartDetails == null)
                {
                    CartDetail cartDetail = new CartDetail()
                    {
                        count = value,
                        idProduct = idProduct,
                        idUser = idUser,
                    };
                    isAdd = true;
                    db.CartDetails.Add(cartDetail);
                }
                else
                {
                    isAdd = false;
                    cartDetails.count += value;
                }
                db.SaveChanges();
                return isAdd;
            }
        }

        public void Payment(int idUser)
        {
            using (VinMartEntities db = new VinMartEntities())
            {
                Account account = db.Accounts.FirstOrDefault(a => a.id == idUser);
                AccountDetail accountDetail = db.AccountDetails.FirstOrDefault(a => a.id == account.idAccountDetail);

                Address deliveryAddress = new Address() {
                    apartmentNumber = accountDetail.apartmentNumber,
                    commune = accountDetail.Commune,
                    name = account.displayName,
                    email = account.email,
                    district = accountDetail.district,
                    phoneNumber = accountDetail.phoneNumber,
                    province = accountDetail.province,
                };
                db.Addresses.Add(deliveryAddress);

                Purchase purchase = new Purchase()
                {
                    createAt = DateTime.Now,
                    idUser = idUser,
                    idAddress = deliveryAddress.id,
                };
                db.Purchases.Add(purchase);

                List<CartDetail> cartDetails = db.CartDetails.Where(cart => cart.idUser == idUser).ToList();
                foreach (CartDetail cartDetail in cartDetails)
                {
                    Product product = db.Products.FirstOrDefault(product1 => product1.id == cartDetail.idProduct);
                    PurchaseDetail purchaseDetail = new PurchaseDetail()
                    {
                        count = cartDetail.count,
                        idPurchase = purchase.id,
                        price = product.price,
                        idProduct = product.id,
                    };
                    db.PurchaseDetails.Add(purchaseDetail);
                    db.CartDetails.Remove(cartDetail);
                }
                db.SaveChanges();
            }
        }
    }
}