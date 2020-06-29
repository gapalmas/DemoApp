using App.Core.Entities;
using App.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.Core.UseCases
{
    public class ManageOperations<T> : IOperations<T> where T : BaseEntity
    {
        #region 'Components'
        private readonly IRepository<T> Repository;
        public ManageOperations(IRepository<T> Repository)
        {
            this.Repository = Repository;
        }
        #endregion
        #region 'Methods'
        #region 'Create'
        public T Create(T Data)
        {
            Repository.Create(Data);
            return Data;
        }

        public Task<T> CreateAsync(T Data)
        {
            return Repository.CreateAsync(Data);
            //return Data;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> Data)
        {
            Repository.AddRange(Data);
            return Data;
        }
        public IEnumerable<T> AddRangeAsync(IEnumerable<T> Data)
        {
            Repository.AddRangeAsync(Data);
            return Data;
        }
        #endregion
        #region 'Read'
        public T Read(T Data)
        {
            return Repository.Get(Data.Id);
        }
        public Task<T> ReadAsync(T Data)
        {
            return Repository.GetAsync(Data.Id);
        }
        public T Get(int Id)
        {
            return Repository.Get(Id);
        }
        public Task<T> GetAsync(int Id)
        {
            return Repository.GetAsync(Id);
        }
        public T Find(Expression<Func<T, bool>> matchitem)
        {
            return Repository.Find(matchitem);
        }
        public Task<T> FindAsync(Expression<Func<T, bool>> matchitem)
        {
            return Repository.FindAsync(matchitem);
        }
        public T FindInclude(Expression<Func<T, bool>> matchitem, Expression<Func<T, object>> criteria)
        {
            return Repository.FindInclude(matchitem, criteria);
        }
        public Task<T> FindIncludeAsync(Expression<Func<T, bool>> matchitem, Expression<Func<T, object>> criteria)
        {
            return Repository.FindIncludeAsync(matchitem, criteria);
        }
        public IEnumerable<T> FindAllWhere(Expression<Func<T, bool>> matchitem)
        {
            return Repository.FindAllWhere(matchitem);
        }
        public IEnumerable<T> FindAllWhereTake(Expression<Func<T, bool>> matchitem, int count)
        {
            return Repository.FindAllWhereTake(matchitem, count);
        }
        public IEnumerable<T> FindAll(Expression<Func<T, bool>> matchitem)
        {
            return Repository.FindAll(matchitem);
        }
        public IEnumerable<T> FindAllInclude(Expression<Func<T, bool>> matchitem, Expression<Func<T, object>> criteria)
        {
            return Repository.FindAllInclude(matchitem, criteria);
        }
        public Task<IEnumerable<T>> FindAllIncludeAsync(Expression<Func<T, bool>> matchitem, Expression<Func<T, object>> criteria)
        {
            return Repository.FindAllIncludeAsync(matchitem, criteria);
        }

        public async Task<IEnumerable<T>> GetAllIncludeAsync(Expression<Func<T, bool>> matchitem, params Expression<Func<T, object>>[] criteria)
        {
            return await Repository.GetAllIncludeAsync(matchitem, criteria);
        }

        public Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return Repository.GetSingleAsync(predicate, includeProperties);
        }

        public Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> matchitem)
        {
            return Repository.FindAllAsync(matchitem);
        }
        public IEnumerable<T> FindAllTake(int count)
        {
            return Repository.FindAllTake(count);
        }
        public Task<IEnumerable<T>> FindAllTakeAsync(int count)
        {
            return Repository.FindAllTakeAsync(count);
        }
        #endregion
        #region'Update'
        public T Update(T Data)
        {
            return Repository.Update(Data);
        }
        public Task<T> UpdateAsync(T Data)
        {
            return Repository.UpdateAsync(Data);
        }
        #endregion
        #region 'Delete'
        public T Delete(T Data)
        {
            Repository.DeleteAsync(Data);
            return Data;
        }
        #endregion
        #region'Count'
        public int Count()
        {
            return Repository.Count();
        }
        public Task<int> CountAsync()
        {
            return Repository.CountAsync();
        }
        #endregion
        #region'Exist'
        public bool Exists(Expression<Func<T, bool>> matchitem)
        {
            return Repository.Exist(matchitem);
        }
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> matchitem)
        {
            return await Repository.ExistAsync(matchitem);
        }

        #endregion
        #endregion
    }
}
