using Alibaba.Heracles.Domain.ValueObjects;

namespace Alibaba.Heracles.Domain.Entities
{
    public class ThrottlingEntity
    {
        public ThrottlingEntity()
        {
        }

        public ThrottlingEntity(string pattern, Limit limit)
        {
            Pattern = pattern;
            Limit = limit;
        }

        public ThrottlingEntity(int id, string pattern, Limit limit)
        {
            Id = id;
            Pattern = pattern;
            Limit = limit;
        }

        public int Id { get; protected set; }
        public string Pattern { get; set; }
        public Limit Limit { get; set; }
    }
}