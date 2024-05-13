namespace JobzApi.ExceptionHandler.CustomExceptions
{
    [Serializable]
    public class InternalServerException : Exception
    {
        public InternalServerException()
        {

        }
        public InternalServerException(string? message)
            : base(message)
        {
        }
    }
}
