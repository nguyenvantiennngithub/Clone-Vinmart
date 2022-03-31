using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VinMart.Models.DTO
{
    public class CartDetailProductDTO
    {
        private int count;
        private ProductDTO product;
        private int idUser;
        public int Count { get => count; set => count = value; }
        public ProductDTO Product { get => product; set => product = value; }
        public int IdUser { get => idUser; set => idUser = value; }

        public CartDetailProductDTO()
        {
        }
    }
}