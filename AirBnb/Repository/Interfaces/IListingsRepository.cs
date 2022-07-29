using AirBnb.Models;

namespace AirBnb.Repository.Interfaces
{
    public interface IListingsRepository : IRepository<Listing>
    {
        public Task<List<GeoData>> GetListingsGeoData(ListingsFilterOptions filterOptions);
        public Task<Properties?> GetListingGeoDataById(int id);
        public Task<PaginatedList<ListingViewModel>> GetListingViewModels(int? pageIndex);
    }
}
