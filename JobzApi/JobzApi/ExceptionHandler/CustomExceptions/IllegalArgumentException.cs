namespace JobzApi.ExceptionHandler.CustomExceptions
{
    [Serializable]
    public class IllegalArgumentException : Exception
    {
        public IllegalArgumentException()
        {

        }
        public IllegalArgumentException(string? message)
            : base(message)
        {
        }
    }
}
