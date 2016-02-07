using System;
using System.Collections.Generic;

namespace Lightmap.Modeling
{
    public class DatabaseModeler
    {
        private List<Table> tables;

        public DatabaseModeler(string databaseName)
        {
            this.DatabaseName = databaseName;
            this.tables = new List<Table>();
        }

        public string DatabaseName { get; }

        public DatabaseModelingOptions Create()
        {
            return new DatabaseModelingOptions(this);
        }

        internal void AddTable(Table table)
        {
            if (table == null)
            {
                throw new ArgumentNullException(nameof(table), "You can not add a null table to the database model.");
            }

            if (tables.Contains(table))
            {
                return;
            }

            this.tables.Add(table);
        }
    }
}
