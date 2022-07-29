using AirBnb.Models;
using AirBnb.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AirBnb.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IStatisticsService _StatsService;

        public StatsModel StatsModel { get; set; } = new();
        public IndexModel(ILogger<IndexModel> logger, IStatisticsService service)
        {
            _StatsService = service;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            StatsModel = await _StatsService.GetStatistics();
        }
    }
}
