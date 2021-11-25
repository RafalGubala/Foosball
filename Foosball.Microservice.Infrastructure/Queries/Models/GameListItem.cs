using System;

namespace Foosball.Microservice.Infrastructure.Queries.Models
{
    public class GameListItem
    {
        public Guid Id { get; set; }

        public string TeamAName { get; set; }

        public string TeamBName { get; set; }
    }
}
