namespace Backend.Data.Interfaces;

public interface IBaseRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> Find(string id);
}
