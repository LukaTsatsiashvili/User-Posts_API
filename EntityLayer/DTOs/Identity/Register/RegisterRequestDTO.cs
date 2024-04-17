using System.ComponentModel.DataAnnotations;

namespace EntityLayer.DTOs.Identity.Register
{
    public class RegisterRequestDTO
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
