namespace MaritimeAPI.Models
{
    public class Voyage
    {
        public int Id { get; set; }

        public required DateTime VoyageDate { get; set; }

        public int DeparturePortId { get; set; }
        public Port? DeparturePort { get; set; }

        public int ArrivalPortId { get; set; }
        public Port? ArrivalPort { get; set; }

        public required DateTime VoyageStart { get; set; }
        public required DateTime VoyageEnd { get; set; }

        public int ShipId { get; set; }
        public Ship? Ship { get; set; }
    }
}
