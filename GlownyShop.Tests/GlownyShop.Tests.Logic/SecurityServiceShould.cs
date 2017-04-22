using GlownyShop.Core.Logic;
using System;
using Xunit;

namespace GlownyShop.Tests.Logic
{
    public class SecurityServiceShould
    {
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
    }
}
