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
    public class IndexModel : PageModel
    {
        private readonly IListingsRepository _repository;

        public IndexModel(IListingsRepository listingsRepository)
        {
            _repository = listingsRepository;
        }

        public IList<Listing> Listing { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Listing = await _repository.GetAllAsync();
        }
    }
}
