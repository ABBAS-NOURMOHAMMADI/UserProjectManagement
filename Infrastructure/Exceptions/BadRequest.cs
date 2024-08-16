using Infrastructure.Helpers;

namespace Infrastructure.Exceptions
{
    public class BadRequest
    {
        public string Message { get; set; }
        public string TechnicalMessage { get; set; }
        public string StackTrace { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }

        public BadRequest(string msg = null) { Message = msg ?? "خطای نامشخص رویداده است"; }

        public BadRequest(Exception ex, string msg = null)
        {
            Message = msg ?? "خطای نامشخص رویداده است";
            if (msg == null)
            {
                Message = UserReadableMessage(ex);
                if (ex is AggregateException)
                {
                    foreach (var e in (ex as AggregateException).InnerExceptions)
                    {
                        var message = UserReadableMessage(e);
                        if (message != null)
                            Message += message + "\r\n";
                    }
                }
            }
            TechnicalMessage = ex.FullMessage();
            StackTrace = ex.StackTrace;
            if (ex is ValidationException)
            {
                Errors = (ex as ValidationException).Failures;
            }
        }

        static string UserReadableMessage(Exception ex)
        {
            if (ex is NotFoundException
                   || ex is ValidationException
                   || ex is DuplicateException
                   || ex is ForbiddenException
                   || ex is InvalidOperationException)
                return ex.Message;
            return null;
        }
    }
}
