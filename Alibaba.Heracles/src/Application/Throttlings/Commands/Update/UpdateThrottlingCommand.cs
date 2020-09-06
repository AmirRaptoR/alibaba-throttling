using System.Threading;
using System.Threading.Tasks;
using Alibaba.Heracles.Application.Common.Exceptions;
using Alibaba.Heracles.Application.Common.Interfaces;
using Alibaba.Heracles.Domain.Entities;
using Alibaba.Heracles.Domain.Enums;
using Alibaba.Heracles.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alibaba.Heracles.Application.Throttlings.Commands.Update
{
    public class UpdateThrottlingCommand : IRequest
    {
        public int Id { get; set; }

        public string Pattern { get; set; }
        public string LimitString { get; set; }
    }

    public class UpdateThrottlingCommandHandler : IRequestHandler<UpdateThrottlingCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateThrottlingCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Unit> Handle(UpdateThrottlingCommand request, CancellationToken cancellationToken)
        {
            var entity =
                await _dbContext.Throttlings.SingleOrDefaultAsync(x => x.Id == request.Id,
                    cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ThrottlingEntity), request.Id);
            }

            entity.Pattern = request.Pattern;
            entity.Limit = Limit.FromString(request.LimitString);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}