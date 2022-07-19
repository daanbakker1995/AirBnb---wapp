using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AirBnb.Data;
using AirBnb.Models;
using AirBnb.Repository.Interfaces;

namespace AirBnb.Pages.Listings
{
    public class CreateModel : PageModel
    {
        private readonly IListingsRepository _repository;

        public CreateModel(IListingsRepository listingsRepository)
        {
            _repository = listingsRepository;
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
            if (!ModelState.IsValid || Listing == null)
            {
                return Page();
            }

            await _repository.InsertAsync(Listing);

            return RedirectToPage("./Index");
        }
    }
}
