using AutoMapper;
using Alibaba.Heracles.Application.Common.Mappings;
using Alibaba.Heracles.Domain.Entities;
using NUnit.Framework;
using System;
using Alibaba.Heracles.Application.Throttlings.Queries.GetSingle;

namespace Alibaba.Heracles.Application.UnitTests.Common.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });

            _mapper = _configuration.CreateMapper();
        }

        [Test]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Test]
        [TestCase(typeof(ThrottlingEntity), typeof(ThrottlingDto))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = Activator.CreateInstance(source);

            _mapper.Map(instance, source, destination);
        }
    }
}