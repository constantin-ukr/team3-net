using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BankSystemApi.Extensions
{
    public static class UserExtensions
    {
        public static string HashPassword(this string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
}
