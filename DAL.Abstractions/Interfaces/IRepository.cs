using Core.DB;
using System.Linq.Expressions;

namespace DAL.Abstractions.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    void Create(T entity);
    Task CreateAsync(T entity);
    T? Read(int id);
    Task<T?> ReadAsync(int id);
    IEnumerable<T?> ReadByCondition(Expression<Func<T, bool>> predicate);
    Task Update(T entity);
    void Delete(int id);

}