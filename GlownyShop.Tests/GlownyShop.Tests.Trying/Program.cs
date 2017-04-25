using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Security.Cryptography;

namespace GlownyShop.Tests.Trying
{
    internal class Program
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

        private static void Main(string[] args)
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
            try
            {
                var disco = DiscoveryClient.GetAsync("http://localhost:51000").Result;

                // request token
                var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
                var tokenResponse = tokenClient.RequestClientCredentialsAsync("api1").Result;

                if (tokenResponse.IsError)
                {
                    Console.WriteLine(tokenResponse.Error);
                    Console.Read();
                    return;
                }

                Console.WriteLine(tokenResponse.Json);

                // call api
                var client = new HttpClient();
                client.SetBearerToken(tokenResponse.AccessToken);

                var response = client.GetAsync("http://localhost:5555/identity").Result;
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.StatusCode);
                }
                else
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(JArray.Parse(content));
                }

                //for (int i = 0; i < 4; i++)
                //{
                //    Console.WriteLine(Guid.NewGuid().ToString());
                //}

                // request token
                tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
                tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync("superadmin@glowny-shop.com", "P@ssw0rd", "api1").Result;

                if (tokenResponse.IsError)
                {
                    Console.WriteLine(tokenResponse.Error);
                    Console.Read();
                    return;
                }

                Console.WriteLine(tokenResponse.Json);

                // call api
                client = new HttpClient();
                client.SetBearerToken(tokenResponse.AccessToken);

                response = client.GetAsync("http://localhost:5555/identity").Result;
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.StatusCode);
                }
                else
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(JArray.Parse(content));
                }

                Console.WriteLine("\n\n");
            }
            catch { }

            Console.Read();
        }
    }
}