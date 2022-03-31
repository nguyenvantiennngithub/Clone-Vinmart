using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VinMart.Models.DTO
{
    public class CartDetailDTO
    {
        private int idProduct;
        private int idUser;
        private int count;

        public int IdProduct { get => idProduct; set => idProduct = value; }
        public int IdUser { get => idUser; set => idUser = value; }
        public int Count { get => count; set => count = value; }
    }
}