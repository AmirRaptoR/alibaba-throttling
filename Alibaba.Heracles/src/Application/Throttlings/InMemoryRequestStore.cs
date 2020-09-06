using System;
using System.Collections.Generic;
using System.Linq;
using Alibaba.Heracles.Application.Common.Interfaces;
using Alibaba.Heracles.Application.Throttlings.Services;
using Alibaba.Heracles.Domain.Enums;

namespace Alibaba.Heracles.Application.Throttlings
{
    public class InMemoryRequestStore : IRequestStore
    {
        class RequestKey
        {
            public RequestKey(string pattern, string clientAddress)
            {
                if (string.IsNullOrWhiteSpace(pattern) || string.IsNullOrWhiteSpace(clientAddress))
                {
                    throw new ArgumentException("pattern and client address must be provided");
                }

                Pattern = pattern;
                ClientAddress = clientAddress;
            }

            public string Pattern { get; }
            public string ClientAddress { get; }


            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((RequestKey) obj);
            }

            protected bool Equals(RequestKey other)
            {
                return Pattern == other.Pattern && ClientAddress == other.ClientAddress;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Pattern, ClientAddress);
            }

            public override string ToString()
            {
                return $"{Pattern}:{ClientAddress}";
            }
        }

        private readonly IDictionary<RequestKey, List<DateTime>> _requests =
            new Dictionary<RequestKey, List<DateTime>>();

        private readonly IDateTime _dateTime;

        public InMemoryRequestStore(IDateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public bool CanAccess(string pattern, string clientAddress, int maxRequests, LimitUnit unit)
        {
            CleanUpExpiredData();

            var requestKey = new RequestKey(pattern, clientAddress);
            if (!_requests.ContainsKey(requestKey))
            {
                _requests[requestKey] = new List<DateTime>();
            }

            var fromTime = unit switch
            {
                LimitUnit.Sec => _dateTime.Now.AddSeconds(-1),
                LimitUnit.Min => _dateTime.Now.AddMinutes(-1),
                LimitUnit.Hr => _dateTime.Now.AddHours(-1),
                _ => throw new ArgumentOutOfRangeException(nameof(unit), unit, null)
            };

            var totalRequestFromTime = _requests[requestKey].Count(x => x >= fromTime);
            // if an api must be throttled not need to add request and refresh timer
            if (totalRequestFromTime >= maxRequests) return false;
            _requests[requestKey].Add(_dateTime.Now);
            return true;
        }

        private void CleanUpExpiredData()
        {
            var expirationTime = _dateTime.Now.AddMinutes(-60);
            foreach (var requestsValue in _requests.Values)
            {
                requestsValue.RemoveAll(x => x < expirationTime);
            }
        }
    }
}