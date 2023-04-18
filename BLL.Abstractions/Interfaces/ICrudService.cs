using Core.DB;
using System.Linq.Expressions;

namespace BLL.Abstractions.Interfaces
{
    public interface ICrudService<T> where T : BaseEntity
    {
        Task Create(T entity);
        Task<T?> Read(int id);
        Task<IEnumerable<T>> ReadByCondition(Expression<Func<T, bool>> predicate);
        Task Update(T entity);
        Task Delete(int id);
    }
}