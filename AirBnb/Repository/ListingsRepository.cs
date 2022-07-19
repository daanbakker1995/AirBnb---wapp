using AirBnb.Data;
using AirBnb.Models;
using AirBnb.Repository.Interfaces;

namespace AirBnb.Repository
{
    public class ListingsRepository : BaseRepository<Listing>, IListingsRepository
    {
        public ListingsRepository(Airbnb2022Context context) : base(context)
        {
        }
    }
}
