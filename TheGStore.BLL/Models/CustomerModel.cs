namespace TheGStore.Bll.Models
{
    public record CustomerModel 
    {
        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string Email { get; init; }
    }
}
