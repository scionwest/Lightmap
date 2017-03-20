using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Lightmap
{
    public class SqliteDatabaseManager : DatabaseManager
    {
        public SqliteDatabaseManager(string databaseName, string connectionString) : base(databaseName, connectionString)
        {
        }

        public override IDbConnection OpenConnection() => this.OpenSqliteConnection();

        public override async Task<IDbConnection> OpenConnectionAsync() => await this.OpenSqliteConnectionAsync();

        public SqliteConnection OpenSqliteConnection()
        {
            var sqliteConnection = new SqliteConnection(base.ConnectionString);
            sqliteConnection.Open();
            return sqliteConnection;
        }

        public async Task<SqliteConnection> OpenSqliteConnectionAsync()
        {
            var sqliteConnection = new SqliteConnection(base.ConnectionString);
            await sqliteConnection.OpenAsync();
            return sqliteConnection;
        }
    }
}
