namespace Infrastructure.Helpers
{
    public static class StringExtensions
    {
        public static string RemoveFromEnd(this string s, string suffix)
        {
            if (s.EndsWith(suffix))
            {
                return s.Substring(0, s.Length - suffix.Length);
            }

            return s;
        }

        public static string RemoveFromEnd(this string s, params string[] suffixList)
        {
            foreach (var suffix in suffixList)
            {
                if (s.EndsWith(suffix))
                {
                    s = s.Substring(0, s.Length - suffix.Length);
                }
            }
            return s;
        }

        public static string? Truncate(this string? value, int totalLength, string truncationSuffix = "…")
        {
            return value?.Length > totalLength
                ? value.Substring(0, totalLength - truncationSuffix.Length) + truncationSuffix
                : value;
        }

        public static string RemoveFromStart(this string s, string prefix)
        {
            if (s.StartsWith(prefix))
            {
                return s.Substring(prefix.Length);
            }
            return s;
        }
    }
}
