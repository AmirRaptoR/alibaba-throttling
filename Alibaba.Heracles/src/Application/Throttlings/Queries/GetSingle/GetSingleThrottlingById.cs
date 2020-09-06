using System.Threading;
using System.Threading.Tasks;
using Alibaba.Heracles.Application.Common.Exceptions;
using Alibaba.Heracles.Application.Common.Interfaces;
using Alibaba.Heracles.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alibaba.Heracles.Application.Throttlings.Queries.GetSingle
{
    public class GetThrottlingById : IRequest<ThrottlingDto>
    {
        public int Id { get; set; }
    }

    public class GetThrottlingByIdCommandHandler : IRequestHandler<GetThrottlingById, ThrottlingDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetThrottlingByIdCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ThrottlingDto> Handle(GetThrottlingById request, CancellationToken cancellationToken)
        {
            var entity =
                await _dbContext.Throttlings.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.Id,
                    cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(ThrottlingEntity), request.Id);
            }

            return _mapper.Map<ThrottlingDto>(entity);
        }
    }
}