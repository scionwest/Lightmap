using System.Data;
using System.Threading.Tasks;

namespace Lightmap
{
    public abstract class DatabaseManagerBase : IDatabaseManager
    {
        protected DatabaseManagerBase(string databaseName, string connectionString)
        {
            this.Database = databaseName;
            this.ConnectionString = connectionString;
        }

        protected virtual string ConnectionString { get; }

        public virtual string Database { get; }

        public abstract IDbConnection OpenConnection();

        public abstract Task<IDbConnection> OpenConnectionAsync();
    }
}
