namespace JobzApi.ExceptionHandler.CustomExceptions
{
    [Serializable]
    public class BadRequestException : Exception
    {
        public BadRequestException()
        {

        }
        public BadRequestException(string? message)
            : base(message)
        {

        }
    }
}
