namespace AirBnb.Models
{
    public class StatsModel
    {
        public List<string> RoomTypes { get; set; }
        public IDictionary<string, int> ShortTermRentals { get; set; }
        public IDictionary<string, int> AvailabilityPerNeighbourhood { get; set; }
        public IDictionary<string, int> AveragePricePerNeighbourhood { get; set; }
        public IDictionary<string, int> TopHostWithListings { get; set; }
        public IDictionary<string, int> LicenseStats { get; set; }
    }
}
