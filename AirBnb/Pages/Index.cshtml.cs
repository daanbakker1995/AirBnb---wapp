using AirBnb.Models;
using AirBnb.Repository.Interfaces;
using AirBnb.Service;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AirBnb.Pages
{
    [ValidateAntiForgeryToken]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly INeighbourhoodsRepository _neighbourhoodsRepository;
        private readonly IListingsRepository _listingsRepository;

        public string AccessToken { get; }
        public List<GeoData> GeoData { get; set; } = new List<GeoData>();
        public List<string> Neighbourhoods { get; set; } = new List<string>();
        public ListingsFilterOptions FilterOptions { get; set; } = new ListingsFilterOptions();

        public IndexModel(ILogger<IndexModel> logger, IListingsRepository listingsRepository, INeighbourhoodsRepository neighbourhoodsRepository, IStatisticsService service, IOptions<MapBoxSettings> mapboxSettings)
        {
            _logger = logger;
            _neighbourhoodsRepository = neighbourhoodsRepository;
            _listingsRepository = listingsRepository;
            AccessToken = mapboxSettings.Value.AccessToken;
        }

        public async Task OnGetAsync(ListingsFilterOptions options)
        {
            await GetData(options);
        }

        public async Task OnPostAsync(ListingsFilterOptions options)
        {
            await GetData(options);
        }

        private async Task GetData(ListingsFilterOptions options)
        {
            if (options.Neighbourhood == "Amsterdam") options.Neighbourhood = default;
            FilterOptions = options;
            Neighbourhoods = await _neighbourhoodsRepository.GetNeighbourhoodsList();
            GeoData = await _listingsRepository.GetListingsGeoData(options);
        }
    }
}