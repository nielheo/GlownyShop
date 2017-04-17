using GlownyShop.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Text;
using Xunit;

namespace GlownyShop.Tests.Integration.Api.Controllers
{
    public class GraphQLControllerShould
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public GraphQLControllerShould()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Test")
                .UseStartup<Startup>()
            );
            _client = _server.CreateClient();
        }

        [Fact]
        public async void ReturnAdminRoleSuperAdminWhenSelectAdminRoles()
        {
            // Given
            var query = @"{
                ""query"": ""query { viewer { adminRoles { id name } } }""
            }";
            var content = new StringContent(query, Encoding.UTF8, "application/json");

            // When
            var response = await _client.PostAsync("/graphql", content);

            // Then
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Super Admin", responseString);
        }

        [Fact]
        public async void ReturnAdminRoleSuperAdminGivenId0()
        {
            // Given
            var query = @"{
                ""query"": ""query { viewer { adminRole(id:0) { id name } } }""
            }";
            var content = new StringContent(query, Encoding.UTF8, "application/json");

            // When
            var response = await _client.PostAsync("/graphql", content);

            // Then
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Super Admin", responseString);
        }

        [Fact]
        public async void ReturnAdminRoleUserAdminGivenId1()
        {
            // Given
            var query = @"{
                ""query"": ""query { viewer { adminRole(id:1) { id name } } }""
            }";
            var content = new StringContent(query, Encoding.UTF8, "application/json");

            // When
            var response = await _client.PostAsync("/graphql", content);

            // Then
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("User Admin", responseString);
        }

        [Fact]
        public async void ReturnAdminRoleClientAdminGivenId2()
        {
            // Given
            var query = @"{
                ""query"": ""query { viewer { adminRole(id:2) { id name } } }""
            }";
            var content = new StringContent(query, Encoding.UTF8, "application/json");

            // When
            var response = await _client.PostAsync("/graphql", content);

            // Then
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Client Admin", responseString);
        }
    }
}