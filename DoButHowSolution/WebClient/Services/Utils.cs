using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MVCWebClient.Services
{
    public class Utils
    {
        public string StripHTML(string input)
        {
            return Regex.Replace(String.Copy(input), "<.*?>", String.Empty);
        }

        public string FormatString(string value)
        {
            if (value == "")
            {
                return value + " ...";
            }

            return value.Substring(0, value.Length > 100 ? 100 : value.Length - 1) + " ...";
        }
    }
}
