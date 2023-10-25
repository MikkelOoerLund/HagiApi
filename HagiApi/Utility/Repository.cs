using Microsoft.EntityFrameworkCore;

namespace HagiApi
{
    public class Repository<TEntity> : IDisposable where TEntity : class
    {
        protected DbContext DbContext { get; }
        protected DbSet<TEntity> DbSet { get; }

        public Repository(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<TEntity>();
        }

        ~Repository()
        {
            Dispose();
        }

        public async Task<TEntity?> GetAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }


        public async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }

}
