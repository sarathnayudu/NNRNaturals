using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TeleMed.Context.Interfaces
{
    /// <summary>
    /// Defines interface for common data access functionality for entity.
    /// </summary>
    /// <typeparam name="T">Type of entity.</typeparam>
    public interface IRepository<T>
    {
        T GetSingle(Expression<Func<T, bool>> whereCondition);

        void Add(T entity);

        void Delete(T entity);

        void Attach(T entity);

        IList<T> GetAll(Expression<Func<T, bool>> whereCondition);

        IList<T> GetAll();

        IQueryable<T> GetQueryable();

        void SetAutoDetectChangesEnabled(bool val);

        long Count(Expression<Func<T, bool>> whereCondition);

        void ExecSp(string SpName, SqlParameter[] param);

        string ExecSpWithOutputParamter(string SpName, SqlParameter[] param);
    }
}
