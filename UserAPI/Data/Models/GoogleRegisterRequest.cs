using System.ComponentModel.DataAnnotations;

namespace UserAPI.Data.Models
{
    public class GoogleRegisterRequest
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MinLength(6, ErrorMessage = "Please enter at least 6 characters!")]
        public string Sub { get; set; } = string.Empty;
    }
}
