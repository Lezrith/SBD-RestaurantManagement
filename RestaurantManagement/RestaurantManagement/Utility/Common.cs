using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RestaurantManagement
{
    internal static class Common
    {
        static public bool IsAssemblyDebugBuild()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(Common));
            return assembly.GetCustomAttributes(false).OfType<DebuggableAttribute>().Select(da => da.IsJITTrackingEnabled).FirstOrDefault();
        }

        static public string HashString(string s)
        {
            byte[] salt;
            byte[] hashBytes;
            byte[] hash;
            using (var rNGCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                rNGCryptoServiceProvider.GetBytes(salt = new byte[16]);
                using (var pbkdf2 = new Rfc2898DeriveBytes(s, salt, 1000))
                {
                    hash = pbkdf2.GetBytes(20);
                    hashBytes = new byte[36];
                    Array.Copy(salt, 0, hashBytes, 0, 16);
                    Array.Copy(hash, 0, hashBytes, 16, 20);
                }
            }
            return Convert.ToBase64String(hashBytes).Substring(0, 20);
        }

        public static bool IsInteger(string text)
        {
            if (Regex.IsMatch(text, "^0\\d+$")) return false;
            return Regex.IsMatch(text, "^\\d*$");
        }

        public static bool IsDecimal(string text, int precision, int scale)
        {
            if (Regex.IsMatch(text, "^0\\d+$")) return false;
            string regex = String.Format("^\\d{{1,{0}}}(,\\d{{0,{1}}})?$", precision - scale, scale);
            return Regex.IsMatch(text, regex);
        }
    }
}