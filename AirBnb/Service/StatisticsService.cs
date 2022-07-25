using AirBnb.Models;
using AirBnb.Repository.Interfaces;

namespace AirBnb.Service
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IListingsRepository _ListingsRepository;

        public StatisticsService(IListingsRepository listingsRepository)
        {
            _ListingsRepository = listingsRepository;
        }

        public async Task GetStatistics()
        {
            var listings = await _ListingsRepository.GetAllAsync();
            var stats = new StatsModel();

        }
    }
}
