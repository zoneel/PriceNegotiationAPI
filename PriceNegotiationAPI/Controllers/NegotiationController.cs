using MediatR;
using Microsoft.AspNetCore.Mvc;
using PriceNegotiationAPI.Application.Negotiation.Command.Accept;
using PriceNegotiationAPI.Application.Negotiation.Command.Create;
using PriceNegotiationAPI.Application.Negotiation.Command.Decline;
using PriceNegotiationAPI.Application.Negotiation.Dto;

namespace PriceNegotiationAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NegotiationController : ControllerBase
{
    private readonly IMediator _mediator;

    public NegotiationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> CreateNegotiation([FromBody] NegotiationDto negotiation)
    {
        await _mediator.Send(new CreateNegotiationCommand(negotiation));
        return Ok(new { Status = "Negotiation Created" });
    }

    [HttpPut("{negotiationId}/accept")]
    public async Task<IActionResult> AcceptNegotiation([FromRoute]int negotiationId)
    {
        await _mediator.Send(new AcceptNegotiationCommand(negotiationId));
        return Ok(new { Status = "Negotiation accepted" });
    }

    [HttpPut("{negotiationId}/reject")]
    public async Task<IActionResult> RejectNegotiation([FromRoute]int negotiationId)
    {
        await _mediator.Send(new DeclineNegotiationCommand(negotiationId));
        return Ok(new { Status = "Negotiation rejected" });
    }
}