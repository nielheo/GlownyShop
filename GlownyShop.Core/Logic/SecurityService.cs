using GlownyShop.Core.Data;
using GlownyShop.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace GlownyShop.Core.Logic
{
    public class SecurityService : ISecurityService
    {
        private readonly IAdminUserRepository _adminUserRepository;

        public SecurityService(IAdminUserRepository adminUserRepository)
        {
            _adminUserRepository = adminUserRepository;
        }

        private static string GetSalt()
        {
            byte[] bytes = new byte[128 / 8];
            using (var keyGenerator = RandomNumberGenerator.Create())
            {
                keyGenerator.GetBytes(bytes);

                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        private static string GetHash(string text)
        {
            // SHA512 is disposable by inheritance.
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));

                // Get the hashed string.
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private static string FirstHalf(string text)
        {
            int len = text.Length;
            int half = len / 2;
            return text.Substring(0, half);
        }

        private static string SecondHalf(string text)
        {
            int len = text.Length;
            int half = len / 2;
            return text.Substring(half);
        }

        private static bool ComplexPassword(string password)
        {
            try
            {
                return (password.Length >= 8
                    && password.Any(char.IsUpper)
                    && password.Any(char.IsLower)
                    && password.Any(char.IsDigit)
                    && password.Any(char.IsPunctuation));
            }
            finally
            {
                password = null;
            }
        }
        
        private static string CalculateHash(string salt, string password)
        {
            string forHash = FirstHalf(salt) + FirstHalf(password) + SecondHalf(salt) + SecondHalf(password);
            string hashed = GetHash(forHash);

            try
            {
                return FirstHalf(salt) + FirstHalf(hashed) + SecondHalf(salt) + SecondHalf(hashed);
            }
            finally
            {
                salt = null;
                password = null;
                forHash = null;
                hashed = null;
            }
        }

        public static bool CompareHashedPassword(string password, string hashedPassword)
        {
            string salt = FirstHalf(hashedPassword).Substring(0, 16)
                + SecondHalf(hashedPassword).Substring(0, 16);

            try
            {
                return CalculateHash(salt, password) == hashedPassword;
            }
            finally
            {
                salt = null;
                password = null;
                hashedPassword = null;
            }
        }

        public static string GenerateHashedPassword(string password)
        {
            string salt = GetSalt();
            try
            {
                if (!ComplexPassword(password))
                    throw new ArgumentException(string.Format("{0} - {1}", "Weak Password", 0), "password");

                return CalculateHash(salt, password);
            }
            finally
            {
                salt = null;
                password = null;
            }
        }

        public AdminUser ValidateAdminUser(string email, string password)
        {
            var adminUser = _adminUserRepository.GetByEmail(email).Result;

            if (adminUser == null)
                return null;

            //adminUser = _adminUserRepository.Get(adminUser.Id, "AdminUserRoles.AdminUser").Result;

            if (CompareHashedPassword(password, adminUser.Password))
                return adminUser;
            else
                return null;
                   
        }
    }
}
