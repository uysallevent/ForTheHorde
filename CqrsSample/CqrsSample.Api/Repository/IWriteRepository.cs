namespace CqrsSample.Api.Repository
{
    public interface IWriteRepository<in T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void DetectUpdate(T entity);
        void Delete(T entity);
        void Delete(int id);
        void Save();
    }
}
