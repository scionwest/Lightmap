using System;
using System.Collections.Generic;
using System.Linq;

namespace Lightmap.Modeling
{
    internal class DatabaseModeler : IDatabaseModeler
    {
        private List<IEntityBuilder> schema = new List<IEntityBuilder>();

        private readonly TableManager tableManager;

        internal DatabaseModeler(string databaseName)
        {
            this.DatabaseName = databaseName;
            this.tableManager = new TableManager();
        }

        public string DatabaseName { get; }

        public IEntityBuilder Create()
        {
            var builder = new EntityBuilder(EntityState.Creating, this.tableManager, this);
            schema.Add(builder);
            return builder;
        }
    }
}
