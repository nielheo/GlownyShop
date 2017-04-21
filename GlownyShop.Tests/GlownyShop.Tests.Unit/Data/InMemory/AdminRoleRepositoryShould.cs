using GlownyShop.Data.InMemory;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GlownyShop.Tests.Unit.Data.InMemory
{
    public class AdminRoleRepositoryShould
    {
        private readonly AdminRoleRepository _adminRoleRepository;

        public AdminRoleRepositoryShould()
        {
            // Given
            var logger = new Mock<ILogger<AdminRoleRepository>>();
            _adminRoleRepository = new AdminRoleRepository(logger.Object);
        }

        [Fact]
        [Trait("test", "unit")]
        public async void ReturnSuperAdminGivenIdof0()
        {
            // When
            var adminRole = await _adminRoleRepository.Get(0);

            // Then
            Assert.NotNull(adminRole);
            Assert.Equal("Super Admin", adminRole.Name);
        }

        [Fact]
        [Trait("test", "unit")]
        public async void ReturnUserAdminGivenIdof1()
        {
            // When
            var adminRole = await _adminRoleRepository.Get(1);

            // Then
            Assert.NotNull(adminRole);
            Assert.Equal("User Admin", adminRole.Name);
        }

        [Fact]
        [Trait("test", "unit")]
        public async void Return2RowsForGetAllUserAdmins()
        {
            // When
            var adminRoles = await _adminRoleRepository.GetAll();

            // Then
            Assert.NotNull(adminRoles);
            Assert.Equal(2, adminRoles.Count);
        }
    }
}