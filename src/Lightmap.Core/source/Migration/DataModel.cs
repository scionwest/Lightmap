using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Lightmap.Migration
{
    public class DataModel : IDataModel
    {
        private List<ITableBuilder> tables;

        public DataModel(IDatabaseMigrator migrator)
        {
            this.DataModelMigration = migrator;

            this.tables = new List<ITableBuilder>();
        }

        public IDatabaseMigrator DataModelMigration { get; }

        public ISchemaBuilder AddSchema(string schemaName)
        {
            throw new NotImplementedException();
        }

        public ITableBuilder[] GetTables() => this.tables.ToArray();

        public ISchemaBuilder[] GetSchemas() => throw new NotImplementedException();

        public ITableBuilder AddTable(string schema, string tableName)
        {
            if (string.IsNullOrEmpty(schema))
            {
                throw new ArgumentException("You must specify the schema that owns the table being added.", nameof(schema));
            }

            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentException("You must specify the name of the table you want to add.", nameof(tableName));
            }

            var tableBuilder = new TableBuilder(schema, tableName, this);
            this.tables.Add(tableBuilder);

            return tableBuilder;
        }

        public ITableBuilder AddTable(ISchemaModel schema, string tableName) => this.AddTable(schema?.Name, tableName);

        public ITableBuilder<TTable> AddTable<TTable>(string schema)
        {
            throw new NotImplementedException();
        }

        public ITableBuilder<TTable> AddTable<TTable>(ISchemaModel schema) => throw new NotImplementedException();

        public ITableBuilder<TTableDefinition> AddTable<TTableDefinition>(string name, Expression<Func<TTableDefinition>> definition)
        {
            throw new NotImplementedException();
        }
    }
}
