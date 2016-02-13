using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Lightmap.Modeling;

namespace Lightmap
{
    public class DatabaseManager
    {
        private Dictionary<IMigration, DatabaseModeler> migrationModelers;

        public DatabaseManager(string database, IEnumerable<IMigration> migrations)
        {
            this.Database = database;
            if (migrations.Any(migration => migration.GetType().GetTypeInfo().GetCustomAttribute<MigrationVersionAttribute>() == null))
            {
                throw new InvalidOperationException($"One or more migration is missing a {typeof(IMigration).Name} attribute. An attribute must be applied with the correct version number in order for migrations to happen in the correct order.");
            }

            this.Migrations = migrations.OrderBy(migration =>
            {
                MigrationVersionAttribute attribute = AttributeCache.GetAttribute<MigrationVersionAttribute>(migration.GetType());
                return attribute == null ? 0 : attribute.MigrationVersion;
            });

            this.migrationModelers = new Dictionary<IMigration, DatabaseModeler>();
            foreach (IMigration migration in this.Migrations)
            {
                var modeler = new DatabaseModeler(this.Database);
                migration.Configure(modeler);
                migrationModelers.Add(migration, modeler);
            }
        }

        public string Database { get; }

        public IDataProviderFactory ProviderFactory { get; set; }

        public IEnumerable<IMigration> Migrations { get; }

        public IDataProvider GetProvider()
        {
            return this.ProviderFactory.CreateProvider(this);
        }

        public IDatabaseModelBrowser GetDatabaseModelBrowser(IMigration migration)
        {
            return migrationModelers[migration];
        }
    }
}