using Alibaba.Heracles.Domain.Exceptions;
using Alibaba.Heracles.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace Alibaba.Heracles.Domain.UnitTests.ValueObjects
{
    public class LimitTests
    {
        [TestCase("12/seC")]
        [TestCase("12/mIn")]
        [TestCase("12/HR")]
        public void ShouldParseCorrectly(string text)
        {
            Assert.AreEqual(Limit.FromString(text).ToString().ToLowerInvariant(),text.ToLowerInvariant());
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("12sec")]
        [TestCase("q/sec")]
        [TestCase("12/seasdfc")]
        public void ShouldThrowExceptionOnInvalidData(string text)
        {
            Assert.Throws<InvalidLimitStringException>(() => Limit.FromString(text));
        }

    }
}