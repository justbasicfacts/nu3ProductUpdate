using System.Collections.Generic;

namespace nu3ProductUpdate.Classes
{
    public class NullStringComparer : EqualityComparer<string>
    {
        public static IEqualityComparer<string> NullEqualsEmptyComparer
        {
            get
            {
                return new NullStringComparer();
            }
        }

        public override bool Equals(string x, string y)
        {
            return string.Equals(x, y)
                || (string.IsNullOrEmpty(x) && string.IsNullOrEmpty(y));
        }

        public override int GetHashCode(string obj)
        {
            if (string.IsNullOrEmpty(obj))
                return 0;
            else
                return obj.GetHashCode();
        }
    }
}