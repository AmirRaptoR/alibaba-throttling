using System.Threading;
using System.Threading.Tasks;
using Alibaba.Heracles.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alibaba.Heracles.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ThrottlingEntity> Throttlings { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}