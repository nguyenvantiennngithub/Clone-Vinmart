using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VinMart.Models.DTO
{
    public class BigCategoryDTO
    {
        private int id;
        private string displayName;
        private string mediaURL;

        public BigCategoryDTO(BigCategory bigCategory)
        {
            this.id = bigCategory.id;
            this.displayName = bigCategory.displayName;
            this.mediaURL = bigCategory.mediaURL;
        }

        public BigCategoryDTO()
        {
        }

        public int Id { get => id; set => id = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public string MediaURL { get => mediaURL; set => mediaURL = value; }
    }
}