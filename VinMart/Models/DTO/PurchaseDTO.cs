using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VinMart.Models.DTO
{
    public class PurchaseDTO
    {
        private int idPurchase;
        private List<PurchaseItemDTO> listItem;

        public int IdPurchase { get => idPurchase; set => idPurchase = value; }
        public List<PurchaseItemDTO> ListItem { get => listItem; set => listItem = value; }
    }
}