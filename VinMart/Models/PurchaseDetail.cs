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
    
    public partial class PurchaseDetail
    {
        public int id { get; set; }
        public Nullable<int> idPurchase { get; set; }
        public double price { get; set; }
        public Nullable<int> idProduct { get; set; }
        public int count { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Purchase Purchase { get; set; }
    }
}
