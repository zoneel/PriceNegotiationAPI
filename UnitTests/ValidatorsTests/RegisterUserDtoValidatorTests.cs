using FluentAssertions;
using FluentValidation.TestHelper;
using NSubstitute;
using PriceNegotiationAPI.Application.User.Dto;
using PriceNegotiationAPI.Application.Validators;
using PriceNegotiationAPI.Domain.Entities;
using PriceNegotiationAPI.Domain.Repository;
using Xunit;

namespace PriceNegotiationAPI.Tests.Application.Validators
{
    public class RegisterUserDtoValidatorTests
    {
        private readonly IUserRepository _userRepository;
        private readonly RegisterUserDtoValidator _validator;

        public RegisterUserDtoValidatorTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _validator = new RegisterUserDtoValidator(_userRepository);
        }

        [Fact]
        public async void Should_Have_Error_When_Name_Is_Empty()
        {
            // Arrange
            var dto = new RegisterUserDto { Name = "" };

            // Act
            var result = await _validator.TestValidateAsync(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public async void Should_Have_Error_When_Email_Is_Invalid()
        {
            // Arrange
            var dto = new RegisterUserDto { Email = "invalidemail" };

            // Act
            var result = await _validator.TestValidateAsync(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }


        [Fact]
        public async Task Should_Have_Error_When_Email_Exists()
        {
            // Arrange
            var dto = new RegisterUserDto { Email = "existingemail@example.com" };
            _userRepository.GetUserByEmailAsync(dto.Email).Returns(new User()); // Simulating existing user

            // Act
            var result = await _validator.ValidateAsync(dto);

            // Assert
            result.Errors.Should().ContainSingle(e => e.ErrorMessage == "Email already exists. Please use a different email.");
        }

        [Fact]
        public async Task Should_Have_Error_When_Name_Exists()
        {
            // Arrange
            var dto = new RegisterUserDto { Name = "existingname" };
            _userRepository.GetUserByNameAsync(dto.Name).Returns(new User()); // Simulating existing user

            // Act
            var result = await _validator.ValidateAsync(dto);

            // Assert
            result.Errors.Should().ContainSingle(e => e.ErrorMessage == "Name already exists. Please use a different name.");
        }
    }
}
