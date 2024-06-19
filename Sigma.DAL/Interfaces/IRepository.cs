namespace Sigma.DAL.Interfaces;

public interface IRepository<Entity> where Entity : class
{
    public Task<Entity> GetAsync(long id);
    public Task<IEnumerable<Entity>> GetAllAsync();
    public Task<Entity> AddAsync(Entity entity);
    public Task<Entity> DeleteAsync(long id);
    public Task<Entity> UpdateAsync(Entity entity);
    public Task<bool> Exists(long id);

}