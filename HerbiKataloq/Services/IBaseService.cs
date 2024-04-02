using HerbiKataloq.Models.TeyyareModels;

namespace HerbiKataloq.Services
{
    public interface IBaseService<TEntity>
    {
        Task<List<TEntity>> Find(string key, string value);
        Task<TEntity> GetAsync(string id);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllByCategory(string category);
        Task<string> CreateAsync(TEntity entity);
        Task UpdateAsync(string id, TEntity entity);
        Task DeleteAsync(string id);
    }
}
