using Alibaba.Heracles.Domain.Common;
using Alibaba.Heracles.Domain.Exceptions;
using System;
using System.Collections.Generic;
using Alibaba.Heracles.Domain.Enums;

namespace Alibaba.Heracles.Domain.ValueObjects
{
    public class Limit : ValueObject
    {
        public Limit(int value, LimitUnit unit)
        {
            this.Value = value;
            this.Unit = unit;
        }

        public static Limit FromString(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new InvalidLimitStringException("String value can not be empty");
            }

            if (!str.Contains("/", StringComparison.Ordinal))
            {
                throw new InvalidLimitStringException("String must contains [/] character");
            }

            var parts = str.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
            {
                throw new InvalidLimitStringException("Must in format [{number}/{time fraction}]");
            }

            if (!int.TryParse(parts[0], out var value))
            {
                throw new InvalidLimitStringException(
                    "Must in format [{number}/{time fraction}]. Number format is invalid");
            }

            if (!Enum.TryParse(typeof(LimitUnit), parts[1], true, out var unit) ||
                unit == null)
            {
                throw new InvalidLimitStringException(
                    "Must in format [{number}/{time fraction}]. Time fractions can be [sec/min/hr]");
            }

            return new Limit(value, (LimitUnit) unit);
        }

        public int Value { get; }
        public LimitUnit Unit { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
            yield return Unit;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Limit))
            {
                return false;
            }

            return this.Equals((Limit) obj);
        }

        protected bool Equals(Limit other) => this.Value == other?.Value && this.Unit == other.Unit;

        public override int GetHashCode() => HashCode.Combine(this.Value, (int) this.Unit);

        public override string ToString() => $"{this.Value}/{this.Unit}";
    }
}