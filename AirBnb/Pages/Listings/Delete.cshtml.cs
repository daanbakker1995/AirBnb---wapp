using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AirBnb.Data;
using AirBnb.Models;
using AirBnb.Repository.Interfaces;

namespace AirBnb.Pages.Listings
{
    public class DeleteModel : PageModel
    {
        private readonly IListingsRepository _repository;

        public DeleteModel(IListingsRepository listingsRepository)
        {
            _repository = listingsRepository;
        }

        [BindProperty]
        public Listing Listing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listing = await _repository.Find(id);

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
            if (id == null)
            {
                return NotFound();
            }
            var listing = await _repository.Find(id);

            if (listing != null)
            {
                Listing = listing;
                await _repository.Delete(Listing);
            }

            return RedirectToPage("./Index");
        }
    }
}
