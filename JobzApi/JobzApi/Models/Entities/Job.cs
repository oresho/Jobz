using System.ComponentModel.DataAnnotations;

namespace JobzApi.Models.Entities
{
    public class Job
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Salary { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
