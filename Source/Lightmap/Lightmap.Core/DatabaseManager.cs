using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Lightmap.Modeling2;

namespace Lightmap
{
    public class DatabaseManager
    {
        public DatabaseManager(string database, IEnumerable<IMigration> migrations)
        {
            this.Database = database;
            if (migrations.Any(migration => migration.GetType().GetTypeInfo().GetCustomAttribute<MigrationVersionAttribute>() == null))
            {
                throw new InvalidOperationException($"One or more migration is missing a {typeof(IMigration).Name} attribute. An attribute must be applied with the correct version number in order for migrations to happen in the correct order.");
            }

            this.Migrations = migrations.OrderBy(migration =>
            {
                return migration.GetType().GetTypeInfo().GetCustomAttribute<MigrationVersionAttribute>().MigrationVersion;
            });

            foreach (IMigration migration in this.Migrations)
            {
                var modeler = new DatabaseModeler(this.Database);
                migration.Configure(modeler);
            }
        }

        public string Database { get; }

        public IEnumerable<IMigration> Migrations { get; }
    }
}