using System.Collections.Generic;
using WebShop.Models.Mappings;

namespace WebShop.Models.ShopEntities
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();

        public List<ProductsCategoriesMapping> ProductCategoriesMapping { get; set; } = new List<ProductsCategoriesMapping>();

    }
}
