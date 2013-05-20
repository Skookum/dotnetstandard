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

        public static string StringHash(int length,
                                          string allowedChars =
                                              "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            if (length < 0) throw new ArgumentOutOfRangeException("length", "length cannot be less than zero.");
            if (string.IsNullOrEmpty(allowedChars)) throw new ArgumentException("allowedChars may not be empty.");

            const int byteSize = 0x100;
            var allowedCharSet = new HashSet<char>(allowedChars).ToArray();
            if (byteSize < allowedCharSet.Length)
                throw new ArgumentException(String.Format("allowedChars may contain no more than {0} characters.",
                                                          byteSize));

            // Guid.NewGuid and System.Random are not particularly random. By using a
            // cryptographically-secure random number generator, the caller is always
            // protected, regardless of use.
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                var result = new StringBuilder();
                var buf = new byte[128];
                while (result.Length < length)
                {
                    rng.GetBytes(buf);
                    for (var i = 0; i < buf.Length && result.Length < length; ++i)
                    {
                        // Divide the byte into allowedCharSet-sized groups. If the
                        // random value falls into the last group and the last group is
                        // too small to choose from the entire allowedCharSet, ignore
                        // the value in order to avoid biasing the result.
                        var outOfRangeStart = byteSize - (byteSize%allowedCharSet.Length);
                        if (outOfRangeStart <= buf[i]) continue;
                        result.Append(allowedCharSet[buf[i]%allowedCharSet.Length]);
                    }
                }
                return result.ToString();
            }
        }
    }
}
