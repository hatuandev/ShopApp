using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using ProductAPI.Application.Products.Queries.GetProductWithPagination;
using ProductAPI.Domain.Entities;

namespace ProductAPI.Application.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(int Id) : IRequest<GetProductByIdDto>;

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdDto>
    {
        private readonly IGenericRepository<Product> _context;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;

        public GetProductByIdQueryHandler(IGenericRepository<Product> context, IMapper mapper, IDistributedCache distributedCache)
        {
            _context = context;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }

        public async Task<GetProductByIdDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.GetByIdAsync(request.Id);
            Guard.Against.NotFound(request.Id, entity);

            string? cacheProductId = await _distributedCache.GetStringAsync("GetProductById", cancellationToken);

            GetProductByIdDto? product;
            if (string.IsNullOrEmpty(cacheProductId))
            {
                product = _mapper.Map<GetProductByIdDto>(entity);
                await _distributedCache.SetStringAsync("GetProductById", JsonConvert.SerializeObject(product), cancellationToken);
                return product;
            }

            product = JsonConvert.DeserializeObject<GetProductByIdDto>(cacheProductId);
            return product;
        }
    }
}
