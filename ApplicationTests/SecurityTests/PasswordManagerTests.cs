using Microsoft.AspNetCore.Identity;
using PriceNegotiationAPI.Application.Security;
using PriceNegotiationAPI.Domain.Entities;

namespace ApplicationTests.SecurityTests;

public class PasswordManagerTests
{
    [Fact]
    public void Secure_Returns_NonEmpty_Hashed_Password()
    {
        // Arrange
        var password = "TestPassword";
        var passwordHasher = new PasswordHasher<User>();
        var passwordManager = new PasswordManager(passwordHasher);

        // Act
        var hashedPassword = passwordManager.Secure(password);

        // Assert
        Assert.NotNull(hashedPassword);
        Assert.NotEmpty(hashedPassword);
    }

    [Fact]
    public void ValidateAsync_Returns_True_For_Valid_Password()
    {
        // Arrange
        var password = "TestPassword";
        var passwordHasher = new PasswordHasher<User>();
        var passwordManager = new PasswordManager(passwordHasher);
        var hashedPassword = passwordManager.Secure(password);

        // Act
        var result = passwordManager.ValidateAsync(password, hashedPassword, CancellationToken.None);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ValidateAsync_Returns_False_For_Invalid_Password()
    {
        // Arrange
        var originalPassword = "TestPassword";
        var invalidPassword = "InvalidPassword";
        var passwordHasher = new PasswordHasher<User>();
        var passwordManager = new PasswordManager(passwordHasher);
        var hashedPassword = passwordManager.Secure(originalPassword);

        // Act
        var result = passwordManager.ValidateAsync(invalidPassword, hashedPassword, CancellationToken.None);

        // Assert
        Assert.False(result);
    }
}