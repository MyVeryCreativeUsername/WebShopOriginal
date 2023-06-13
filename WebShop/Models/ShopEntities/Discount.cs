namespace WebShop.Models.ShopEntities
{
    public class Discount
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Description { get; set; }
        public float NewPrice { get; set; }

    }
}
