using System;
using System.Linq;
using System.Text;

namespace LedItBe.Core.Utils
{
    internal class StringUtils
    {
        private const string _asciiTable = "!\"#$%&'()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[\\]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~";
        private static Random _random = new Random();

        public static string GenerateAsciiString(int length)
        {
            return new string(Enumerable.Repeat(_asciiTable, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public static string Base64Encode(string text)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
        }

        public static string Base64Decode(string base64)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64));
        }
    }
}
