using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.Extensions.DependencyInjection;
using PriceNegotiationAPI.Application.User.Dto;
using PriceNegotiationAPI.Application.Validators;
using PriceNegotiationAPI.Domain.Entities;
using PriceNegotiationAPI.Domain.Repository;
using PriceNegotiationAPI.Infrastructure.Repository;

namespace ApplicationTests.ValidatorsTests;

public class LoginUserDtoValidatorTests
{
    [Fact]
    public void Email_Should_Not_Be_Empty()
    {
        // Arrange
        var validator = new LoginUserDtoValidator();
        var dto = new LoginUserDto { Email = "" }; // Empty email

        // Act
        var result = validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Email_Should_Be_Valid()
    {
        // Arrange
        var validator = new LoginUserDtoValidator();
        var dto = new LoginUserDto { Email = "invalidemail" }; // Invalid email format

        // Act
        var result = validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Password_Should_Not_Be_Empty()
    {
        // Arrange
        var validator = new LoginUserDtoValidator();
        var dto = new LoginUserDto { Password = "" }; // Empty password

        // Act
        var result = validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void Valid_LoginUserDto_Should_Pass_Validation()
    {
        // Arrange
        var validator = new LoginUserDtoValidator();
        var dto = new LoginUserDto { Email = "valid@email.com", Password = "password" }; // Valid data

        // Act
        var result = validator.TestValidate(dto);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}