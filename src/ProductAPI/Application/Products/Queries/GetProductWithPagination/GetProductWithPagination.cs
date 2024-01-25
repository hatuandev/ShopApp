using AutoMapper.QueryableExtensions;
using ProductAPI.Application.Common.Mappings;

namespace ProductAPI.Application.Products.Queries.GetProductWithPagination;

public record GetProductWithPaginationQuery : IRequest<PaginatedList<GetProductWithPaginationDto>>
{
    public int number { get; init; } = 1;
    public int size { get; init; } = 10;
}

public class GetProductWithPaginationQueryHandler : IRequestHandler<GetProductWithPaginationQuery, PaginatedList<GetProductWithPaginationDto>>
{
    private readonly IGenericRepository<Product> _context;
    private readonly IMapper _mapper;

    public GetProductWithPaginationQueryHandler(IGenericRepository<Product> context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<GetProductWithPaginationDto>> Handle(GetProductWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.GetAll()
            .ProjectTo<GetProductWithPaginationDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.number, request.size);
    }
}