using AirBnb.Data;
using AirBnb.Models;
using AirBnb.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace AirBnb.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AirbnbV2Context _context;
        private readonly IDistributedCache _cache;
        protected DbSet<TEntity> _set => _context.Set<TEntity>();

        public BaseRepository(AirbnbV2Context context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<IList<TEntity>> GetAllAsync(int take = 0, int skip = 0)
        {
            var cacheKey = $"GET_ALL_SKIP_{skip}_TAKE_{take}";
            var result = new List<TEntity>();

            var cachedData = await _cache.GetAsync(cacheKey);
            if (cachedData != null)
            {
                // If data found in cache, encode and deserialize cached data
                var cachedDataValue = Encoding.UTF8.GetString(cachedData);
                result = JsonConvert.DeserializeObject<List<TEntity>>(cachedDataValue);
                return result;
            }

            var query = _set.AsNoTracking();
            if (take > 0) query = query.Take(take);
            if (skip > 0) query = query.Skip(skip);
            var enities = await query.ToListAsync();

            // serialize data
            var cachedDataEntities = JsonConvert.SerializeObject(enities);
            var newDataToCache = Encoding.UTF8.GetBytes(cachedDataEntities);

            // set cache options 
            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(2))
                .SetSlidingExpiration(TimeSpan.FromMinutes(1));

            // Add data in cache
            await _cache.SetAsync(cacheKey, newDataToCache, options);

            return enities;
        }

        public async Task<TEntity?> Find(params object[] keys)
        {
            return await _set.FindAsync(keys);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _set.AddAsync(entity);
            await SaveAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _set.Update(entity);
            await SaveAsync();
        }

        public async Task Delete(TEntity entity)
        {
            _set.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
