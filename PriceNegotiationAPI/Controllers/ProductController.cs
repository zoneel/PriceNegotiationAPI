using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using PriceNegotiationAPI.Application.Product.Command.Add;
using PriceNegotiationAPI.Application.Product.Command.Delete;
using PriceNegotiationAPI.Application.Product.Dto;
using PriceNegotiationAPI.Application.Product.Query.Get;
using PriceNegotiationAPI.Application.Validators;

namespace PriceNegotiationAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<AddProductDto> _productValidator;
    private readonly IValidator<int> _idValidator;

    public ProductController(IMediator mediator, IValidator<AddProductDto> productValidator, IValidator<int> idValidator)
    {
        _mediator = mediator;
        _productValidator = productValidator;
        _idValidator = idValidator;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] AddProductDto product)
    {
        var validationResult = await _productValidator.ValidateAsync(product);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(error => error.ErrorMessage);
            return BadRequest(errors);
        }
        
        await _mediator.Send(new AddProductCommand(product));
        return Ok(new { Status = "Product Created" });
    }

    [HttpGet]
    public async Task<IEnumerable<ProductDto>> ShowProducts()
    {
        var products = await _mediator.Send(new GetProductsQuery());
        return products;
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteProduct([FromRoute]int productId)
    {
        var validationResult = await _idValidator.ValidateAsync(productId);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(error => error.ErrorMessage);
            return BadRequest(errors);
        }
        
        await _mediator.Send(new DeleteProductCommand(productId));
        return Ok(new { Status = "Product deleted" });
    }
}