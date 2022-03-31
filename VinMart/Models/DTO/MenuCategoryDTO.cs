using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VinMart.Models.DTO
{
    public class MenuCategoryDTO
    {
        private string bigDisplayName;
        private string bigMediaURL;
        private List<MenuCategoryItemDTO> menuCategoryItem;
        private int idBigCategory;
        public string BigDisplayName { get => bigDisplayName; set => bigDisplayName = value; }
        public string BigMediaURL { get => bigMediaURL; set => bigMediaURL = value; }
        public List<MenuCategoryItemDTO> MenuCategoryItem { get => menuCategoryItem; set => menuCategoryItem = value; }
        public int IdBigCategory { get => idBigCategory; set => idBigCategory = value; }
    }
}