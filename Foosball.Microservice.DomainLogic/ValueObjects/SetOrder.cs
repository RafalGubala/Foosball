using System;
using System.Collections.Generic;

namespace Foosball.Microservice.DomainLogic.ValueObjects
{
    public class SetOrder : ValueObject, IComparable
    {
        private SetOrder(int order)
        {
            Value = order;
        }

        public int Value { get; }

        public static SetOrder CreateFrom(int order)
        {
            if (order < 0)
            {
                throw new ArgumentException("Order cannot be < 0");
            }

            if (order > 2)
            {
                throw new ArgumentException("Order cannot be > 2");
            }

            return new SetOrder(order);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public int CompareTo(object? obj)
        {
            var setOrder = (SetOrder)obj;
            return Value - setOrder.Value;
        }
    }
}
