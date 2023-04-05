using MessagingSystem.DataAccess.Entities;

namespace MessagingSystem.DataAccess;

public interface IRepository<T>
{
    void Add(T entity);
    void Delete(T entity);
    T GetById(Guid id);
    List<T> GetAll();
    void Serialize();
    void Deserialize();
}