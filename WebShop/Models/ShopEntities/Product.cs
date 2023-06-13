using System.Collections.Generic;
using WebShop.Models.Mappings;
using WebShop.Models.UserEntities;

namespace WebShop.Models.ShopEntities
{
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Qty { get; set; }


        public Discount ProductDiscount { get; set; }

        public List<Review> ProductReviews { get; set; } = new List<Review>();

        public List<Category> Categorys { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();

        public List<ProductsCategoriesMapping> ProductCategoriesMapping { get; set; } = new List<ProductsCategoriesMapping>();

        public List<ProductsOrdersMapping> ProductsOrdersMapping { get; set; } = new List<ProductsOrdersMapping>();



    }






}
