using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DailyPlanning.ExtendedMethods
{
    public static class HtmlParser
    {
        public static IEnumerable<string> Tags = new List<string> { "<p", "</p", "<a", "</a", "<table", "</table>", "<tr", "</tr", "<td", "</td", "<th", "</th", "<img", "<s", "</s", "<em", "</em", "<strong", "</strong", "<ul", "</ul", "<ol", "</ol", "<li", "</li", "<blockquote", "</blockquote", "<h1", "<h2", "<h3", "<h4", "<h5", "<h6", "</h1", "</h2", "</h3", "</h4", "</h5", "</h6" };

        public static string Parse(this String html)
        {

            if (String.IsNullOrEmpty(html))
            {
                return html;
            }

            Regex regex = new Regex(@"<[^>]*(>|$)");

            MatchCollection allTags = regex.Matches(html);

            foreach (Match tag in allTags)
            {
                var str = tag.Value.IndexOf(" ") == -1 ? tag.Value.Substring(0, tag.Value.IndexOf(">")) : tag.Value.Substring(0, tag.Value.IndexOf(" "));
                
                if (!Tags.Contains(str))
                {
                    html = html.Replace(tag.Value, tag.Value.StartsWith("</") ? "</p>" : "<p>");
                }
            }

            return html;
        }
    }
}