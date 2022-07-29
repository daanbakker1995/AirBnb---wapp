using AirBnb.Models;

namespace AirBnb.Service
{
    public interface IStatisticsService
    {
        public Task<StatsModel> GetStatistics();
    }
}
