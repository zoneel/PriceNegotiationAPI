
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PriceNegotiationAPI.Application.Product.Command.Add;
using PriceNegotiationAPI.Application.Product.Command.Delete;
using PriceNegotiationAPI.Application.Product.Dto;
using PriceNegotiationAPI.Application.Product.Query.Get;

namespace PriceNegotiationAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductDto product)
    {
        await _mediator.Send(new AddProductCommand(product));
        return Ok(new { Status = "Product Created" });
    }

    [HttpGet]
    public async Task<List<ShowProductDto>> ShowProducts()
    {
        await _mediator.Send(new GetProductsQuery());
        return new List<ShowProductDto>();
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteProduct([FromRoute]int productId)
    {
        await _mediator.Send(new DeleteProductCommand(productId));
        return Ok(new { Status = "Product deleted" });
    }
}