using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Web.Data
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly DataContext dataContext;

        private DbSet<T> Dbset => dataContext.Set<T>();
        public IQueryable<T> Entities => Dbset;

        public GenericRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void Remove(T entity)
        {
            Dbset.Remove(entity);
        }

        public void Add(T entity)
        {
            Dbset.Add(entity);
        }
    }
}
