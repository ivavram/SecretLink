
namespace Interface
{
    public interface IBaseRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id); 
        void Create(T entity); 
        void Update(T entity);
        void Delete(int id);
    }
}