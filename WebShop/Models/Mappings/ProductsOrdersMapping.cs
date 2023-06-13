using WebShop.Models.ShopEntities;
using WebShop.Models.UserEntities;

namespace WebShop.Models.Mappings
{
    public class ProductsOrdersMapping
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }

        public Order Order { get; set; }
        public int OrderId { get; set; }



    }
}
