using Microsoft.EntityFrameworkCore;
using Sigma.DAL.DbContext;
using Sigma.DAL.Interfaces;

namespace Sigma.DAL.Repositories;

public class Repository<Entity> : IRepository<Entity> where Entity : class
{
    private readonly PgDbContext _dbContext;
    public Repository(PgDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Entity> AddAsync(Entity entity)
    {
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<Entity> DeleteAsync(long id)
    {
        var entity = await GetAsync(id);
        _dbContext.Set<Entity>().Remove(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> Exists(long id)
    {
        var entity = await GetAsync(id);
        return entity != null;
    }

    public async Task<IEnumerable<Entity>> GetAllAsync()
    {
        return await _dbContext.Set<Entity>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Entity> GetAsync(long id)
    {
        return await _dbContext.Set<Entity>().FindAsync(id);
    }

    public async Task<Entity> UpdateAsync(Entity entity)
    {
        try
        {
            var result = _dbContext.Update<Entity>(entity);
            _dbContext.SaveChanges();
            return result.Entity;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}