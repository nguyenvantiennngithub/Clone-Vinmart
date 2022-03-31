using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VinMart.Models.DTO
{
    public class ProductGroupByBigCategoryItemDTO
    {
        private BigCategoryDTO bigCategory;
        private ProductDTO product;

        public BigCategoryDTO BigCategory { get => bigCategory; set => bigCategory = value; }
        public ProductDTO Product { get => product; set => product = value; }
    }
}