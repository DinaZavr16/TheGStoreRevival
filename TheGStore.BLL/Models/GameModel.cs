namespace TheGStore.Bll.Models
{
    public record GameModel
    {
        public string Icon { get; init; }

        public string Name { get; init; }

        public decimal Price { get; init; }

        public int DeveloperId { get; init; }

        public string Description { get; init; }
    }
}
