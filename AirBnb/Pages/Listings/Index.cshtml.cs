using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AirBnb.Data;
using AirBnb.Models;

namespace AirBnb.Pages.Listings
{
    public class IndexModel : PageModel
    {
        private readonly AirBnb.Data.AirbnbV2Context _context;

        public IndexModel(AirBnb.Data.AirbnbV2Context context)
        {
            _context = context;
        }

        public PaginatedList<ListingViewModel> Listing { get; set; } = default!;

        // This method is created with scaffolding and therefore not using a repository.
        public async Task OnGetAsync(int? pageIndex)
        {
            if (_context.Listings == null) return;

            IQueryable<Listing> listings = _context.Listings;

            Listing = await PaginatedList<ListingViewModel>.CreateAsync(listings.Select(x => new ListingViewModel
            {
                Id = x.Id,
                Name = x.Name,
                HostId = x.HostId,
                HostName = x.HostName,
                NeighbourhoodCleansed = x.NeighbourhoodCleansed,
                PropertyType = x.PropertyType,
                RoomType = x.RoomType,
                Price = x.Price,
                MinimumNights = x.MinimumNights,
                MaximumNights = x.MaximumNights
            }).AsNoTracking(), pageIndex ?? 1, 500);
        }
    }
}
