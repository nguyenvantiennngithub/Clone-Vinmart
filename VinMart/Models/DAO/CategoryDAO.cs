using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VinMart.Models.DTO;

namespace VinMart.Models.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        internal static CategoryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoryDAO();
                }
                return instance;
            }
            set => instance = value;
        }
        //List<MenuCategoryDTO>
        public List<MenuCategoryDTO> GetMenuCategory()
        {
            using (VinMartEntities db = new VinMartEntities())
            {
                return (List<MenuCategoryDTO>)db.BigCategories.
                  Join(db.Categories,
                      big => big.id,
                      cate => cate.idBigCategory,
                      (big, cate) =>
                      new MenuCategoryItemDTO
                      {
                          BigDisplayName = big.displayName,
                          BigMediaURL = big.mediaURL,
                          CateDisplayName = cate.displayName
                      }
                  ).ToList().GroupBy(cate => new { cate.BigDisplayName, cate.IdBigCategory, cate.BigMediaURL })
                 .Select(cate => new MenuCategoryDTO
                 {
                     IdBigCategory = cate.Key.IdBigCategory,
                     BigDisplayName = cate.Key.BigDisplayName,
                     BigMediaURL = cate.Key.BigMediaURL,
                     MenuCategoryItem = cate.ToList(),
                 }).ToList();
            }
        }
    }
}