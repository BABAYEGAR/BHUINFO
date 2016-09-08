using System;
using System.Text;
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
            return Regex.Replace(text, pattern, String.Empty);
        }
    }
}