﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AirBnb.Data;
using AirBnb.Models;

namespace AirBnb.Pages.Listings
{
    public class EditModel : PageModel
    {
        private readonly AirBnb.Data.AirbnbV2Context _context;

        public EditModel(AirBnb.Data.AirbnbV2Context context)
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

            var listing =  await _context.Listings.FirstOrDefaultAsync(m => m.Id == id);
            if (listing == null)
            {
                return NotFound();
            }
            Listing = listing;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Listing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListingExists(Listing.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ListingExists(int id)
        {
          return (_context.Listings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
