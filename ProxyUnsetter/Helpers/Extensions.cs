using System;
using System.Collections.Generic;

namespace ProxyUnsetter.Helpers
{
    public static class Extensions
    {
        /// <summary>
        /// 'Humanize' a camel cased string. "TheSystemCannotApplyYourChanges" becomes:
        /// "the system cannot apply your changes."
        /// </summary>
        /// <param name="camelCasedText"></param>
        /// <returns></returns>
        public static string Humanize(this string camelCasedText)
        {
            var result = new List<char> { camelCasedText.Substring(0, 1).ToLower()[0] };
            foreach (var c in camelCasedText.Substring(1))
            {
                if (c > 64 && c < 91) // it's uppercase 
                {
                    result.Add(' ');
                    result.Add((char)(c + 32));
                }
                else if ((c > 47 && c < 58) || c == 95) // it's a number or 
                {
                    result.Add(' ');
                    result.Add(c);
                }
                else if (c == 95) // it's an underscore
                {
                    result.Add(' ');
                }
                else
                {
                    result.Add(c);
                }
            }
            return new string(result.ToArray());
        }

        public static string Humanize(this Enum camelCasedEnumValue)
        {
            return camelCasedEnumValue.ToString().Humanize();
        }

        /// <summary>
        /// Trim the string to the maximum length specified (minus 2) and add two dots.
        /// </summary>
        /// <param name="longString"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string TrimAndDot(this string longString, int maxLength)
        {
            if (longString.Length >= maxLength)
            {
                return longString.Substring(0, maxLength - 3) + "..";
            }
            return longString;
        }

        public static string TrimHttpAndPort(this string url)
        {
            var result = url;
            if (result.StartsWith("http://"))
            {
                result = result.Substring(7);
            }
            if (result.Contains(":"))
            {
                // ReSharper disable once StringIndexOfIsCultureSpecific.1
                result = result.Substring(0, result.IndexOf(":"));
            }
            return result;
        }
    }
}
