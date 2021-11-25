using System;
using System.Collections.Generic;

namespace Foosball.Microservice.DomainLogic.ValueObjects
{
    public class TeamName : ValueObject
    {
        private TeamName(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public static TeamName Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"Argument {nameof(name)} cannot be null or empty string", nameof(name));
            }

            return new TeamName(name);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}
