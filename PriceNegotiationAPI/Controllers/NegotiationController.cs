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

namespace PriceNegotiationAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class NegotiationController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<AddNegotiationDto> _negotiationValidator;

    public NegotiationController(IMediator mediator, IValidator<AddNegotiationDto> negotiationValidator)
    {
        _mediator = mediator;
        _negotiationValidator = negotiationValidator;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateNegotiation([FromBody] AddNegotiationDto addNegotiation)
    {
        var userId = Convert.ToInt32(HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value);
        var validationResult = await _negotiationValidator.ValidateAsync(addNegotiation);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(error => error.ErrorMessage);
            return BadRequest(errors);
        }
        
        await _mediator.Send(new CreateNegotiationCommand(addNegotiation, userId));
        return Ok(new { Status = "Negotiation Created" });
    }

    [Authorize(Policy = "EmployeeOnlyPolicy")]
    [HttpPut("{negotiationId}/accept")]
    public async Task<IActionResult> AcceptNegotiation([FromRoute]int negotiationId)
    {
        await _mediator.Send(new AcceptNegotiationCommand(negotiationId));
        return Ok(new { Status = "Negotiation accepted" });
    }

    [Authorize(Policy = "EmployeeOnlyPolicy")]
    [HttpPut("{negotiationId}/reject")]
    public async Task<IActionResult> RejectNegotiation([FromRoute]int negotiationId)
    {
        await _mediator.Send(new DeclineNegotiationCommand(negotiationId));
        return Ok(new { Status = "Negotiation rejected" });
    }
    
    [Authorize(Policy = "EmployeeOnlyPolicy")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllNegotiations()
    {
        var negotiations = await _mediator.Send(new GetAllNegotiationsQuery());
        return Ok(negotiations);
    }
    
    [Authorize(Policy = "EmployeeOnlyPolicy")]
    [HttpGet("pending")]
    public async Task<IActionResult> GetAllPendingNegotiations()
    {
        var negotiations = await _mediator.Send(new GetPendingNegotiationsQuery());
        return Ok(negotiations);
    }
}