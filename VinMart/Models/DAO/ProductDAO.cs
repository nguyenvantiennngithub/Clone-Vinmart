using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VinMart.Models.DTO;
using VinMart.Models.ViewModel;

namespace VinMart.Models.DAO
{
    public class ProductDAO
    {
        private static ProductDAO instance;

        internal static ProductDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductDAO();
                }
                return instance;
            }
            set => instance = value;
        }
        //get first page of all big category
        public List<ProductGroupByBigCategoryDTO> GetFirstPageAllBigCategory()
        {
            using (VinMartEntities db = new VinMartEntities()) {
                int perPage = 8;
                int page = 1;

                return db.BigCategories
                    .Join(db.Categories,
                        bigCate => bigCate.id,
                        cate => cate.idBigCategory,
                        (bigCategory, cate) => new
                        {
                            bigCategory = new BigCategoryDTO() {
                                Id = bigCategory.id,
                                DisplayName = bigCategory.displayName,
                                MediaURL = bigCategory.mediaURL,
                            },
                            idCategory = cate.id,
                        }
                    ).Join(db.Products,
                        cate => cate.idCategory,
                        product => product.idCategory,
                        (cate, product) => new 
                        {
                            BigCategory = cate.bigCategory,
                            Product = new ProductDTO() {
                                MediaURL = product.mediaURL,
                                DisplayName = product.displayName,
                                Id = product.id,
                                IdCategory = (int)product.idCategory,
                                Price = product.price,
                                SalePrice = product.salePrice,
                                Status = (int)product.status,

                            },
                        }
                    )
                    .ToList().GroupBy(cate =>  new { cate.BigCategory.DisplayName, cate.BigCategory.Id })
                    .Select(cate => new ProductGroupByBigCategoryDTO() {
                        DisplayName = cate.Key.DisplayName,
                        Id = cate.Key.Id,
                        Products = cate.Select(temp => temp.Product).ToList().OrderBy(product => product.DisplayName).Skip((page - 1 )*perPage).Take(perPage).ToList(),
                        Count = cate.Count(),
                    }).ToList();
            }
        }
        //get a speficel page of a big category
        public List<ProductGroupByBigCategoryDTO> GetProductBigCategoryByPage(int idBigCategory, int page)
        {
            using (VinMartEntities db = new VinMartEntities())
            {

                int perPage = 8;
                return db.BigCategories.Where(bigCategory => bigCategory.id == idBigCategory)
                     .Join(db.Categories,
                         bigCategory => bigCategory.id,
                         category => category.idBigCategory,
                         (bigCategory, category) => new
                         {
                             BigCategory = new BigCategoryDTO()
                             {
                                 Id = bigCategory.id,
                                 DisplayName = bigCategory.displayName,
                                 MediaURL = bigCategory.mediaURL,
                             },
                             IdCategory = category.id,
                         })
                     .Join(db.Products,
                         bigCategory => bigCategory.IdCategory,
                         product => product.idCategory,
                         (bigCategory, product) => new
                         {
                             BigCategory = bigCategory.BigCategory,
                             Product = new ProductDTO()
                             {
                                 MediaURL = product.mediaURL,
                                 DisplayName = product.displayName,
                                 Id = product.id,
                                 IdCategory = (int)product.idCategory,
                                 Price = product.price,
                                 SalePrice = product.salePrice,
                                 Status = (int)product.status,
                             }
                         })
                     .ToList().GroupBy(bigCategory => new { bigCategory.BigCategory.Id, bigCategory.BigCategory.DisplayName })
                     .Select(temp => new ProductGroupByBigCategoryDTO()
                     {
                         Id = temp.Key.Id,
                         DisplayName = temp.Key.DisplayName,
                         Count = temp.Count(),
                         Products = temp.Select(b => b.Product).ToList().OrderBy(product => product.DisplayName).Skip((page - 1) * perPage).Take(perPage).ToList(),
                     }).ToList();
            }
        }

        public ProductDetailVM GetProductDetail(int idProduct)
        {
            using (VinMartEntities db = new VinMartEntities())
            {
                return db.Products.Where(product => product.id == idProduct)
                    .Join(db.Categories,
                    product => product.idCategory,
                    category => category.id,
                    (product, category) => new
                    {
                        product = product,
                        category = category
                    })
                    .Join(db.BigCategories,
                    temp => temp.category.idBigCategory,
                    big => big.id,
                    (temp, big) => new
                    {
                        product = temp.product,
                        category = temp.category,
                        bigCategory = big,
                    })
                    .Join(db.ProductStatus,
                    temp => temp.product.status,
                    status => status.id,
                    (temp, status) => new
                    {
                        product = temp.product,
                        category = temp.category,
                        bigCategory = temp.bigCategory,
                        status = status,
                    })
                    .Join(db.ProductDetails,
                        temp => temp.product.idProductDetail,
                        detail => detail.id,
                        (temp, detail) => new ProductDetailVM
                        {
                            Product = new ProductDTO() {
                                DisplayName = temp.product.displayName,
                                Id = temp.product.id,
                                IdCategory = (int)temp.product.idCategory,
                                MediaURL = temp.product.mediaURL,
                                Price = temp.product.price,
                                SalePrice = temp.product.salePrice,
                                Status = (int)temp.product.status,
                            },
                            BigCategory = new BigCategoryDTO() {
                                DisplayName = temp.bigCategory.displayName,
                                Id = temp.bigCategory.id,
                                MediaURL = temp.bigCategory.mediaURL,   
                            },
                            Category = new CategoryDTO() {
                                Id = temp.category.id,
                                DisplayName = temp.category.displayName,
                                IdBigCategory = (int)temp.category.idBigCategory,
                            },
                            Status = new StatusDTO() {
                                DisplayName = temp.status.displayName,
                                Id = temp.status.id,
                            },
                            ProductDetail = new ProductDetailDTO() {
                                Id = detail.id,
                                Ingredients = detail.ingredients,
                                Manuals = detail.manuals,
                                Origin = detail.origin,
                                Preserve = detail.preserve,
                            }
                        }
                    ).FirstOrDefault();

                    
            }
        }
        public CartDetailProductDTO GetProduct(int id, int idUser)
        {
            using (VinMartEntities db = new VinMartEntities())
            {
                

                Product product = db.Products.Where(pro => pro.id == id).FirstOrDefault();
                ProductDTO productDTO = new ProductDTO(product);
                return new CartDetailProductDTO()
                {
                    Count = 1,
                    Product = productDTO,
                    IdUser = idUser,
                };
            }
        }
    }
}