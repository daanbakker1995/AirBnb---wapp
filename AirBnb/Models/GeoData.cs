namespace AirBnb.Models
{
    public class GeoData
    {
        public string Type { get; set; } = "feature";
        public Properties Properties { get; set; } = new();
        public Geometry Geometry { get; set; } = new();

    }

    public class Geometry
    {
        public string Type { get; set; } = "Point";
        public List<string> Coordinates { get; set; } = new List<string>();
    }

    public class Properties
    {
        public int ListingId { get; set; }
        public string Name { get; set; }
        public int HostId { get; set; }
        public string HostName { get; set; }
        public string Neighbourhood { get; set; }
        public string RoomType { get; set; }
        public int NumberOfReviews { get; set; }
        public int MinimunNights { get; set; }
        public double Price { get; set; }
        public string Listing_url { get; internal set; }
    }
}
