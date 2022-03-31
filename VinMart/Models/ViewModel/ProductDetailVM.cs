using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VinMart.Models.DTO;

namespace VinMart.Models.ViewModel
{
    public class ProductDetailVM
    {
        private ProductDTO product;
        private BigCategoryDTO bigCategory;
        private CategoryDTO category;
        private StatusDTO status;
        private ProductDetailDTO productDetail;
        private List<CartDetailProductDTO> cartDetailProducts;

        public ProductDTO Product { get => product; set => product = value; }
        public BigCategoryDTO BigCategory { get => bigCategory; set => bigCategory = value; }
        public CategoryDTO Category { get => category; set => category = value; }
        public StatusDTO Status { get => status; set => status = value; }
        public ProductDetailDTO ProductDetail { get => productDetail; set => productDetail = value; }
        public List<CartDetailProductDTO> CartDetailProducts { get => cartDetailProducts; set => cartDetailProducts = value; }
    }
}