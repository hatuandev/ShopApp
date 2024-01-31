using AutoMapper.QueryableExtensions;
using ProductAPI.Application.Common.Mappings;

namespace ProductAPI.Application.StockPickings.Queries.GetStockPickingWithPagination;

public record GetStockPickingWithPaginationQuery : IRequest<PaginatedList<GetStockPickingWithPaginationDto>>
{
    public int number { get; init; } = 1;
    public int size { get; init; } = 10;
}

public class GetStockPickingWithPaginationQueryHandler : IRequestHandler<GetStockPickingWithPaginationQuery, PaginatedList<GetStockPickingWithPaginationDto>>
{
    private readonly IGenericRepository<StockPicking> _context;
    private readonly IMapper _mapper;

    public GetStockPickingWithPaginationQueryHandler(IGenericRepository<StockPicking> context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<GetStockPickingWithPaginationDto>> Handle(GetStockPickingWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.GetAll()
           .ProjectTo<GetStockPickingWithPaginationDto>(_mapper.ConfigurationProvider)
           .PaginatedListAsync(request.number, request.size);
    }
}
