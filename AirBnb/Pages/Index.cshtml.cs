using AirBnb.Models;
using AirBnb.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AirBnb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly INeighbourhoodsRepository _neighbourhoodsRepository;
        private readonly IListingsRepository _listingsRepository;
        public List<GeoData> GeoData { get; set; } = default!;
        public List<string> Neighbourhoods { get; set; } = default!;
        public ListingsFilterOptions FilterOptions { get; set; } = new ListingsFilterOptions();

        public IndexModel(ILogger<IndexModel> logger, IListingsRepository listingsRepository, INeighbourhoodsRepository neighbourhoodsRepository)
        {
            _logger = logger;
            _neighbourhoodsRepository = neighbourhoodsRepository;
            _listingsRepository = listingsRepository;
        }

        public async Task OnGetAsync(ListingsFilterOptions options)
        {
            if (options.Neighbourhood == "Amsterdam") options.Neighbourhood = default;
            if (options.RoomType == "Amsterdam") options.Neighbourhood = default;
            FilterOptions = options;
            GeoData = await _listingsRepository.GetListingsGeoData(options);
            Neighbourhoods = await _neighbourhoodsRepository.GetNeighbourhoodsList();
        }
    }
}