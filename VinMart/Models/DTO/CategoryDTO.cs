using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VinMart.Models.DTO
{
    public class CategoryDTO
    {
        private int id;
        private int idBigCategory;
        private string displayName;

        public CategoryDTO(Category category)
        {
            this.id = category.id;
            this.idBigCategory = (int)category.idBigCategory;
            this.displayName = category.displayName;
        }
        public CategoryDTO()
        {
        }

        public int Id { get => id; set => id = value; }
        public int IdBigCategory { get => idBigCategory; set => idBigCategory = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
    }
}