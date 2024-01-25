namespace ProductAPI.Application.Products.Queries.GetProductWithPagination;

public class GetProductWithPaginationDto
{
    public int Id { get; init; }
    public string? Name { get; init; }

    public string? Code { get; init; }

    public decimal? Cost { get; init; }

    public ProductType? ProductType { get; init; }

    public string? Description { get; init; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, GetProductWithPaginationDto>();
        }
    }
}
