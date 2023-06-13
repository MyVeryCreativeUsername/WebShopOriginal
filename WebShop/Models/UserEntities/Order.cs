using System.Collections.Generic;
using WebShop.Models.Mappings;
using WebShop.Models.ShopEntities;

namespace WebShop.Models.UserEntities
{
    public class Order
    {
        public int Id { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();


        public List<ProductsOrdersMapping> ProductsOrdersMapping { get; set; } = new List<ProductsOrdersMapping>();



    }
}
