namespace MaritimeAPI.Models
{
    public class Port
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int CountryId { get; set; }
        public Country? Country { get; set; }

    }
}