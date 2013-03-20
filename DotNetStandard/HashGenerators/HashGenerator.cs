using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DotNetStandard.HashGenerators
{
    public static class HashGenerator
    {
        public static string GetRandomHash()
        {
            Random r = new Random();
            return GetHash(r.Next(Int32.MaxValue).ToString(CultureInfo.InvariantCulture));
        }

        public static string GetHash(string text)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] message = UE.GetBytes(text);

            SHA256Managed hashString = new SHA256Managed();
            StringBuilder hex = new StringBuilder();

            byte[] hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex.Append(String.Format("{0:x2}", x));
            }
            return hex.ToString();
        }

        public static string GetBCryptHash(string text)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(text);

            return hash;
        }
    }
}
