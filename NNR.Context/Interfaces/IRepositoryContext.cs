using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleMed.Context.Interfaces
{
    public interface IRepositoryContext
    {
        IDbSet<T> GetObjectSet<T>() where T : class;

        DbContext ObjectContext { get; }

        /// <summary>
        /// Save all changes to all repositories
        /// </summary>
        /// <returns>Integer with number of objects affected</returns>
        int SaveChanges();

        /// <summary>
        /// Terminates the current repository context
        /// </summary>
        void Terminate();

        void SetAutoDetectChangesEnabled(bool val);
    }
}
