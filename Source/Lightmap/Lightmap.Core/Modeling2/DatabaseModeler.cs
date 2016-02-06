using System;
using System.Collections.Generic;
using System.Linq;

namespace Lightmap.Modeling2
{
    public class DatabaseModeler : IDatabaseModeler
    {
        private List<EntityBuilder> schema;

        private readonly TableManager tableManager;

        public DatabaseModeler(string databaseName)
        {
            if (string.IsNullOrEmpty(databaseName))
            {
                throw new ArgumentNullException(nameof(databaseName), "A database filename must be provided before you can model a database.");
            }

            this.DatabaseName = databaseName;
            this.tableManager = new TableManager();
            this.schema = new List<EntityBuilder>();
        }

        public string DatabaseName { get; }

        public IEntityBuilder Create()
        {
            EntityBuilder builder = this.schema.FirstOrDefault(b => b.State == EntityState.Creating);
            if (builder == null)
            {
                builder = new EntityBuilder(EntityState.Creating, this.tableManager, this);
                schema.Add(builder);
            }

            return builder;
        }
    }
}
