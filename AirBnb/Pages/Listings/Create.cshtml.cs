using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AirBnb.Data;
using AirBnb.Models;

namespace AirBnb.Pages.Listings
{
    public class CreateModel : PageModel
    {
        private readonly AirBnb.Data.AirbnbV2Context _context;

        public CreateModel(AirBnb.Data.AirbnbV2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Listing Listing { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Listings == null || Listing == null)
            {
                return Page();
            }

            _context.Listings.Add(Listing);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
