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
    
    public partial class AccountDetail
    {
        public int id { get; set; }
        public Nullable<System.DateTime> birthDay { get; set; }
        public string phoneNumber { get; set; }
        public string gender { get; set; }
        public string province { get; set; }
        public string district { get; set; }
        public string commune { get; set; }
        public string apartmentNumber { get; set; }
        public string detail { get; set; }
    
        public virtual Account Account { get; set; }
    }
}