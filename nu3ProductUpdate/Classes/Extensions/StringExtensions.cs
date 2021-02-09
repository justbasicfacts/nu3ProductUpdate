namespace nu3ProductUpdate.Classes.Extensions
{
    public static class StringExtensions
    {
        public static bool IsEquals(this string input, string other)
        {
            return NullStringComparer.NullEqualsEmptyComparer.Equals(input, other);
        }
    }
}