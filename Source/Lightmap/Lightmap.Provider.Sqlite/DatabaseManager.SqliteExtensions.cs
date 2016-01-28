using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Lightmap.Provider.Sqlite
{
    public static class DatabaseManagerSqliteExtensions
    {
        public static SqliteConnection CreateSqliteConnection(this DatabaseManager manager)
        {
            return new SqliteConnection($"Data Source={manager.Database}");
        }

        public static async Task<SqliteConnection> OpenSqliteConnectionAsync(this DatabaseManager manager)
        {
            SqliteConnection connection = manager.CreateSqliteConnection();
            await connection.OpenAsync();
            return connection;
        }

        public static bool IsSqliteDatabaseAvailable(this DatabaseManager manager)
        {
            return File.Exists(manager.Database);
        }

        public static async Task UpgradeDatabase(this DatabaseManager manager)
        {
            if (string.IsNullOrEmpty(manager.Database))
            {
                throw new DatabaseExistsException($"Unable to locate the Sqlite database {manager.Database}.", manager.Database);
            }

            using (DbConnection connection = await manager.OpenSqliteConnectionAsync())
            {
                var version = await connection.ExecuteScalarAsync("Pragma schema_version");
            }

            IEnumerable<IMigration> migrationsRemainingToUpgrade = manager.Migrations.Where(migration =>
            {
                var migrationVersion = migration.GetType().GetTypeInfo().GetCustomAttribute<MigrationVersionAttribute>();
                return true;
            });

            foreach (IMigration migration in migrationsRemainingToUpgrade)
            {
                try
                {
                    await migration.Apply();
                }
                catch (Exception)
                {
                    await migration.Rollback();
                    break;
                }
            }
        }
    }
}
