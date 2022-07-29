using AirBnb.Data;
using AirBnb.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AirBnb.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AirbnbV2Context _context;

        protected DbSet<TEntity> _set => _context.Set<TEntity>();

        public BaseRepository(AirbnbV2Context context)
        {
            _context = context;
        }

        public async Task<IList<TEntity>> GetAllAsync(int take = 0, int skip = 0)
        {
            var query = _set.AsNoTracking();
            if (take > 0) query = query.Take(take);
            if (skip > 0) query = query.Skip(skip);
            return await query.ToListAsync();
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
