using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextPvrTools
{
    public static class NextPvrDateTime
    {
        private static string sDateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'";
        private static string sDateFormat = "yyyy-MM-dd'T'HH:mm:ss.fffffff";

        public static bool TryParse(string s, out DateTime result)
        {
            if (DateTime.TryParseExact(s, sDateTimeFormat, CultureInfo.InvariantCulture, 
                DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out result))
            {
                return true;
            }
            if (DateTime.TryParseExact(s, sDateFormat, CultureInfo.InvariantCulture, 
                DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out result))
            {
                return true;
            }
            // TODO: Attempt brute force manual parsing of each number
            return false;
        }

        public static string Serialize(DateTime dt)
        {
            return dt.ToString(sDateTimeFormat, CultureInfo.InvariantCulture);
        }

        public static string Serialize(DateTime dt, bool dateOnly)
        {
            if (dateOnly)
                return dt.ToString("yyyy-MM-dd'T00:00:00.0000000'", CultureInfo.InvariantCulture);
            else
                return dt.ToString(sDateTimeFormat, CultureInfo.InvariantCulture);
        }
    }
}
