using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhuInfo.Data.Service.TextFormatter
{
    public class RemoveCharacters
    {
        /// <summary>
        /// This method removes specified special characters from a text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public  string RemoveSpecialCharacters(string text)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in text)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_' || c == '-')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
