using GlownyShop.Core.Logic;
using System;
using System.Security.Cryptography;
using System.Text;

namespace GlownyShop.Tests.Trying
{
    class Program
    {
        private static string getSalt()
        {
            byte[] bytes = new byte[128 / 8];
            using (var keyGenerator = RandomNumberGenerator.Create())
            {
                keyGenerator.GetBytes(bytes);

                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        static void Main(string[] args)
        {
            //using (var sha256 = SHA256.Create())
            //{
            //    // Send a sample text to hash.
            //    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes("hello world"));

            //    // Get the hashed string.
            //    var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

            //    // Print the string. 
            //    Console.WriteLine(hash);

            //}

            //string a = "12345678";
            //string a1 = SecurityService.GenerateHashedPassword(a);
            //Console.WriteLine(string.Format("{0}|{1}", a, a1));
            //Console.WriteLine(SecurityService.CompareHashedPassword(a, a1));

            //a = "12345678";
            //a1 = SecurityService.GenerateHashedPassword(a);
            //Console.WriteLine(string.Format("{0}|{1}", a, a1));
            //Console.WriteLine(SecurityService.CompareHashedPassword(a, a1));

            //a = "1234sdfsdsdfsdf5678";
            //a1 = SecurityService.GenerateHashedPassword(a);
            //Console.WriteLine(string.Format("{0}|{1}", a, a1));
            //Console.WriteLine(SecurityService.CompareHashedPassword(a, a1));

            //a = "123434SDSA5678";
            //a1 = SecurityService.GenerateHashedPassword(a);
            //Console.WriteLine(string.Format("{0}|{1}", a, a1));
            //Console.WriteLine(SecurityService.CompareHashedPassword(a, a1));

            //a = "2%^532gjasdhjgsa12343A_sd&@*$#(XNSJD_+5678";
            //a1 = SecurityService.GenerateHashedPassword(a);
            //Console.WriteLine(string.Format("{0}|{1}", a, a1));
            //Console.WriteLine(SecurityService.CompareHashedPassword(a, a1));

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(Guid.NewGuid().ToString());
            }

            Console.Read();
        }
    }
}