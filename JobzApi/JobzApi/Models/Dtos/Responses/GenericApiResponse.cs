namespace JobzApi.Models.Dtos.Responses
{
    public class GenericApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }

    public class GenericApiResponse<T> : GenericApiResponse
    {
        public T Data { get; set; }
    }
}
