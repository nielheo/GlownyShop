using GlownyShop.Core.Logic;
using GlownyShop.Data.EntityFramework;
using GlownyShop.Data.EntityFramework.Repositories;
using GlownyShop.Data.EntityFramework.Seed;
using GlownyShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

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
            string newId = Guid.NewGuid().ToString();
            var newAdminUser = new AdminUser
            {
                Id = newId,
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
            string newGuid = Guid.NewGuid().ToString();
            var newAdminUser = new AdminUser
            {
                Id = newGuid,
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
                var admnUser = await db.AdminUsers.FindAsync(newGuid);
                Assert.Null(admnUser);
            }
        }

        [Fact]
        [Trait("test", "unit")]
        public async void FailedAddNewUserGivenDuplicateEmailWithCase()
        {
            // When
            string newGuid = Guid.NewGuid().ToString();
            var newAdminUser = new AdminUser
            {
                Id = newGuid,
                Email = "SuperAdmin@glowny-shop.com",
            };
            var adminUserRoleService = new AdminUserRoleService(_adminUserRepository, _adminRoleRepository);

            //Then
            ArgumentException ex = await Assert.ThrowsAsync<ArgumentException>(
                () => adminUserRoleService.AddAdminUser(newAdminUser));
            Assert.Contains("already used", ex.Message);
            Assert.Equal("email", ex.ParamName);

            using (var db = new GlownyShopContext(_options, _dbLogger.Object))
            {
                var admnUser = await db.AdminUsers.FindAsync(newGuid);
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
                Id = new Guid("1d9a394c-60b8-4523-ab41-52b2936836c3").ToString(),
                Email = "super-admin@glowny-shop.com",
                FirstName = "First 1",
                LastName = "Last 1",
                IsActive = false,
                Password = "0000"
            };
            var adminUserRoleService = new AdminUserRoleService(_adminUserRepository, _adminRoleRepository);
            var adminUser = await adminUserRoleService.UpdateAdminUser(newAdminUser);

            // Then
            Assert.NotNull(adminUser);
            using (var db = new GlownyShopContext(_options, _dbLogger.Object))
            {
                var admnUser = await db.AdminUsers.FindAsync(new Guid("1d9a394c-60b8-4523-ab41-52b2936836c3").ToString());
                Assert.NotNull(admnUser);
                Assert.Equal("super-admin@glowny-shop.com", admnUser.Email);
                Assert.Equal(new Guid("1d9a394c-60b8-4523-ab41-52b2936836c3").ToString(), admnUser.Id);
                Assert.Equal("First 1", admnUser.FirstName);
                Assert.Equal("Last 1", admnUser.LastName);
                Assert.Equal(false, admnUser.IsActive);
                Assert.NotEqual("0000", admnUser.Password);

                //clean up
                admnUser.Email = "superadmin@glowny-shop.com";
                admnUser.FirstName = "Super";
                admnUser.LastName = "Admin";
                admnUser.Password = SecurityService.GenerateHashedPassword("P@ssw0rd");
                admnUser.IsActive = true;

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
                Id = new Guid("1d9a394c-60b8-4523-ab41-52b2936836c3").ToString(),
                Email = "superadmin@glowny-shop.com",
                FirstName = "First 1",
                LastName = "Last 1",
                IsActive = false,
                Password = "0000"
            };
            var adminUserRoleService = new AdminUserRoleService(_adminUserRepository, _adminRoleRepository);
            var adminUser = await adminUserRoleService.UpdateAdminUser(newAdminUser);

            // Then
            Assert.NotNull(adminUser);
            using (var db = new GlownyShopContext(_options, _dbLogger.Object))
            {
                var admnUser = await db.AdminUsers.FindAsync(new Guid("1d9a394c-60b8-4523-ab41-52b2936836c3").ToString());
                Assert.NotNull(admnUser);
                Assert.Equal("superadmin@glowny-shop.com", admnUser.Email);
                Assert.Equal(new Guid("1d9a394c-60b8-4523-ab41-52b2936836c3").ToString(), admnUser.Id);
                Assert.Equal("First 1", admnUser.FirstName);
                Assert.Equal("Last 1", admnUser.LastName);
                Assert.Equal(false, admnUser.IsActive);
                Assert.NotEqual("0000", admnUser.Password);

                //clean up
                admnUser.Email = "superadmin@glowny-shop.com";
                admnUser.FirstName = "Super";
                admnUser.LastName = "Admin";
                admnUser.Password = SecurityService.GenerateHashedPassword("P@ssw0rd");
                admnUser.IsActive = true;
                db.SaveChanges();
            }
        }

        [Fact]
        [Trait("test", "unit")]
        public async void FailedUpdateUserDuplicateEmail()
        {
            // When
            var newAdminUser = new AdminUser
            {
                Id = new Guid("1d9a394c-60b8-4523-ab41-52b2936836c3").ToString(),
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
                var admnUser = await db.AdminUsers.FindAsync(new Guid("1d9a394c-60b8-4523-ab41-52b2936836c3").ToString());
                Assert.NotNull(admnUser);
                Assert.Equal("superadmin@glowny-shop.com", admnUser.Email);
                Assert.Equal(new Guid("1d9a394c-60b8-4523-ab41-52b2936836c3").ToString(), admnUser.Id);
            }
        }

        [Fact]
        [Trait("test", "unit")]
        public async void FailedUpdateNewUserDuplicateEmailCase()
        {
            // When
            var newAdminUser = new AdminUser
            {
                Id = new Guid("1d9a394c-60b8-4523-ab41-52b2936836c3").ToString(),
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
                var admnUser = await db.AdminUsers.FindAsync(new Guid("1d9a394c-60b8-4523-ab41-52b2936836c3").ToString());
                Assert.NotNull(admnUser);
                Assert.Equal("superadmin@glowny-shop.com", admnUser.Email);
                Assert.Equal(new Guid("1d9a394c-60b8-4523-ab41-52b2936836c3").ToString(), admnUser.Id);
            }
        }
    }
}