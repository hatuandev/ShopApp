using ProductAPI.Application.Common.Mappings;

namespace ProductAPI.Application.ProductTypes.Queries.GetProductTypeAll;

public record GetProductTypeAllQuery : IRequest<IEnumerable<GetProductTypeAllDto>>;

public class GetProductTypeAllQueryHandler : IRequestHandler<GetProductTypeAllQuery, IEnumerable<GetProductTypeAllDto>>
{
    private readonly IGenericRepository<ProductType> _context;
    private readonly IMapper _mapper;

    public GetProductTypeAllQueryHandler(IGenericRepository<ProductType> context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetProductTypeAllDto>> Handle(GetProductTypeAllQuery request, CancellationToken cancellationToken)
    {
        return await _context.GetAll().ProjectToListAsync<GetProductTypeAllDto>(_mapper.ConfigurationProvider);
    }
}

