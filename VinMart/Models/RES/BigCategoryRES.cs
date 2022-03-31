using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VinMart.Models.RES
{
    public class BigCategoryRES
    {
        private int id;
        private string displayName;
        private HttpPostedFileBase mediaURL;
        public string DisplayName { get => displayName; set => displayName = value; }
        public HttpPostedFileBase MediaURL { get => mediaURL; set => mediaURL = value; }
        public int Id { get => id; set => id = value; }
    }
}