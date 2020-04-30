using DemoApp.Web.Models.Entities;

namespace DemoApp.Web.Data
{
    public interface IUnitOfWork
    {
        IRepository<Product> ProductRepository { get; }

        void Commit();
        void Dispose();
        void RejectChanges();
    }
}