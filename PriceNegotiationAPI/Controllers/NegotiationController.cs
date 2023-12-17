using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using PriceNegotiationAPI.Application.Negotiation.Command.Accept;
using PriceNegotiationAPI.Application.Negotiation.Command.Create;
using PriceNegotiationAPI.Application.Negotiation.Command.Decline;
using PriceNegotiationAPI.Application.Negotiation.Dto;
using PriceNegotiationAPI.Application.Negotiation.Query.GetAll;
using PriceNegotiationAPI.Application.Negotiation.Query.GetPending;
using PriceNegotiationAPI.Domain.Security;

namespace PriceNegotiationAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class NegotiationController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<AddNegotiationDto> _negotiationValidator;
    private readonly IUserIdentity _userIdentity;

    public NegotiationController(IMediator mediator, IValidator<AddNegotiationDto> negotiationValidator, IUserIdentity userIdentity)
    {
        _mediator = mediator;
        _negotiationValidator = negotiationValidator;
        _userIdentity = userIdentity;
    }
    
    /// <summary>
    /// Creates a new negotiation.
    /// </summary>
    /// <param name="addNegotiation">dto for adding new negotiation</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateNegotiation([FromBody] AddNegotiationDto addNegotiation)
    {
        var userId = _userIdentity.GetUserId(HttpContext);
        
        var validationResult = await _negotiationValidator.ValidateAsync(addNegotiation);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(error => error.ErrorMessage);
            return BadRequest(errors);
        }
        
        await _mediator.Send(new CreateNegotiationCommand(addNegotiation, userId));
        return Ok(new { Status = "Negotiation Created" });
    }

    /// <summary>
    /// Accepts a negotiation.
    /// </summary>
    /// <param name="negotiationId">negotiationId of negotiation to accept.</param>
    /// <returns></returns>
    [Authorize(Policy = "EmployeeOnlyPolicy")]
    [HttpPut("{negotiationId}/accept")]
    public async Task<IActionResult> AcceptNegotiation([FromRoute]int negotiationId)
    {
        await _mediator.Send(new AcceptNegotiationCommand(negotiationId));
        return Ok(new { Status = "Negotiation accepted" });
    }

    /// <summary>
    /// Rejects a negotiation.
    /// </summary>
    /// <param name="negotiationId">negotiationId of negotiation to reject.</param>
    /// <returns></returns>
    [Authorize(Policy = "EmployeeOnlyPolicy")]
    [HttpPut("{negotiationId}/reject")]
    public async Task<IActionResult> RejectNegotiation([FromRoute]int negotiationId)
    {
        await _mediator.Send(new DeclineNegotiationCommand(negotiationId));
        return Ok(new { Status = "Negotiation rejected" });
    }
    
    /// <summary>
    /// Get all negotiations
    /// </summary>
    /// <returns></returns>
    [Authorize(Policy = "EmployeeOnlyPolicy")]
    [HttpGet("all")]
    public async Task<List<ShowNegotiationDto>> GetAllNegotiations()
    {
        var negotiations = await _mediator.Send(new GetAllNegotiationsQuery());
        return negotiations;
    }
    
    /// <summary>
    /// Get all pending negotiations.
    /// </summary>
    /// <returns></returns>
    [Authorize(Policy = "EmployeeOnlyPolicy")]
    [HttpGet("pending")]
    public async Task<List<ShowNegotiationDto>> GetAllPendingNegotiations()
    {
        var negotiations = await _mediator.Send(new GetPendingNegotiationsQuery());
        return negotiations;
    }
}