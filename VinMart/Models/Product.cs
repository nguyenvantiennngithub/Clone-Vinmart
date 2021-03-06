//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VinMart.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public Product()
        {
            this.CartDetails = new HashSet<CartDetail>();
            this.PurchaseDetails = new HashSet<PurchaseDetail>();
        }
    
        public int id { get; set; }
        public Nullable<int> idCategory { get; set; }
        public string displayName { get; set; }
        public string mediaURL { get; set; }
        public double price { get; set; }
        public double salePrice { get; set; }
        public string unit { get; set; }
        public Nullable<int> idProductDetail { get; set; }
        public Nullable<int> status { get; set; }
    
        public virtual ICollection<CartDetail> CartDetails { get; set; }
        public virtual Category Category { get; set; }
        public virtual ProductDetail ProductDetail { get; set; }
        public virtual ProductStatu ProductStatu { get; set; }
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }
    }
}
