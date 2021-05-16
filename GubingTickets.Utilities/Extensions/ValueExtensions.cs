using System;
using System.Linq;

namespace GubingTickets.Utilities.Extensions
{
    public static class ValueExtensions
    {
        private static Random random = new Random();

        private static string _UpperChars = "ABCDEFGHIJKLMNPQRSTUVWXYZ123456789";
        private static string _UpperWithLowerChars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnPpQqRrSsTtUuVvWwXxYyZz123456789";

        public static string RandomString(this int length, bool allowLower)
        {
            if(allowLower)
                return new string(Enumerable.Repeat(_UpperWithLowerChars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            else
                return new string(Enumerable.Repeat(_UpperChars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
