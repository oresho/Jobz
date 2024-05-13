using System.ComponentModel.DataAnnotations;

namespace JobzApi.Models.Dtos.Requests
{
    public class LoginUserRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}