namespace JobzApi.Models.Dtos.Responses
{
    public class PaginatedResponse<T>
    {
        public int TotalResult { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public T Data { get; set; }
    }
}
