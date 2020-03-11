using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleProject.Data.Models;

namespace SimpleProject.Data.Repositories
{
    public class SimpleRepository
    {
        private readonly SimpleProjectContext _dbContext;

        public SimpleRepository(SimpleProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Entity> GetById(Guid id)
        {
            return await _dbContext.Entities
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Create(Entity entity)
        {
            _dbContext.Entities.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Entity entity)
        {
            _dbContext.Entities.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
