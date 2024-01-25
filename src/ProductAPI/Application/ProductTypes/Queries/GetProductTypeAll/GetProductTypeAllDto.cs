namespace ProductAPI.Application.ProductTypes.Queries.GetProductTypeAll;

public class GetProductTypeAllDto
{
    public int Id { get; init; }
    public string? Type { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ProductType, GetProductTypeAllDto>();
        }
    }
}
