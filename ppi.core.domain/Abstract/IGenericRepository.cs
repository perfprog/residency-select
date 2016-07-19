using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PPI.Core.Domain.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        
        IQueryable<T> AsQueryable(); 
        IEnumerable<T> GetAll();
        IEnumerable<T> GetWithRawSql(string query, params object[] parameters);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        T Single(Expression<Func<T, bool>>  predicate);
        T SingleOrDefault(Expression<Func<T, bool>> predicate);
        T First(Expression<Func<T, bool>> predicate);
        T GetById(int id);
        void Add(IEnumerable<T> entity);
        void Delete(IEnumerable<T> entity);        
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);        
        
    }
}
