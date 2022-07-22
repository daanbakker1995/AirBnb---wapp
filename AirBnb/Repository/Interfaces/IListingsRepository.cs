using AirBnb.Models;

namespace AirBnb.Repository.Interfaces
{
    public interface IListingsRepository : IRepository<Listing>
    {
        public Task<List<GeoData>> GetListingsGeoData(ListingsFilterOptions filterOptions);
        public Task<List<Listing>> GetOverViewListingsDataAsync(ListingsFilterOptions options = null);
    }
}
