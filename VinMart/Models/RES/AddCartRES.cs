using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VinMart.Models.RES
{
    public class AddCartRES
    {
        private int idProduct;
        private int value;

        public int IdProduct { get => idProduct; set => idProduct = value; }
        public int Value { get => value; set => this.value = value; }
    }
}