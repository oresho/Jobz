namespace JobzApi.Models.Dtos.Responses
{
    public class JobResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Type { get; set; }
        public string Description { get; set; } 
        public string Location { get; set; }
        public string Salary { get; set; }
        public CompanyResponse Company { get; set; }
    }
}
