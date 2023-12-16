using PriceNegotiationAPI.Application.Product.Dto;
    
namespace PriceNegotiationAPI.Application.Product.Mapping;

public class ProductMapping
{
    public static Domain.Entities.Product MapProductDtoToProductEntity(ProductDto productDto)
    {
        var product = new Domain.Entities.Product
        {
            ProductId = productDto.ProductId,
            Name = productDto.Name,
            BasePrice = productDto.BasePrice
        };
        return product;
    }
    
    public static Domain.Entities.Product MapAddProductDtoToProductEntity(AddProductDto productDto)
    {
        var product = new Domain.Entities.Product
        {
            Name = productDto.Name,
            BasePrice = productDto.BasePrice
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
                ProductId = product.ProductId,
                Name = product.Name,
                BasePrice = product.BasePrice
            };
            productDtos.Add(productDto);
        }
        return productDtos;
    }

}