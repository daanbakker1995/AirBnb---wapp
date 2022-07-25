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

        public IList<ListingViewModel> Listing { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Listings != null)
            {
                var listings = await _context.Listings.Select(x => new ListingViewModel
                {
                    Id = x.Id,
                    HostId = x.HostId,
                    Name = x.Name,
                    HostName = x.HostName,
                    Neighbourhood = x.Neighbourhood,
                    PropertyType = x.PropertyType,
                    RoomType = x.RoomType,
                    Price = x.Price,
                    MinimumNights = x.MinimumNights,
                    MaximumNights = x.MaximumNights
                }).ToListAsync();
                Listing = listings;
            }
        }
    }
}
