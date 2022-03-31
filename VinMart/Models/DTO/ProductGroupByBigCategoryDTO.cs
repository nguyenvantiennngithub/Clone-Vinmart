using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VinMart.Models.DTO
{
    public class ProductGroupByBigCategoryDTO
    {
        private string displayName;
        private int id;
        List<ProductDTO> products;
        private int count;
        public string DisplayName { get => displayName; set => displayName = value; }
        public List<ProductDTO> Products { get => products; set => products = value; }
        public int Count { get => count; set => count = value; }
        public int Id { get => id; set => id = value; }
    }
}