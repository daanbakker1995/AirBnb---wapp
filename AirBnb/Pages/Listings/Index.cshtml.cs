using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AirBnb.Data;
using AirBnb.Models;
using AirBnb.Repository;
using AirBnb.Repository.Interfaces;

namespace AirBnb.Pages.Listings
{
    public class IndexModel : PageModel
    {
        private readonly AirBnb.Data.AirbnbV2Context _context;
        private readonly IListingsRepository _listingsRepository;

        public IndexModel(AirBnb.Data.AirbnbV2Context context, IListingsRepository listingsRepository)
        {
            _context = context;
            _listingsRepository = listingsRepository;
        }

        public PaginatedList<ListingViewModel> Listings { get; set; } = default!;

        // This method is created with scaffolding and therefore not using a repository.
        public async Task OnGetAsync(int? pageIndex)
        {
            if (_context.Listings == null) return;

            Listings = await _listingsRepository.GetListingViewModels(pageIndex);
        }
    }
}
