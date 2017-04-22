using GlownyShop.Core.Logic;
using GlownyShop.Data.EntityFramework;
using GlownyShop.Data.EntityFramework.Repositories;
using GlownyShop.Data.EntityFramework.Seed;
using GlownyShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using Xunit;

namespace GlownyShop.Tests.Unit.Data.EntityFramework.Repositories
{
    public class AdminUserRepositoryShould
    {
        private readonly AdminUserRepository _adminUserRepository;
        private DbContextOptions<GlownyShopContext> _options;
        private Mock<ILogger<GlownyShopContext>> _dbLogger;

        public AdminUserRepositoryShould()
        {
            // Given
            _dbLogger = new Mock<ILogger<GlownyShopContext>>();
            // https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory
            _options = new DbContextOptionsBuilder<GlownyShopContext>()
                .UseInMemoryDatabase(databaseName: "GlownyShop_AdminUser")
                .Options;
            using (var context = new GlownyShopContext(_options, _dbLogger.Object))
            {
                context.EnsureSeedData();
            }
            var glownyShopContext = new GlownyShopContext(_options, _dbLogger.Object);
            var repoLogger = new Mock<ILogger<AdminUserRepository>>();
            _adminUserRepository = new AdminUserRepository(glownyShopContext, repoLogger.Object);
        }

        [Fact]
        [Trait("test", "unit")]
        public async void Return4RowsGivenGetAll()
        {
            // When
            var glownyShopContext = new GlownyShopContext(_options, _dbLogger.Object);
            var repoLogger = new Mock<ILogger<AdminUserRepository>>();
            var _adminUserRepositoryAll = new AdminUserRepository(glownyShopContext, repoLogger.Object);
            var adminUsers = await _adminUserRepositoryAll.GetAll();

            // Then
            Assert.NotNull(adminUsers);
            Assert.Equal(4, adminUsers.Count);

            //CleanUp
            var saved = await _adminUserRepositoryAll.SaveChangesAsync();
        }

        //[Fact]
        //[Trait("test", "unit")]
        //public async void ReturnSuperAdminGivenIdOf0()
        //{
        //    // When
        //    var adminRole0 = await _adminRoleRepository.Get(0);

        //    // Then
        //    Assert.NotNull(adminRole0);
        //    Assert.Equal("Super Admin", adminRole0.Name);
        //}

        //[Fact]
        //[Trait("test", "unit")]
        //public async void ReturnUserAdminGivenIdOf1()
        //{
        //    // When
        //    var adminRole1 = await _adminRoleRepository.Get(1);

        //    // Then
        //    Assert.NotNull(adminRole1);
        //    Assert.Equal("User Admin", adminRole1.Name);
        //}

        [Fact]
        [Trait("test", "unit")]
        public async void AddNewAdminUser()
        {
            // Given
            var newAdminUser = new AdminUser
            {
                Id = 10,
                FirstName = "First",
                LastName = "Last",
                Email = "first@last.com",
                IsActive = false,
                Password = SecurityService.GenerateHashedPassword("P@ssw0rd"),
            };

            // When
            _adminUserRepository.Add(newAdminUser);
            var saved = await _adminUserRepository.SaveChangesAsync();

            // Then
            Assert.True(saved);
            //var id = newAdminUser.Id;
            using (var db = new GlownyShopContext(_options, _dbLogger.Object))
            {
                var adminUser = await db.AdminUsers.FindAsync(10);
                Assert.NotNull(adminUser);
                Assert.Equal(10, adminUser.Id);
                Assert.Equal("First", adminUser.FirstName);
                Assert.Equal("Last", adminUser.LastName);
                Assert.Equal("first@last.com", adminUser.Email);

                // Cleanup
                db.AdminUsers.Remove(adminUser);
                await db.SaveChangesAsync();
            }
        }
    }
}