using AirBnb.Models;
using AirBnb.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AirBnb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IListingsRepository _listingsRepository;
        public IList<Listing> Listings { get; set; } = new List<Listing>();
        public IList<GeoData> GeoData { get; set; } = default!;
        //[BindProperty]
        public ListingsFilterOptions FilterOptions { get; set; } = new ListingsFilterOptions();

        public IndexModel(ILogger<IndexModel> logger, IListingsRepository listingsRepository)
        {
            _logger = logger;
            _listingsRepository = listingsRepository;
        }

        public async Task OnGetAsync(ListingsFilterOptions options)
        {
            FilterOptions = options;
            Listings = await _listingsRepository.GetOverViewListingsDataAsync(options);
            GeoData = await _listingsRepository.GetListingsGeoData(options);
        }
    }
}