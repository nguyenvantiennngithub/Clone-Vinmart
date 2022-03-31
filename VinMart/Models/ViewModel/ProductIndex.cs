using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VinMart.Models.DTO;

namespace VinMart.Models.ViewModel
{
    public class ProductIndex
    {
        List<MenuCategoryDTO> menuCategory;
        List<ProductGroupByBigCategoryDTO> productCategory;
        List<CartDetailDTO> cartDetails;
        List<CartDetailProductDTO> cartDetailProducts;
        public List<MenuCategoryDTO> MenuCategory { get => menuCategory; set => menuCategory = value; }
        public List<ProductGroupByBigCategoryDTO> ProductCategory { get => productCategory; set => productCategory = value; }
        public List<CartDetailDTO> CartDetails { get => cartDetails; set => cartDetails = value; }
        public List<CartDetailProductDTO> CartDetailProducts { get => cartDetailProducts; set => cartDetailProducts = value; }
    }
}