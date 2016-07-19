using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;



namespace PPI.Core.Domain.Concrete
{
    using PPI.Core.Domain.Abstract;

    public class EfGenericRepository<T> : IGenericRepository<T> where T : class
    {

        private DbContext Context;        
        public EfGenericRepository(DbContext context)
        {
            Context = context;
        }
        
        
        [Log]        
        public virtual IQueryable<T> AsQueryable()
        {
            return Context.Set<T>().AsQueryable();
        }        
        [Log]
        public virtual IEnumerable<T> GetWithRawSql(string query, params object[] parameters)
        {
            return Context.Set<T>().SqlQuery(query, parameters);
        }
        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }
        [Log]
        
        public IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }
        [Log]
        
        public T Single(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Single(predicate);
        }
        [Log]
        
        public T SingleOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().SingleOrDefault(predicate);
        }
        [Log]
        
        public T First(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().FirstOrDefault(predicate);
        }
        [Log]
        
        public T GetById(int id)
        {
            return Context.Set<T>().Find(id);
        }
        [Log]
        
        public void Add(IEnumerable<T> entity)
        {
            Context.Set<T>().AddRange(entity);
        }
        [Log]
        
        public void Delete(IEnumerable<T> entity)
        {
            Context.Set<T>().RemoveRange(entity);
        }
        [Log]
        
        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }
        [Log]
        
        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);            
        }
        [Log]
        
        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;               
        }

        
    }
}
