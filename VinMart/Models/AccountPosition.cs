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
    
    public partial class AccountPosition
    {
        public AccountPosition()
        {
            this.Accounts = new HashSet<Account>();
        }
    
        public int id { get; set; }
        public string displayName { get; set; }
    
        public virtual ICollection<Account> Accounts { get; set; }
    }
}