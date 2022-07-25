using AirBnb.Data;
using AirBnb.Models;
using AirBnb.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AirBnb.Repository
{
    public class NeighbourhoodsRepository : BaseRepository<Neighbourhood>, INeighbourhoodsRepository
    {
        public NeighbourhoodsRepository(AirbnbV2Context context) : base(context)
        {
        }

        public async Task<List<string>> GetNeighbourhoodsList()
        {
            List<string> neighbourhoods = new();
            var list = _set.AsNoTracking();

            foreach (var item in await list.Select(x => x.Neighbourhood1).ToListAsync())
            {
                neighbourhoods.Add(item);
            }
            return neighbourhoods;
        }
    }
}
