namespace Infrastructure.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message = null, Exception innerException = null)
            : base(message, innerException)
        {

        }
    }
}
