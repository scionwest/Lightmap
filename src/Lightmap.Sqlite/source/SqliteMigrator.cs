using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Lightmap.Modeling
{
    internal class SqliteMigrator : IDatabaseMigrator
    {
        public SqliteMigrator(IMigration[] migrations)
        {
            this.Migrations = migrations;
        }

        public IMigration[] Migrations { get; }

        public bool IsMigrationNeeded()
        {
            throw new NotImplementedException();
        }

        public void Apply(IDatabaseManager databaseManager)
        {
            int dbVersion = 0;
            using (IDbConnection connection = databaseManager.OpenConnection())
            {
                var schemaVersion = connection.ExecuteScalar("Pragma schema_version");
                if (!int.TryParse(schemaVersion.ToString(), out dbVersion))
                {
                    // throw exception here.
                }
            }

            // Return all migrations that have a version attribute assigned to them, that are greater than our current version.
            IEnumerable<IMigration> migrationsRemainingToUpgrade = this.Migrations.Where(migration =>
            {
                //MigrationVersionAttribute migrationVersion = AttributeCache.GetAttribute<MigrationVersionAttribute>(migration.GetType());
                //return migrationVersion != null;
                return true;
            });
        }

        public Task ApplyAsync(IDatabaseManager databaseManager)
        {
            throw new NotImplementedException();
        }
    }
}
