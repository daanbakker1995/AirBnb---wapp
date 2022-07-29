using AirBnb.Models;
using AirBnb.Repository.Interfaces;
using System.Linq;

namespace AirBnb.Service
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IListingsRepository _ListingsRepository;

        public StatisticsService(IListingsRepository listingsRepository)
        {
            _ListingsRepository = listingsRepository;
        }

        public async Task<StatsModel> GetStatistics()
        {
            var listings = await _ListingsRepository.GetAllAsync();

            var stats = new StatsModel
            {
                TotalRoomTypes = listings.GroupBy(l => l.RoomType).Select(l => new { RoomType = l.Key, RoomTypeCount = l.Count() }).ToDictionary(x => x.RoomType, x => x.RoomTypeCount),
                TopHostWithListings = listings.GroupBy(l => l.HostName)
                    .Select(l => new TopHost
                    {
                        HostName = l.Key,
                        TotalListings = l.Count(),
                        PrivateRooms = l.Where(l => l.RoomType == RoomTypeExtensions.GetRoomTypeDBName(RoomType.PrivateRoom)).Count(),
                        EntireHomeApt = l.Where(l => l.RoomType == RoomTypeExtensions.GetRoomTypeDBName(RoomType.EntireHomeApt)).Count(),
                        HotelRoom = l.Where(l => l.RoomType == RoomTypeExtensions.GetRoomTypeDBName(RoomType.HotelRoom)).Count(),
                        SharedRoom = l.Where(l => l.RoomType == RoomTypeExtensions.GetRoomTypeDBName(RoomType.SharedRoom)).Count(),
                    })
                    .Take(20)
                    .OrderByDescending(l => l.TotalListings).ToList(),
                AveragePricePerNeighbourhood = listings.GroupBy(l => l.NeighbourhoodCleansed).Select(l => new ListStringInt { StringValue = l.Key, IntValue = (int)Math.Round((decimal)l.Average(x => x.Price)) }).ToList(),
                AveragePricePerRoomTypes = listings.GroupBy(l => l.RoomType).Select(l => new ListStringInt { StringValue = l.Key, IntValue = (int)Math.Round((decimal)l.Average(x => x.Price)) }).ToList(),
                ListingsPerNeighbourhood = listings.GroupBy(l => l.NeighbourhoodCleansed).Select(l => new ListStringInt  { StringValue = l.Key, IntValue = l.Count() }).ToList(),
                ShortTermRentalsInMonth = new Dictionary<string, int>
                {
                     { "In30Days", (int)listings.Select(x => x.Availability30).Sum() },
                     { "In60Days", (int)listings.Select(x => x.Availability60).Sum() },
                     { "In90Days", (int)listings.Select(x => x.Availability90).Sum() }
                }
            };

            return stats;
        }
    }
}
