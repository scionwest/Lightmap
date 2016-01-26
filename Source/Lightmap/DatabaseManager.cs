using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Lightmap
{
    public class DatabaseManager
    {
        private readonly IEnumerable<IMigration> availableMigrations;

        public DatabaseManager(string database, IEnumerable<IMigration> migrations)
        {
            this.Database = database;
            if (migrations.Any(migration => migration.GetType().GetTypeInfo().GetCustomAttribute<MigrationVersionAttribute>() == null))
            {
                throw new InvalidOperationException($"One or more migration is missing a {typeof(IMigration).Name} attribute. An attribute must be applied with the correct version number in order for migrations to happen in the correct order.");
            }

            this.availableMigrations = migrations.OrderBy(migration =>
            {
                return migration.GetType().GetTypeInfo().GetCustomAttribute<MigrationVersionAttribute>().MigrationVersion;
            });

            foreach (IMigration migration in this.availableMigrations)
            {
                migration.Configure();
            }
        }

        public string Database { get; }

        public DbConnection CreateConnection()
        {
            return this.CreateSqliteConnection();
        }

        public async Task<DbConnection> OpenConnectionAsync(DbConnection connection)
        {
            await connection.OpenAsync();
            return connection;
        }

        public bool IsDatabaseAvailable()
        {
            return File.Exists(this.Database);
        }

        public async Task CreateDatabase()
        {
            if (string.IsNullOrEmpty(this.Database))
            {
                throw new DatabaseExistsException($"Unable to locate the Sqlite database {this.Database}.", this.Database);
            }

            if (this.IsDatabaseAvailable())
            {
                throw new DatabaseExistsException("The database already exists.", this.Database);
            }

            using (SqliteConnection dbConnection = this.CreateSqliteConnection())
            {
                await this.OpenConnectionAsync(dbConnection);
                await this.MigrateSqlDatabase(this.availableMigrations);
            }
        }

        public async Task UpgradeDatabase()
        {
            if (string.IsNullOrEmpty(this.Database))
            {
                throw new DatabaseExistsException($"Unable to locate the Sqlite database {this.Database}.", this.Database);
            }

            // If a database does not exist, we just create and upgrade to the latest.
            if (!this.IsDatabaseAvailable())
            {
                await this.CreateDatabase();
                return;
            }

            DbConnection connection = this.CreateConnection();
            await this.OpenConnectionAsync(connection);
            var version = await connection.ExecuteScalarAsync("Pragma schema_version");
            IEnumerable<IMigration> migrationsRemainingToUpgrade = this.availableMigrations.Where(migration =>
            {
                var migrationVersion = migration.GetType().GetTypeInfo().GetCustomAttribute<MigrationVersionAttribute>();
                return true;
            });
        }

        private SqliteConnection CreateSqliteConnection()
        {
            return new SqliteConnection($"Data Source={this.Database}");
        }

        private async Task MigrateSqlDatabase(IEnumerable<IMigration> migrations)
        {
            foreach (IMigration migration in migrations)
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
