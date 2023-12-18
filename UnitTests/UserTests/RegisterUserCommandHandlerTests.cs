using NSubstitute;
using PriceNegotiationAPI.Application.User.Command.Register;
using PriceNegotiationAPI.Application.User.Dto;
using PriceNegotiationAPI.Domain.Entities;
using PriceNegotiationAPI.Domain.Repository;
using PriceNegotiationAPI.Domain.Security;

namespace ApplicationTests.UserTests;

public class RegisterUserCommandHandlerTests
{
    [Fact]
    public async Task Handle_Adds_New_User_With_Hashed_Password()
    {
        // Arrange
        var userRepository = Substitute.For<IUserRepository>();
        var passwordManager = Substitute.For<IPasswordManager>();

        var handler = new RegisterUserCommandHandler(userRepository, passwordManager);

        var userDto = new RegisterUserDto()
        {
            Name = "Test User",
            Email = "test@example.com",
            Password = "password",
            Role = 0
        };

        var hashedPassword = "hashedPassword"; // Adjust this based on the password manager's hashing

        passwordManager.Secure(userDto.Password).Returns(hashedPassword);

        var command = new RegisterUserCommand(userDto);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        await userRepository.Received(1).AddUserAsync(Arg.Is<User>(
            u => u.Name == userDto.Name &&
                 u.Email == userDto.Email &&
                 u.Password == hashedPassword &&
                 u.Role == userDto.Role));
    }
}