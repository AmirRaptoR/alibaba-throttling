using Alibaba.Heracles.Application.Common.Interfaces;
using System;

namespace Alibaba.Heracles.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
    
}