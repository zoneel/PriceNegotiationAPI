using FluentValidation;
using FluentValidation.TestHelper;
using PriceNegotiationAPI.Application.Negotiation.Dto;
using PriceNegotiationAPI.Application.Validators;

namespace ApplicationTests.ValidatorsTests;

public class AddNegotiationDtoValidatorTests
{
    private readonly AddNegotiationDtoValidator _validator;

    public AddNegotiationDtoValidatorTests()
    {
        _validator = new AddNegotiationDtoValidator();
    }
    
    [Fact]
    public void ProductId_ShouldHaveErrorMessage_WhenEmpty()
    {
        // Arrange & Act
        var result = _validator.TestValidate(new AddNegotiationDto { ProductId = 0 }); // Setting empty ProductId

        // Assert
        result.ShouldHaveValidationErrorFor(dto => dto.ProductId)
            .WithErrorMessage("Product Id is required");
    }

    [Fact]
    public void ProposedPrice_ShouldHaveErrorMessage_WhenEmpty()
    {
        // Arrange & Act
        var result = _validator.TestValidate(new AddNegotiationDto
            { ProposedPrice = 0 }); // Setting empty ProposedPrice

        // Assert
        result.ShouldHaveValidationErrorFor(dto => dto.ProposedPrice)
            .WithErrorMessage("Proposed Price is required");
    }

    [Fact]
    public void ValidDto_ShouldPassValidation()
    {
        // Arrange & Act
        var result = _validator.TestValidate(new AddNegotiationDto
        {
            ProductId = 1,
            ProposedPrice = 100.0m
        }); // Providing valid data

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}

