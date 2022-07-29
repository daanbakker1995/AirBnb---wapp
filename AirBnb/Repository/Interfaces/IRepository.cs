using System.Linq.Expressions;

namespace AirBnb.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IList<TEntity>> GetAllAsync(int take = 0, int skip = 0);
        Task<TEntity?> Find(params object[] keys);
        Task InsertAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task Delete(TEntity entity);
    }
}
