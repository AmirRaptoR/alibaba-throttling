using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Alibaba.Heracles.Application.Common.Interfaces;
using Alibaba.Heracles.Application.Throttlings.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alibaba.Heracles.Application.Throttlings.Commands.CheckAccess
{
    public class CheckAccessCommand : IRequest
    {
        public string Url { get; set; }
        public string Ip { get; set; }
    }

    public class CheckAccessCommandHandler : IRequestHandler<CheckAccessCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IRequestStore _requestStore;
        private readonly IRequestMatcher _requestMatcher;

        public CheckAccessCommandHandler(IApplicationDbContext applicationDbContext, IRequestStore requestStore,
            IRequestMatcher requestMatcher)
        {
            _applicationDbContext = applicationDbContext;
            _requestStore = requestStore;
            _requestMatcher = requestMatcher;
        }

        public async Task<Unit> Handle(CheckAccessCommand request, CancellationToken cancellationToken)
        {
            var allThrottlings = await _applicationDbContext.Throttlings.AsNoTracking()
                .ToListAsync(cancellationToken: cancellationToken);
            var mustThrottle = allThrottlings.Where(x => _requestMatcher.IsMatched(request.Url, x.Pattern))
                .ToArray();


            if (mustThrottle.Any(x =>
                !_requestStore.CanAccess(x.Pattern, request.Ip, x.Limit.Value, x.Limit.Unit)))
            {
                throw new TooManyRequestsException();
            }

            return Unit.Value;
        }
    }
}