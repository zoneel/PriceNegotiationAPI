using PriceNegotiationAPI.Application.Product.Dto;
using PriceNegotiationAPI.Domain.ValueObject;

namespace PriceNegotiationAPI.Application.Product.Mapping;

public class ProductMapping
{
    public static Domain.Entities.Product MapProductDtoToProductEntity(ProductDto productDto)
    {
        var product = new Domain.Entities.Product
        {
            Name = productDto.Name,
            BasePrice = new Price(productDto.BasePrice)
        };
        return product;
    }
    
    public static IEnumerable<ProductDto> MapProductsToProductDtosEntity(IEnumerable<Domain.Entities.Product> products)
    {
        var productDtos = new List<ProductDto>();
        foreach (var product in products)
        {
            var productDto = new ProductDto()
            {
                Name = product.Name,
                BasePrice = product.BasePrice.Value
            };
            productDtos.Add(productDto);
        }
        return productDtos;
    }

}