using System.Text;

namespace Infrastructure.Helpers
{
    public static class ExceptionExtensions
    {
        public static string FullMessage(this Exception ex, bool ignoreFirstMessage = false)
        {
            var msg = new StringBuilder();
            var e = ex;
            int i = 0;
            while (e != null)
            {
                if (!ignoreFirstMessage || i > 0)
                    msg.Append(e.Message).Append("\r\n");
                e = e.InnerException;
                i++;
            }
            return msg.ToString();
        }
    }
}
