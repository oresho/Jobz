using System.ComponentModel.DataAnnotations;

namespace JobzApi.Models.Dtos.Requests
{
    public class RegisterUserRequest
    {
        [Required(ErrorMessage = "Firstname is required")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [Phone]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Role is required")]
        public string? Role { get; set; } = "JobFinder";
    }
}
