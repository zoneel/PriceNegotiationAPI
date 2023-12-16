using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PriceNegotiationAPI.Application.User.Command.Login;
using PriceNegotiationAPI.Application.User.Command.Register;
using PriceNegotiationAPI.Application.User.Dto;
using PriceNegotiationAPI.Domain.Exceptions;
using PriceNegotiationAPI.Domain.Security;

namespace PriceNegotiationAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<RegisterUserDto> _userRegistrationValidator;
    private readonly IValidator<LoginUserDto> _userLoginValidator;

    public UserController(IMediator mediator, IValidator<RegisterUserDto> userRegistrationValidator, IValidator<LoginUserDto> userLoginValidator)
    {
        _mediator = mediator;
        _userRegistrationValidator = userRegistrationValidator;
        _userLoginValidator = userLoginValidator;
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="user">Dto for registering new user.</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto user)
    {
        var validationResult = await _userRegistrationValidator.ValidateAsync(user);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(error => error.ErrorMessage);
            return BadRequest(errors);
        }

        await _mediator.Send(new RegisterUserCommand(user));
        return Ok(new { Status = "User Registered" });
    }

    /// <summary>
    /// Login user and get a JWT token.
    /// </summary>
    /// <param name="user">dto for logging in.</param>
    /// <returns>JWT token</returns>
    /// <exception cref="InvalidCredentialsException"></exception>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<JwtToken> LoginUser([FromBody] LoginUserDto user)
    {
        var validationResult = await _userLoginValidator.ValidateAsync(user);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(error => error.ErrorMessage);
            throw new InvalidCredentialsException("Invalid username or password");
        }

        var usersToken = await _mediator.Send(new LoginUserCommand(user));
        if (usersToken == null)
        {
            return new JwtToken();
        }

        return usersToken;
    }
}