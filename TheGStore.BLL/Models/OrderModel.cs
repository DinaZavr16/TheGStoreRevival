using System;

namespace TheGStore.Bll.Models
{
    public record OrderModel 
    {
        public int CustomerId { get; init; }

        public int GameId { get; init; }

        public DateTime Date { get; init; }
    }
}
