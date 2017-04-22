using GlownyShop.Data.EntityFramework;
using GlownyShop.Data.EntityFramework.Repositories;
using GlownyShop.Data.EntityFramework.Seed;
using GlownyShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using GlownyShop.Core.Logic;
using System;

namespace GlownyShop.Tests.Logic
{
    public class AdminUserRoleServiceShould
    {
        private readonly AdminUserRepository _adminUserRepository;
        private readonly AdminRoleRepository _adminRoleRepository;
        private DbContextOptions<GlownyShopContext> _options;
        private Mock<ILogger<GlownyShopContext>> _dbLogger;

        public AdminUserRoleServiceShould()
        {
            // Given
            _dbLogger = new Mock<ILogger<GlownyShopContext>>();
            // https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory
            _options = new DbContextOptionsBuilder<GlownyShopContext>()
                .UseInMemoryDatabase(databaseName: "GlownyShop_AdminUserRoleServiceShould")
                .Options;
            using (var context = new GlownyShopContext(_options, _dbLogger.Object))
            {
                context.EnsureSeedData();
            }
            var glownyShopContext = new GlownyShopContext(_options, _dbLogger.Object);
            var adminUserRepoLogger = new Mock<ILogger<AdminUserRepository>>();
            var adminRoleRepoLogger = new Mock<ILogger<AdminRoleRepository>>();
            _adminUserRepository = new AdminUserRepository(glownyShopContext, adminUserRepoLogger.Object);
            _adminRoleRepository = new AdminRoleRepository(glownyShopContext, adminRoleRepoLogger.Object);
        }

        [Fact]
        [Trait("test", "unit")]
        public async void SuccesAddNewUser()
        {
            // When
            var newAdminUser = new AdminUser
            {
                Email = "new-email@glowny-shop.com"
            };
            var adminUserRoleService = new AdminUserRoleService(_adminUserRepository, _adminRoleRepository);
            var adminUser = await adminUserRoleService.AddAdminUser(newAdminUser);

            // Then
            Assert.NotNull(adminUser);
            using (var db = new GlownyShopContext(_options, _dbLogger.Object))
            {
                var admnUser = await db.AdminUsers.FirstOrDefaultAsync(a => 
                    a.Email == "new-email@glowny-shop.com");
                Assert.NotNull(admnUser);
                Assert.Equal("new-email@glowny-shop.com", admnUser.Email);
                Assert.Equal(adminUser.Id, admnUser.Id);

                // Cleanup
                db.AdminUsers.Remove(admnUser);
                await db.SaveChangesAsync();
            }
        }

        [Fact]
        [Trait("test", "unit")]
        public async void FailedAddNewUserGivenDuplicateEmail()
        {
            // When
            var newAdminUser = new AdminUser
            {
                Id = 100,
                Email = "superadmin@glowny-shop.com"
            };
            var adminUserRoleService = new AdminUserRoleService(_adminUserRepository, _adminRoleRepository);

            //Then
            ArgumentException ex = await Assert.ThrowsAsync<ArgumentException>(
                () => adminUserRoleService.AddAdminUser(newAdminUser));
            Assert.Contains("already used", ex.Message);
            Assert.Equal("email", ex.ParamName);

            using (var db = new GlownyShopContext(_options, _dbLogger.Object))
            {
                var admnUser = await db.AdminUsers.FindAsync(100);
                Assert.Null(admnUser);
            }
        }

        [Fact]
        [Trait("test", "unit")]
        public async void FailedAddNewUserGivenDuplicateEmailWithCase()
        {
            // When
            var newAdminUser = new AdminUser
            {
                Id = 101,
                Email = "SuperAdmin@glowny-shop.com"
            };
            var adminUserRoleService = new AdminUserRoleService(_adminUserRepository, _adminRoleRepository);

            //Then
            ArgumentException ex = await Assert.ThrowsAsync<ArgumentException>(
                () => adminUserRoleService.AddAdminUser(newAdminUser));
            Assert.Contains("already used", ex.Message);
            Assert.Equal("email", ex.ParamName);

            using (var db = new GlownyShopContext(_options, _dbLogger.Object))
            {
                var admnUser = await db.AdminUsers.FindAsync(100);
                Assert.Null(admnUser);
            }
        }

        [Fact]
        [Trait("test", "unit")]
        public async void SuccesUpdateUserPasswordChanged()
        {
            // When
            var newAdminUser = new AdminUser
            {
                Id = 1,
                Email = "super-admin@glowny-shop.com"
            };
            var adminUserRoleService = new AdminUserRoleService(_adminUserRepository, _adminRoleRepository);
            var adminUser = await adminUserRoleService.UpdateAdminUser(newAdminUser);

            // Then
            Assert.NotNull(adminUser);
            using (var db = new GlownyShopContext(_options, _dbLogger.Object))
            {
                var admnUser = await db.AdminUsers.FindAsync(1);
                Assert.NotNull(admnUser);
                Assert.Equal("super-admin@glowny-shop.com", admnUser.Email);
                Assert.Equal(1, admnUser.Id);

                //clean up
                admnUser.Email = "superadmin@glowny-shop.com";
                db.SaveChanges();
            }
        }

        [Fact]
        [Trait("test", "unit")]
        public async void SuccesUpdateUserPasswordUnchanged()
        {
            // When
            var newAdminUser = new AdminUser
            {
                Id = 1,
                Email = "superadmin@glowny-shop.com",
                FirstName = "First 1"
            };
            var adminUserRoleService = new AdminUserRoleService(_adminUserRepository, _adminRoleRepository);
            var adminUser = await adminUserRoleService.UpdateAdminUser(newAdminUser);

            // Then
            Assert.NotNull(adminUser);
            using (var db = new GlownyShopContext(_options, _dbLogger.Object))
            {
                var admnUser = await db.AdminUsers.FindAsync(1);
                Assert.NotNull(admnUser);
                Assert.Equal("superadmin@glowny-shop.com", admnUser.Email);
                Assert.Equal(1, admnUser.Id);

                //clean up
                admnUser.Email = "superadmin@glowny-shop.com";
                db.SaveChanges();
            }
        }

        [Fact]
        [Trait("test", "unit")]
        public async void FailedUpdateNewUserDuplicateEmail()
        {
            // When
            var newAdminUser = new AdminUser
            {
                Id = 1,
                Email = "useradmin@glowny-shop.com"
            };
            var adminUserRoleService = new AdminUserRoleService(_adminUserRepository, _adminRoleRepository);
            //Then
            ArgumentException ex = await Assert.ThrowsAsync<ArgumentException>(
                () => adminUserRoleService.UpdateAdminUser(newAdminUser));
            Assert.Contains("already used", ex.Message);
            Assert.Equal("email", ex.ParamName);

            using (var db = new GlownyShopContext(_options, _dbLogger.Object))
            {
                var admnUser = await db.AdminUsers.FindAsync(1);
                Assert.NotNull(admnUser);
                Assert.Equal("superadmin@glowny-shop.com", admnUser.Email);
                Assert.Equal(1, admnUser.Id);
            }
        }

        [Fact]
        [Trait("test", "unit")]
        public async void FailedUpdateNewUserDuplicateEmailCase()
        {
            // When
            var newAdminUser = new AdminUser
            {
                Id = 1,
                Email = "UserAdmin@glowny-shop.com"
            };
            var adminUserRoleService = new AdminUserRoleService(_adminUserRepository, _adminRoleRepository);
            //Then
            ArgumentException ex = await Assert.ThrowsAsync<ArgumentException>(
                () => adminUserRoleService.UpdateAdminUser(newAdminUser));
            Assert.Contains("already used", ex.Message);
            Assert.Equal("email", ex.ParamName);

            using (var db = new GlownyShopContext(_options, _dbLogger.Object))
            {
                var admnUser = await db.AdminUsers.FindAsync(1);
                Assert.NotNull(admnUser);
                Assert.Equal("superadmin@glowny-shop.com", admnUser.Email);
                Assert.Equal(1, admnUser.Id);
            }
        }
    }
}
