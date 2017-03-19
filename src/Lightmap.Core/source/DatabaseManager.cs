using System.Data;
using System.Threading.Tasks;

namespace Lightmap
{
    public abstract class DatabaseManager : IDatabaseManager
    {
        public DatabaseManager(string databaseName)
        {
            this.Database = databaseName;
        }

        public string Database { get; }

        public abstract IDbConnection OpenConnection();

        public abstract Task<IDbConnection> OpenConnectionAsync();
    }
}
