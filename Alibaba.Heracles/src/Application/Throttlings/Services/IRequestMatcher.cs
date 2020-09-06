using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Alibaba.Heracles.Application.Throttlings.Services
{
    public interface IRequestMatcher
    {
        bool IsMatched(string url, string pattern);
    }

    public class RegexRequestMatcher : IRequestMatcher
    {
        public bool IsMatched(string url, string pattern)
        {
            return Regex.IsMatch(url, pattern);
        }
    }
}