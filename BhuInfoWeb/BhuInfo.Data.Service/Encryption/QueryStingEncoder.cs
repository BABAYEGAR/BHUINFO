using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhuInfo.Data.Service.Encryption
{
    public class QueryStingEncoder
    {
        public double Encode(string encodeMe)
        {
            return Convert.ToDouble(Convert.ToBase64String(Encoding.UTF8.GetBytes(encodeMe)));
        }

        public int Decode(string decodeMe)
        {
            byte[] encoded = Convert.FromBase64String(decodeMe);
            return (int) double.Parse(Encoding.UTF8.GetString(encoded));
        }
    }
}

