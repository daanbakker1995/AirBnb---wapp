namespace AirBnb.Models
{
    public class StatsModel
    {
        public IDictionary<string, int>? TotalRoomTypes { get; set; }
        public IDictionary<string, int>? ShortTermRentalsInMonth { get; set; }
        public List<ListStringInt> ListingsPerNeighbourhood { get; set; }
        public List<ListStringInt> AveragePricePerNeighbourhood { get; set; }
        public List<ListStringInt> AveragePricePerRoomTypes { get; set; }
        public List<TopHost>? TopHostWithListings { get; set; }
    }

    public class ListStringInt
    {
        public string StringValue { get; set; }
        public int IntValue { get; set; }
    }

    public class TopHost
    {
        public string? HostName { get; set; }
        public int TotalListings { get; set; }
        public int PrivateRooms { get; set; }
        public int EntireHomeApt { get; set; }
        public int HotelRoom { get; set; }
        public int SharedRoom { get; set; }
    }
}
