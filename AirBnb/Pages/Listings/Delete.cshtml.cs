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
    public class DeleteModel : PageModel
    {
        private readonly AirBnb.Data.AirbnbV2Context _context;

        public DeleteModel(AirBnb.Data.AirbnbV2Context context)
        {
            _context = context;
        }

        [BindProperty]
      public Listing Listing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Listings == null)
            {
                return NotFound();
            }

            var listing = await _context.Listings.FirstOrDefaultAsync(m => m.Id == id);

            if (listing == null)
            {
                return NotFound();
            }
            else 
            {
                Listing = listing;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Listings == null)
            {
                return NotFound();
            }
            var listing = await _context.Listings.FindAsync(id);

            if (listing != null)
            {
                Listing = listing;
                _context.Listings.Remove(Listing);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
