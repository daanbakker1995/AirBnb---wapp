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
        private readonly Airbnb2022Context _context;

        public IndexModel(Airbnb2022Context context)
        {
            _context = context;
        }

        public IList<Listing> Listing { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Listings != null)
            {
                Listing = await _context.Listings.ToListAsync();
            }
        }
    }
}
