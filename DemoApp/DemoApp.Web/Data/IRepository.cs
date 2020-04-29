using System.Linq;

namespace DemoApp.Web.Data
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Entities { get; }

        void Add(T entity);
        void Remove(T entity);
    }
}