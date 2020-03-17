using System.Linq;
using VueShopServer.Api.Entities;

namespace VueShopServer.Api.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Get(int id);

        T Insert(T entity);

        T Delete(T entity);

        T Update(T entity);

        IQueryable<T> AsQueryable { get; }
    }
}
