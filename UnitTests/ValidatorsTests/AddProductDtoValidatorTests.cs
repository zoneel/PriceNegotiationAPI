using FluentValidation.TestHelper;
using PriceNegotiationAPI.Application.Product.Dto;
using PriceNegotiationAPI.Application.Validators;

namespace ApplicationTests.ValidatorsTests;

public class AddProductDtoValidatorTests
{
    private readonly AddProductDtoValidator _validator;

    public AddProductDtoValidatorTests()
    {
        _validator = new AddProductDtoValidator();
    }

    [Fact]
    public void Name_ShouldHaveErrorMessage_WhenEmpty()
    {
        // Arrange & Act
        var result = _validator.TestValidate(new AddProductDto { Name = "" }); // Setting empty Name

        // Assert
        result.ShouldHaveValidationErrorFor(dto => dto.Name)
            .WithErrorMessage("'Name' must not be empty.");
    }

    [Fact]
    public void Name_ShouldHaveErrorMessage_WhenExceedsMaxLength()
    {
        // Arrange & Act
        var result = _validator.TestValidate(new AddProductDto { Name = new string('A', 51) }); // Setting Name exceeding maximum length

        // Assert
        result.ShouldHaveValidationErrorFor(dto => dto.Name)
            .WithErrorMessage("The length of 'Name' must be 50 characters or fewer. You entered 51 characters.");
    }

    [Fact]
    public void BasePrice_ShouldHaveErrorMessage_WhenEmpty()
    {
        // Arrange & Act
        var result = _validator.TestValidate(new AddProductDto { BasePrice = 0 }); // Setting empty BasePrice

        // Assert
        result.ShouldHaveValidationErrorFor(dto => dto.BasePrice)
            .WithErrorMessage("'Base Price' must not be empty.");
    }

    [Fact]
    public void ValidDto_ShouldPassValidation()
    {
        // Arrange & Act
        var result = _validator.TestValidate(new AddProductDto
        {
            Name = "Valid Product",
            BasePrice = 100.0m
        }); // Providing valid data

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}