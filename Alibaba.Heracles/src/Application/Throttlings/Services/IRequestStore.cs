using System.Threading.Tasks;
using Alibaba.Heracles.Domain.Enums;

namespace Alibaba.Heracles.Application.Throttlings.Services
{
    public interface IRequestStore
    {
        bool CanAccess(string pattern, string clientAddress, int maxRequests, LimitUnit unit);
    }
}