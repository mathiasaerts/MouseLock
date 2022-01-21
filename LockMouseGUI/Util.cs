using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseLock
{
    static class Util
    {
        public static string Truncate(this string value, int maxChars)
        {
            if (value.Length <= maxChars)
                return value;
            var half = maxChars / 2;
            return value.Substring(0, half) + "....." + value.Substring(value.Length - half, half);
        }

        public static bool inList(string checkString, List<string> list)
        {
            // To lowercase
            checkString = checkString.ToLower();
            foreach (string item in list)
            {
                if (checkString.Equals(item.ToLower()))
                    return true;
            }
            return false;
        }
    }
}
