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

        public static string GetMd5Hash(string input)
        {
            var data = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }
    }
}
