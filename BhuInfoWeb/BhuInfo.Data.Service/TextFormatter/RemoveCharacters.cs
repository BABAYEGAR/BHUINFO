using System.Text.RegularExpressions;

namespace BhuInfo.Data.Service.TextFormatter
{
    public class RemoveCharacters
    {
        /// <summary>
        ///     This method removes specified special characters from a text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string RemoveSpecialCharacters(string text)
        {
            var pattern = @"<(.|\n)*?>";
            return Regex.Replace(text, pattern, string.Empty);
        }

        /// <summary>
        ///     This method removes enodings froma string
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static string StripUnicodeCharactersFromString(string inputValue)
        {
            return Regex.Replace(inputValue, @"[^\u0000-\u007F]", string.Empty);
        }
    }
}