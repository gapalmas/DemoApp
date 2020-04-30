using DemoApp.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Web.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext dataContext;

        public IRepository<Product> ProductRepository => new GenericRepository<Product>(dataContext);

        public UnitOfWork(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void Dispose()
        {
            dataContext.Dispose();
        }

        public void Commit()
        {
            dataContext.SaveChanges();
        }

        public void RejectChanges()
        {
            foreach (var item in dataContext.ChangeTracker.Entries().Where(i => i.State != EntityState.Unchanged))
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        item.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                    case EntityState.Modified:
                        item.Reload();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
