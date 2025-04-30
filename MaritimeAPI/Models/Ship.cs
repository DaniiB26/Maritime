namespace MaritimeAPI.Models
{
    public class Ship
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required float MaxSpeed { get; set; }
    }
}