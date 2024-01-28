using ProductAPI.Application.Products.Queries.GetProductWithPagination;

namespace ProductAPI.Application.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(int Id) : IRequest<GetProductByIdDto>;

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdDto>
    {
        private readonly IGenericRepository<Product> _context;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IGenericRepository<Product> context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetProductByIdDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.GetByIdAsync(request.Id);
            Guard.Against.NotFound(request.Id, entity);

            var product = _mapper.Map<GetProductByIdDto>(entity);
            return product;
        }
    }
}
