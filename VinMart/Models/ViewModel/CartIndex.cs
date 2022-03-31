using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VinMart.Models.DTO;

namespace VinMart.Models.ViewModel
{
    public class CartIndex
    {
        List<CartDetailProductDTO> cartDetailProducts;
        public List<CartDetailProductDTO> CartDetailProducts { get => cartDetailProducts; set => cartDetailProducts = value; }
    }
}