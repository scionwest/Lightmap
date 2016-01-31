using System;
using System.Collections.Generic;
using System.Linq;

namespace Lightmap.Modeling
{
    internal class DatabaseModeler : IDatabaseModeler
    {
        private List<IEntityBuilder> schema = new List<IEntityBuilder>();

        internal DatabaseModeler(string databaseName)
        {
            this.DatabaseName = databaseName;
        }

        public string DatabaseName { get; }

        public IEntityBuilder Create()
        {
            var builder = new EntityBuilder(EntityState.Creating);
            schema.Add(builder);
            return builder;
        }
    }
}
