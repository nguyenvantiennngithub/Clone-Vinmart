using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VinMart.Models.DTO
{
    public class StatusDTO
    {
        private int id;
        private string displayName;
        public int Id { get => id; set => id = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
    }
}