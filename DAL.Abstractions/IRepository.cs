using Core.DB;
using System.Linq.Expressions;

namespace DAL.Abstractions;

public interface IRepository<T> where T : BaseEntity
{
    void Create(T entity);
    T Read(int id);
    IEnumerable<T> ReadByCondition(Expression<Func<T, bool>> predicate);
}