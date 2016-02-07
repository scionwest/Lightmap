using System;
using System.Collections.Generic;
using System.Linq;

namespace Lightmap.Modeling
{
    public class DatabaseModeler
    {
        private List<Table> tables;

        public DatabaseModeler(string databaseName)
        {
            if (string.IsNullOrEmpty(databaseName))
            {
                throw new ArgumentNullException(nameof(databaseName), "The modeler needs to know what database you are modeling. You must provide a database name.");
            }

            this.DatabaseName = databaseName;
            this.tables = new List<Table>();
        }

        public string DatabaseName { get; }

        public DatabaseModelingOptions Create()
        {
            return new DatabaseModelingOptions(this);
        }

        public void AddTable(Table table)
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

        public Table[] GetTables()
        {
            return this.tables.ToArray();
        }

        public bool HasTable(string name)
        {
            return this.tables.Any(table => table.Name == name);
        }
    }
}
