using System.Threading;
using System.Threading.Tasks;
using Alibaba.Heracles.Application.Common.Interfaces;
using Alibaba.Heracles.Domain.Entities;
using Alibaba.Heracles.Domain.ValueObjects;
using MediatR;

namespace Alibaba.Heracles.Application.Throttlings.Commands.Create
{
    public class CreateThrottlingCommand : IRequest<int>
    {
        public string Pattern { get; set; }

        public string LimitString { get; set; }
    }

    public class CreateTodoItemCommandHandler : IRequestHandler<CreateThrottlingCommand, int>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateTodoItemCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(CreateThrottlingCommand request, CancellationToken cancellationToken)
        {
            var entity = new ThrottlingEntity(request.Pattern, Limit.FromString(request.LimitString));
            await _dbContext.Throttlings.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}