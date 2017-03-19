using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Lightmap.Sqlite
{
    public class SqliteDatabaseManager : DatabaseManager
    {
        public SqliteDatabaseManager(string databaseName) : base(databaseName)
        {
        }

        public override IDbConnection OpenConnection() => this.OpenSqliteConnection();

        public override async Task<IDbConnection> OpenConnectionAsync() => await this.OpenSqliteConnectionAsync();

        public SqliteConnection OpenSqliteConnection()
        {
            var sqliteConnection = new SqliteConnection($"Data Source={base.Database}");
            sqliteConnection.Open();
            return sqliteConnection;
        }

        public async Task<SqliteConnection> OpenSqliteConnectionAsync()
        {
            var sqliteConnection = new SqliteConnection($"Data Source={base.Database}");
            await sqliteConnection.OpenAsync();
            return sqliteConnection;
        }
    }
}
