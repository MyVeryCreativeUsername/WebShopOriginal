using System.ComponentModel.DataAnnotations;
using WebShop.Models.UserEntities;

namespace WebShop.DTOs.UserDTOs
{
    public class RegistrationDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public Address Address { get; set; }



    }
}
