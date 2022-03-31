using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VinMart.Models.DTO;

namespace VinMart.Models.ViewModel
{
    public class HistoryTransactionVM
    {
        List<CartDetailProductDTO> cartDetailProducts;
        List<PurchaseFullDetail> list;
        public HistoryTransactionVM(List<CartDetailProductDTO> cartDetailProducts, List<PurchaseFullDetail> list)
        {
            this.CartDetailProducts = cartDetailProducts;
            this.List = list;
        }

        public List<CartDetailProductDTO> CartDetailProducts { get => cartDetailProducts; set => cartDetailProducts = value; }
        public List<PurchaseFullDetail> List { get => list; set => list = value; }
    }
}