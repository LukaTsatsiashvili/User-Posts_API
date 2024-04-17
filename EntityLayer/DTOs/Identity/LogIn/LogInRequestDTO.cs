using System.ComponentModel.DataAnnotations;

namespace EntityLayer.DTOs.Identity.LogIn
{
    public class LogInRequestDTO
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
