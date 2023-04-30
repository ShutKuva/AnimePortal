using Core.DB;
using System.Linq.Expressions;

namespace DAL.Abstractions.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task CreateAsync(T entity);
    Task<T?> ReadAsync(int id);
    Task<IEnumerable<T>> ReadByConditionAsync(Expression<Func<T, bool>> predicate);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);

}