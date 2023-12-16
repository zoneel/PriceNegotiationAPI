using NSubstitute;
using PriceNegotiationAPI.Application.User.Command.Login;
using PriceNegotiationAPI.Application.User.Dto;
using PriceNegotiationAPI.Domain.Entities;
using PriceNegotiationAPI.Domain.Exceptions;
using PriceNegotiationAPI.Domain.Repository;
using PriceNegotiationAPI.Domain.Security;

namespace ApplicationTests.UserTests;

    public class LoginUserCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Returns_Valid_JwtToken_For_Valid_Credentials()
        {
            // Arrange
            var userRepository = Substitute.For<IUserRepository>();
            var passwordManager = Substitute.For<IPasswordManager>();
            var jwtService = Substitute.For<IJwtService>();

            var handler = new LoginUserCommandHandler(userRepository, passwordManager, jwtService);

            var userEmail = "test@example.com";
            var userPassword = "password";
            var user = new User { Email = userEmail, Password = "hashedPassword", UserId = 1, Role = 0 }; // Adjust user details as needed

            userRepository.GetUserByEmailAsync(userEmail, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(user));

            passwordManager.ValidateAsync(userPassword, user.Password, Arg.Any<CancellationToken>())
                .Returns(true);

            jwtService.CreateTokenAsync(user.UserId, user.Role)
                .Returns(new JwtToken { Token = "validToken" }); // Adjust the token as needed

            var command = new LoginUserCommand(new LoginUserDto() { Email = userEmail, Password = userPassword });

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("validToken", result.Token); // Ensure the token matches the expected valid token
        }

        [Fact]
        public async Task Handle_Throws_InvalidCredentialsException_For_Invalid_Credentials()
        {
            // Arrange
            var userRepository = Substitute.For<IUserRepository>();
            var passwordManager = Substitute.For<IPasswordManager>();
            var jwtService = Substitute.For<IJwtService>();

            var handler = new LoginUserCommandHandler(userRepository, passwordManager, jwtService);

            userRepository.GetUserByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<User>(null)); // Simulate user not found

            passwordManager.ValidateAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(false); // Simulate password validation failure

            var command = new LoginUserCommand(new LoginUserDto() { Email = "nonexistent@example.com", Password = "invalidPassword" });

            // Act & Assert
            await Assert.ThrowsAsync<InvalidCredentialsException>(() => handler.Handle(command, CancellationToken.None));
        }
    }