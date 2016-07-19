using System.Linq;
using System.Data.Entity;



namespace PPI.Core.Domain.Concrete
{
    using PPI.Core.Domain.Abstract;
    public class EfSpecialRepository<T> : ISpecialRepository<T> where T : class
    {

        private DbContext Context;
        
        public EfSpecialRepository(DbContext context)
        {
            Context = context;
        }
        [Log]
        public IQueryable<T> RunQuery(string query, params object[] parameters)
        {
            return Context.Database.SqlQuery<T>(query, parameters).ToList<T>().AsQueryable();
        }


        public int ExecuteQuery(string query, params object[] parameters)
        {
            return Context.Database.ExecuteSqlCommand(query, parameters);
        }
    }
}
