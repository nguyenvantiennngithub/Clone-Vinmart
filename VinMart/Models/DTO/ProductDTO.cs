using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VinMart.Models.DTO
{
    public class ProductDTO
    {
        private int id;
        private int idCategory;
        private string displayName;
        private string mediaURL;
        private double price;
        private double salePrice;
        private int status;

        public ProductDTO(Product product)
        {
            this.id = product.id;
            this.idCategory = (int)product.idCategory;
            this.displayName = product.displayName;
            this.mediaURL = product.mediaURL;
            this.price = product.price;
            this.salePrice = product.salePrice;
            this.status = (int)product.status;
        }
        public ProductDTO()
        {
        }
        public int Id { get => id; set => id = value; }
        public int IdCategory { get => idCategory; set => idCategory = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public string MediaURL { get => mediaURL; set => mediaURL = value; }
        public double Price { get => price; set => price = value; }
        public double SalePrice { get => salePrice; set => salePrice = value; }
        public int Status { get => status; set => status = value; }

        
    }
}