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
    
    public partial class AddressUser
    {
        public int id { get; set; }
        public Nullable<int> idUser { get; set; }
        public Nullable<int> idAddress { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual Address Address { get; set; }
    }
}