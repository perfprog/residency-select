using System.Linq;


namespace PPI.Core.Domain.Abstract
{
    public interface ISpecialRepository<T> where T : class
    {
        IQueryable<T> RunQuery(string query, params object[] parameters);
        int ExecuteQuery(string query, params object[] parameters);
    }
}
