using GlownyShop.Core.Logic;
using GlownyShop.Data.EntityFramework;
using GlownyShop.Data.EntityFramework.Repositories;
using GlownyShop.Data.EntityFramework.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace GlownyShop.Tests.Logic
{
    public class SecurityServiceShould
    {
        private readonly AdminUserRepository _adminUserRepository;
        private readonly AdminRoleRepository _adminRoleRepository;
        private readonly SecurityService _securityService;
        private DbContextOptions<GlownyShopContext> _options;
        private Mock<ILogger<GlownyShopContext>> _dbLogger;

        public SecurityServiceShould()
        {
            // Given
            _dbLogger = new Mock<ILogger<GlownyShopContext>>();
            // https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory
            _options = new DbContextOptionsBuilder<GlownyShopContext>()
                .UseInMemoryDatabase(databaseName: "GlownyShop_SecurityServiceShould")
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
            _securityService = new SecurityService(_adminUserRepository);
        }

        [Fact]
        [Trait("test", "unit")]
        public void ReturnTrueGivenDifferentPassword()
        {
            // When
            string password1 = "9284Asdfhk@#&^_jhd632_@&";
            string password2 = "9284Asdfhk@#&^_jhd632_@&";
            string hashedPassword = SecurityService.GenerateHashedPassword(password1);
            bool compared = SecurityService.CompareHashedPassword(password2, hashedPassword);

            // Then
            Assert.NotNull(hashedPassword);
            Assert.Equal(true, compared);
        }

        [Fact]
        [Trait("test", "unit")]
        public void ReturnFalseGivenDifferentPassword()
        {
            // When
            string password1 = "9284sdAfhk@#&^_jhd632_@&";
            string password2 = "9284sdAhk@#&^_jhd632_@&";
            string hashedPassword = SecurityService.GenerateHashedPassword(password1);
            bool compared = SecurityService.CompareHashedPassword(password2, hashedPassword);

            // Then
            Assert.NotNull(hashedPassword);
            Assert.Equal(false, compared);
        }

        [Fact]
        [Trait("test", "unit")]
        public void ReturnFalseGivenDifferentCasePassword()
        {
            // When
            string password1 = "9284sdAfhk@#&^_jhd632_@&";
            string password2 = "9284SdAfhk@#&^_jhd632_@&";
            string hashedPassword = SecurityService.GenerateHashedPassword(password1);
            bool compared = SecurityService.CompareHashedPassword(password2, hashedPassword);

            // Then
            Assert.NotNull(hashedPassword);
            Assert.Equal(false, compared);
        }

        [Fact]
        [Trait("test", "unit")]
        public void ReturnExceptionGivenNotComplexPassword()
        {
            // When
            string password1 = "9284sd1fhk@#&^_jhd632_@&";
            
            // Then
            Exception ex = Assert.Throws<ArgumentException>(() => SecurityService.GenerateHashedPassword(password1));
            Assert.Contains("Weak Password - 0", ex.Message);
            Assert.Contains("Parameter name: password", ex.Message);
        }

        [Fact]
        [Trait("test", "unit")]
        public void ReturnSuccessGivenComplexPassword()
        {
            // When
            string password1 = "9284sdA1fhk@#&^_jhd632_@&";

            // Then
            Exception ex = Record.Exception(() => SecurityService.GenerateHashedPassword(password1));
            Assert.Equal(null, ex);
        }

        [Fact]
        [Trait("test", "unit")]
        public void ReturnExceptionGivenShortPassword()
        {
            // When
            string password1 = "9sA1fh";

            // Then
            Exception ex = Assert.Throws<ArgumentException>(() => SecurityService.GenerateHashedPassword(password1));
            Assert.Contains("Weak Password - 0", ex.Message);
            Assert.Contains("Parameter name: password", ex.Message);
        }

        [Fact]
        [Trait("test", "unit")]
        public void SuccessValidateUser()
        {
            // When
            string email = "useradmin@glowny-shop.com";
            string password = "P@ssw0rd";

            var result = _securityService.ValidateAdminUser(email, password);
            Assert.Equal(true, result);
        }

        [Fact]
        [Trait("test", "unit")]
        public void SuccessValidateUserGivenEmailWithDifferentCase()
        {
            // When
            string email = "UserAdmin@Glowny-Shop.com";
            string password = "P@ssw0rd";

            var result = _securityService.ValidateAdminUser(email, password);
            Assert.Equal(true, result);
        }

        [Fact]
        [Trait("test", "unit")]
        public void FailedValidateUserGivenWrongEmail()
        {
            // When
            string email = "useradminwew@glowny-shop.com";
            string password = "P@ssw0rd";

            var result = _securityService.ValidateAdminUser(email, password);
            Assert.Equal(false, result);
        }


        [Fact]
        [Trait("test", "unit")]
        public void FailedValidateUserGivenDifferentPassword()
        {
            // When
            string email = "useradmin@glowny-shop.com";
            string password = "P@ssw0rd1";

            var result = _securityService.ValidateAdminUser(email, password);
            Assert.Equal(false, result);
        }

        [Fact]
        [Trait("test", "unit")]
        public void FailedValidateUserGivenDifferentCasePassword()
        {
            // When
            string email = "useradmin@glowny-shop.com";
            string password = "P@ssw0rD";

            var result = _securityService.ValidateAdminUser(email, password);
            Assert.Equal(false, result);
        }
    }
}
