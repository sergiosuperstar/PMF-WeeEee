using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DailyPlanning.ExtendedMethods
{
    public static class HtmlParser
    {
        public static IEnumerable<string> Tags = new List<string> { "<p", "<table", "<tr", "<td", "<th" };

        public static string Parse(this String str)
        {
            var index = str.IndexOf("<");

            var substr = str;
            bool found = false;

            while (index > -1)
            {
                substr = substr.Substring(index);
                foreach (string tag in Tags)
                {
                    if (substr.ToLower().StartsWith(tag) || substr.StartsWith("</"))
                    {
                        found = true;
                        substr = substr.Substring(substr.IndexOf(">"));
                        break;
                    }
                }

                if (!found)
                {
                    var notAllowed = substr.Substring(substr.IndexOf("<") + 1, substr.IndexOf(">"));

                    substr = substr.Replace("<" + notAllowed, "");
                    substr = substr.Replace("</" + notAllowed, "");

                    str = str.Replace("<" + notAllowed, "");
                    str = str.Replace("</" + notAllowed, "");
                }

                found = false;
                index = substr.IndexOf("<");
            }

            return str;
        }
    }
}