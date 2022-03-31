using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VinMart.Models.DTO
{
    public class ProductDetailDTO
    {
        private int id;
        private string ingredients;
        private string preserve;
        private string origin;
        private string manuals;

        public int Id { get => id; set => id = value; }
        public string Ingredients { get => ingredients; set => ingredients = value; }
        public string Preserve { get => preserve; set => preserve = value; }
        public string Origin { get => origin; set => origin = value; }
        public string Manuals { get => manuals; set => manuals = value; }
    }
}