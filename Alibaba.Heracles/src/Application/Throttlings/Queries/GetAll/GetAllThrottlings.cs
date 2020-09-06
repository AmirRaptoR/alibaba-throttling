using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Alibaba.Heracles.Application.Common.Interfaces;
using Alibaba.Heracles.Application.Throttlings.Queries.GetSingle;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alibaba.Heracles.Application.Throttlings.Queries.GetAll
{
    public class GetAllThrottlings : IRequest<IEnumerable<ThrottlingDto>>
    {
        public int Id { get; set; }
    }

    public class GetAllThrottlingsCommandHandler : IRequestHandler<GetAllThrottlings, IEnumerable<ThrottlingDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllThrottlingsCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ThrottlingDto>> Handle(GetAllThrottlings request,
            CancellationToken cancellationToken)
        {
            return await _mapper.ProjectTo<ThrottlingDto>(_dbContext.Throttlings.AsNoTracking())
                .ToListAsync(cancellationToken);
        }
    }
}