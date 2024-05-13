using System.ComponentModel.DataAnnotations;

namespace JobzApi.Models.Dtos.Requests
{
    public class CreateJobRequest
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(64)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Type is required")]
        [MaxLength(32)]
        public string Type { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [MaxLength(1024)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Location is required")]
        [MaxLength(128)]
        public string Location { get; set; }
        [Required(ErrorMessage = "Salary is required")]
        public string Salary { get; set; }
        [Required(ErrorMessage = "CompanyId is required")]
        public int CompanyId { get; set; }
    }
}