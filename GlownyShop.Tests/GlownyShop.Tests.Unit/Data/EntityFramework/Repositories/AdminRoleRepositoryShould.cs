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
    public class AdminRoleRepositoryShould
    {
        private readonly AdminRoleRepository _adminRoleRepository;
        private DbContextOptions<GlownyShopContext> _options;
        private Mock<ILogger<GlownyShopContext>> _dbLogger;

        public AdminRoleRepositoryShould()
        {
            // Given
            _dbLogger = new Mock<ILogger<GlownyShopContext>>();
            // https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory
            _options = new DbContextOptionsBuilder<GlownyShopContext>()
                .UseInMemoryDatabase(databaseName: "GlownyShop")
                .Options;
            using (var context = new GlownyShopContext(_options, _dbLogger.Object))
            {
                context.EnsureSeedData();
            }
            var glownyShopContext = new GlownyShopContext(_options, _dbLogger.Object);
            var repoLogger = new Mock<ILogger<AdminRoleRepository>>();
            _adminRoleRepository = new AdminRoleRepository(glownyShopContext, repoLogger.Object);
        }

        [Fact]
        [Trait("test", "unit")]
        public async void Return3RowsGivenGetAll()
        {
            // When
            var glownyShopContext = new GlownyShopContext(_options, _dbLogger.Object);
            var repoLogger = new Mock<ILogger<AdminRoleRepository>>();
            var _adminRoleRepositoryAll = new AdminRoleRepository(glownyShopContext, repoLogger.Object);
            var adminRoles = await _adminRoleRepositoryAll.GetAll();

            // Then
            Assert.NotNull(adminRoles);
            Assert.Equal(3, adminRoles.Count);

            //CleanUp
            var saved = await _adminRoleRepositoryAll.SaveChangesAsync();
        }

        [Fact]
        [Trait("test", "unit")]
        public async void ReturnSuperAdminGivenIdOf0()
        {
            // When
            var adminRole0 = await _adminRoleRepository.Get(0);

            // Then
            Assert.NotNull(adminRole0);
            Assert.Equal("Super Admin", adminRole0.Name);
        }

        [Fact]
        [Trait("test", "unit")]
        public async void ReturnUserAdminGivenIdOf1()
        {
            // When
            var adminRole1 = await _adminRoleRepository.Get(1);

            // Then
            Assert.NotNull(adminRole1);
            Assert.Equal("User Admin", adminRole1.Name);
        }
               
        [Fact]
        [Trait("test", "unit")]
        public async void AddNewAdminRole()
        {
            // Given
            var adminRoleNormalUser = new AdminRole { Id = 9,
                Name = "Normal User" };

            // When
            _adminRoleRepository.Add(adminRoleNormalUser);
            var saved = await _adminRoleRepository.SaveChangesAsync();

            // Then
            Assert.True(saved);
            using (var db = new GlownyShopContext(_options, _dbLogger.Object))
            {
                var droid = await db.AdminRoles.FindAsync(9);
                Assert.NotNull(droid);
                Assert.Equal(9, droid.Id);
                Assert.Equal("Normal User", droid.Name);

                // Cleanup
                db.AdminRoles.Remove(droid);
                await db.SaveChangesAsync();
            }
        }

        [Fact]
        [Trait("test", "unit")]
        public async void UpdateExistingAdminRole()
        {
            // Given
            var superAdmin = await _adminRoleRepository.Get(0);
            superAdmin.Name = "SuperAdmin";

            // When
            _adminRoleRepository.Update(superAdmin);
            var saved = await _adminRoleRepository.SaveChangesAsync();

            // Then
            Assert.True(saved);
            using (var db = new GlownyShopContext(_options, _dbLogger.Object))
            {
                var adminRole = await db.AdminRoles.FindAsync(0);
                Assert.NotNull(adminRole);
                Assert.Equal(0, adminRole.Id);
                Assert.Equal("SuperAdmin", adminRole.Name);

                // Cleanup
                adminRole.Name = "Super Admin";
                db.AdminRoles.Update(adminRole);
                await db.SaveChangesAsync();
            }
        }

        [Fact]
        [Trait("test", "unit")]
        public async void DeleteExistingAdminRole()
        {
            // Given
            using (var db = new GlownyShopContext(_options, _dbLogger.Object))
            {
                var adminRole9 = new AdminRole { Id = 9, Name = "Normal User" };
                await db.AdminRoles.AddAsync(adminRole9);
                await db.SaveChangesAsync();
            }

            // When
            _adminRoleRepository.Delete(9);
            var saved = await _adminRoleRepository.SaveChangesAsync();

            // Then
            Assert.True(saved);
            using (var db = new GlownyShopContext(_options, _dbLogger.Object))
            {
                var deletedDroid = await db.AdminRoles.FindAsync(9);
                Assert.Null(deletedDroid);
            }
        }

        [Fact]
        [Trait("test", "unit")]
        public async void Return1AdminUsersGivenIdOf0()
        {
            // When
            var adminRole1 = await _adminRoleRepository.Get(0, include: "AdminUserRoles.AdminUser");
            
            // Then
            Assert.NotNull(adminRole1);
            Assert.Equal(1, adminRole1.AdminUserRoles.Select(u=>u.AdminUser).Count());
        }

        [Fact]
        [Trait("test", "unit")]
        public async void Return2AdminUsersGivenIdOf2()
        {
            // When
            var adminRole1 = await _adminRoleRepository.Get(2, include: "AdminUserRoles.AdminUser");

            // Then
            Assert.NotNull(adminRole1);
            Assert.Equal(2, adminRole1.AdminUserRoles.Select(u => u.AdminUser).Count());
        }
    }
}