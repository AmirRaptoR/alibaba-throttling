using Alibaba.Heracles.Application.Common.Mappings;
using Alibaba.Heracles.Domain.Entities;
using AutoMapper;

namespace Alibaba.Heracles.Application.Throttlings.Queries.GetSingle
{
    public class ThrottlingDto : IMapFrom<ThrottlingEntity>
    {
        public int Id { get; set; }
        public string Pattern { get; set; }
        public string LimitString { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ThrottlingEntity, ThrottlingDto>()
                .ForMember(x => x.Id, expression => expression.MapFrom(x => x.Id))
                .ForMember(x => x.LimitString, expression => expression.MapFrom(x => x.Limit.ToString()))
                .ForMember(x => x.Pattern, expression => expression.MapFrom(x => x.Pattern));
        }
    }
}