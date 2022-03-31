using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VinMart.Models.DTO
{
    public class MenuCategoryItemDTO
    {
        private string bigDisplayName;
        private string bigMediaURL;
        private int idBigCategory;
        private int cateId;
        private string cateDisplayName;

        public string BigDisplayName { get => bigDisplayName; set => bigDisplayName = value; }
        public string BigMediaURL { get => bigMediaURL; set => bigMediaURL = value; }
        public string CateDisplayName { get => cateDisplayName; set => cateDisplayName = value; }
        public int IdBigCategory { get => idBigCategory; set => idBigCategory = value; }
        public int CateId { get => cateId; set => cateId = value; }
    }
}