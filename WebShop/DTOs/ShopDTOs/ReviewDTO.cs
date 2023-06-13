using WebShop.Models.ShopEntities;

namespace WebShop.DTOs.ShopDTOs
{
    public class ReviewDTO
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
