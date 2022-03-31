using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VinMart.Models.DTO
{
    public class PurchaseItemDTO
    {
        private int idPurchase;
        private PurchaseDetail purchaseDetail;
        private Product product;
public int IdPurchase { get => idPurchase; set => idPurchase = value; }
        public PurchaseDetail PurchaseDetail { get => purchaseDetail; set => purchaseDetail = value; }
        public Product Product { get => product; set => product = value; }
    }
}