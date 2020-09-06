using Alibaba.Heracles.Application.Common.Behaviours;
using Alibaba.Heracles.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using Alibaba.Heracles.Application.Throttlings.Commands.Create;

namespace Alibaba.Heracles.Application.UnitTests.Common.Behaviours
{
    public class RequestLoggerTests
    {
        private readonly Mock<ILogger<CreateThrottlingCommand>> _logger;


        public RequestLoggerTests()
        {
            _logger = new Mock<ILogger<CreateThrottlingCommand>>();
        }

        [Test]
        public async Task ShouldLogInformation()
        {
            var requestLogger = new LoggingBehaviour<CreateThrottlingCommand>(_logger.Object);

            await requestLogger.Process(new CreateThrottlingCommand(),
                new CancellationToken());

            _logger.Verify(i =>
                i.Log(LogLevel.Information, It.IsAny<EventId>(), It.IsAny<string>()), Times.Once);
        }

    }
}