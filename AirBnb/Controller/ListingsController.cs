using AirBnb.Models;
using AirBnb.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace AirBnb.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ListingController : ControllerBase
    {
        private readonly IListingsRepository _listingsRepository;

        public ListingController(IListingsRepository listingsRepository, IDistributedCache cache)
        {
            _listingsRepository = listingsRepository;
        }

        [HttpGet]
        public async Task<Properties?> Get()
        {
            return await _listingsRepository.GetListingGeoDataById(1);
        }

        [HttpGet("{id}")]
        public async Task<Properties?> Get(int id)
        {
            return await _listingsRepository.GetListingGeoDataById(id);
        }
    }
}
