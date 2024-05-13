using System.ComponentModel.DataAnnotations;

namespace JobzApi.Models.Dtos.Requests
{
    public class CreateCompanyRequest
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(32)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [MaxLength(1024)]
        public string Description { get; set; }
        [Required(ErrorMessage = "ContactEmail is required")]
        [MaxLength(32)]
        [EmailAddress]
        public string ContactEmail { get; set; }
        [Required(ErrorMessage = "ContactPhone is required")]
        [MaxLength(32)]
        [Phone]
        public string ContactPhone { get; set; }
    }
}
