namespace AirBnb.Models
{
    public class ListingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HostId { get; set; }
        public string HostName { get; set; }

        public string NeighbourhoodCleansed { get; set; }
        public string PropertyType { get; set; }
        public string RoomType { get; set; }
        public double? Price { get; set; }
        public int? MinimumNights { get; set; }
        public int? MaximumNights { get; set; }
    }
}
