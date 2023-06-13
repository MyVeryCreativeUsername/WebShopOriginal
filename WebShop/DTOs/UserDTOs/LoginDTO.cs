using System.ComponentModel.DataAnnotations;

namespace WebShop.DTOs.UserDTOs
{
    public class LoginDTO
    {
        public string Email { get; set; }
        [Required, MinLength(6)]
        public string Password { get; set; }

    }
}
