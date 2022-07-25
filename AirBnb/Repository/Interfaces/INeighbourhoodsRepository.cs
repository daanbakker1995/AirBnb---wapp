using AirBnb.Models;

namespace AirBnb.Repository.Interfaces
{
    public interface INeighbourhoodsRepository : IRepository<Neighbourhood>
    {

        public Task<List<string>> GetNeighbourhoodsList();
    }
}
