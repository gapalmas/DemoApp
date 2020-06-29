using App.Core.Entities;
using App.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.Infrastructure.Data
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        #region 'Components'
        protected DataContext _dbContext;
        public GenericRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion
        #region 'Methods'
        #region 'CREATE'
        public IEnumerable<T> AddRange(IEnumerable<T> obj)
        {
            _dbContext.Set<T>().AddRange(obj);
            _dbContext.SaveChanges();
            return obj;
        }
        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> obj)
        {
            _dbContext.Set<T>().AddRange(obj);
            await _dbContext.SaveChangesAsync();
            return obj;
        }
        public T Create(T obj)
        {
            _dbContext.Set<T>().Add(obj);
            _dbContext.SaveChanges();
            return obj;
        }
        public async Task<T> CreateAsync(T obj)
        {
            _dbContext.Set<T>().Add(obj);
            await _dbContext.SaveChangesAsync();
            return obj;
        }
        #endregion
        #region 'READ'

        public T Find(Expression<Func<T, bool>> matchitem)
        {
            return _dbContext.Set<T>().SingleOrDefault(matchitem);
        }
        public async Task<T> FindAsync(Expression<Func<T, bool>> matchitem)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(matchitem);
        }
        public T FindInclude(Expression<Func<T, bool>> matchitem, Expression<Func<T, object>> criteria)
        {
            return _dbContext.Set<T>().Include(criteria).SingleOrDefault(matchitem);
        }
        public async Task<T> FindIncludeAsync(Expression<Func<T, bool>> matchitem, Expression<Func<T, object>> criteria)
        {
            return await _dbContext.Set<T>().Include(criteria).SingleOrDefaultAsync(matchitem);
        }
        public IEnumerable<T> FindAll(Expression<Func<T, bool>> matchitem)
        {
            return _dbContext.Set<T>().Where(matchitem).ToList();
        }
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> matchitem)
        {
            return await _dbContext.Set<T>().Where(matchitem).ToListAsync();
        }
        public IEnumerable<T> FindAllInclude(Expression<Func<T, bool>> matchitem, Expression<Func<T, object>> criteria)
        {
            return _dbContext.Set<T>().Where(matchitem).Include(criteria).ToList();
        }
        public async Task<IEnumerable<T>> FindAllIncludeAsync(Expression<Func<T, bool>> matchitem, Expression<Func<T, object>> criteria)
        {
            return await _dbContext.Set<T>().Where(matchitem).Include(criteria).ToListAsync();
        }

        public Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllIncludeAsync(Expression<Func<T, bool>> matchitem, params Expression<Func<T, object>>[] criteria)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            foreach (var includeProperty in criteria)
            {
                query = query.Include(includeProperty);
            }

            return await query.Where(matchitem).ToListAsync();
        }

        public IEnumerable<T> FindAllTake(int count)
        {
            return _dbContext.Set<T>().Take(count).ToList();
        }
        public async Task<IEnumerable<T>> FindAllTakeAsync(int count)
        {
            return await _dbContext.Set<T>().Take(count).ToListAsync();
        }
        public IEnumerable<T> FindAllWhere(Expression<Func<T, bool>> matchitem)
        {
            return _dbContext.Set<T>().Where(matchitem).ToList();
        }
        public async Task<IEnumerable<T>> FindAllWhereAsync(Expression<Func<T, bool>> matchitem)
        {
            return await _dbContext.Set<T>().Where(matchitem).ToListAsync();
        }
        public IEnumerable<T> FindAllWhereTake(Expression<Func<T, bool>> matchitem, int count)
        {
            return _dbContext.Set<T>().Where(matchitem).Take(count).ToList();
        }
        public async Task<IEnumerable<T>> FindAllWhereTakeAsync(Expression<Func<T, bool>> matchitem, int count)
        {
            return await _dbContext.Set<T>().Where(matchitem).Take(count).ToListAsync();
        }
        public T Get(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }
        public async Task<T> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public IEnumerable<T> Read()
        {
            return _dbContext.Set<T>().ToList();
        }
        public async Task<IEnumerable<T>> ReadAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
        #endregion
        #region 'UPDATE'
        public T Update(T obj)
        {
            T exist = _dbContext.Set<T>().Find(obj.Id);
            _dbContext.Entry(exist).CurrentValues.SetValues(obj); ;
            _dbContext.SaveChanges();
            return obj;
        }
        public async Task<T> UpdateAsync(T obj)
        {
            T exist = _dbContext.Set<T>().Find(obj.Id);
            _dbContext.Entry(exist).CurrentValues.SetValues(obj);
            //_dbContext.Entry(obj).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return obj;
        }
        #endregion
        #region 'DELETE'
        public T Delete(T obj)
        {
            _dbContext.Set<T>().Remove(obj);
            _dbContext.SaveChanges();
            return obj;
        }
        public async Task<T> DeleteAsync(T obj)
        {
            T exist = _dbContext.Set<T>().Find(obj.Id);
            _dbContext.Set<T>().Remove(exist);
            await _dbContext.SaveChangesAsync();
            return obj;
        }
        public IEnumerable<T> DeleteRange(IEnumerable<T> obj)
        {
            _dbContext.Set<T>().RemoveRange(obj);
            _dbContext.SaveChanges();
            return obj;
        }
        public async Task<IEnumerable<T>> DeleteRangeAsync(IEnumerable<T> obj)
        {
            _dbContext.Set<T>().RemoveRange(obj);
            await _dbContext.SaveChangesAsync();
            return obj;
        }
        #endregion
        #region 'COUNT'
        public int Count()
        {
            return _dbContext.Set<T>().Count();
        }
        public async Task<int> CountAsync()
        {
            return await _dbContext.Set<T>().CountAsync();
        }
        #endregion
        #region 'EXISTS'
        public bool Exist(Expression<Func<T, bool>> matchitem) /*Using for Generic Repository*/
        {
            return _dbContext.Set<T>().SingleOrDefault(matchitem) != null;
        }
        public async Task<bool> ExistAsync(Expression<Func<T, bool>> matchitem)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(matchitem) != null;
        }
        public bool Exists(Expression<Func<T, bool>> matchitem) /*Using for Manage Operations*/
        {
            return _dbContext.Set<T>().SingleOrDefault(matchitem) != null;
        }
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> matchitem)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(matchitem) != null;
        }
        #endregion
        #endregion
    }
}
