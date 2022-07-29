using AirBnb.Models;
using AirBnb.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingController : ControllerBase
    {
        private readonly IListingsRepository _listingsRepository;

        public ListingController(IListingsRepository listingsRepository)
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
