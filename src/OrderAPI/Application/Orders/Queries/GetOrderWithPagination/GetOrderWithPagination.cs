namespace OrderAPI.Application.Orders.Queries.GetOrderWithPagination;

public class GetOrderWithPaginationQuery : IRequest<PaginatedList<GetOrderWithPaginationDto>>
{
    public int number { get; init; } = 1;
    public int size { get; init; } = 10;
}

public class GetProductWithPaginationQueryHandler : IRequestHandler<GetOrderWithPaginationQuery, PaginatedList<GetOrderWithPaginationDto>>
{
    private readonly IGenericRepository<Order> _context;
    private readonly IMapper _mapper;

    public GetProductWithPaginationQueryHandler(IGenericRepository<Order> context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<GetOrderWithPaginationDto>> Handle(GetOrderWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.GetAll()
            .ProjectTo<GetOrderWithPaginationDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.number, request.size);
    }
}