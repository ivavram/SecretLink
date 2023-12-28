using Interface;
using Microsoft.AspNetCore.Http.HttpResults;


namespace Repository
{

    public class BaseRepository<T> : IBaseRepo<T> where T : class
    {
        protected Context? context;
        protected DbSet<T> dbSet;
        
        public BaseRepository(Context context)
        {
            this.context = context;
            dbSet = this.context.Set<T>();
        }
        public virtual async void Create(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public virtual async void Delete(int id)
        {
            T? entity = await dbSet.FindAsync(id);
            if( entity != null)
                dbSet.Remove(entity);
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await this.dbSet.ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            T? entity = await dbSet.FindAsync(id);
            return entity!;
        }

        public virtual void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}