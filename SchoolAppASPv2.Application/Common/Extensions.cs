using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppASPv2.Application.Common
{
    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }
    }

    public static class LinqExtension
    {
        public static IEnumerable<T> TakenUntilIncluding<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            foreach(T el in list)
            {
                yield return el;
                if (predicate(el))
                    yield break;
            }
        }
    }

    public static class PasswordExtension
    {
        public static string CalculateMD5Hash(this string input)
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(input))
            {
                // step 1, calculate MD5 hash from input
                MD5 md5 = MD5.Create();
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hash = md5.ComputeHash(inputBytes);

                // step 2, convert byte array to hex string
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
            }
            return sb.ToString();

        }
    }
}
