using System.Threading;
using System.Threading.Tasks;
using Alibaba.Heracles.Application.Common.Exceptions;
using Alibaba.Heracles.Application.Common.Interfaces;
using Alibaba.Heracles.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alibaba.Heracles.Application.Throttlings.Commands.Delete
{
    public class DeleteThrottlingCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteThrottlingCommandHandler : IRequestHandler<DeleteThrottlingCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteThrottlingCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteThrottlingCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Throttlings.SingleOrDefaultAsync(x => x.Id.Equals(request.Id),
                cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ThrottlingEntity), request.Id);
            }

            _dbContext.Throttlings.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}