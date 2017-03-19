using System;
using System.Linq.Expressions;

namespace Lightmap.Migration
{
    public interface IDataModel
    {
        ITableBuilder[] GetTables();

        ISchemaBuilder[] GetSchemas();

        IDatabaseMigrator DataModelMigration { get; }

        ITableBuilder AddTable(string schema, string tableName);

        ITableBuilder AddTable(ISchemaModel schema, string tableName);

        ITableBuilder<TTable> AddTable<TTable>(string schema);

        ITableBuilder<TTable> AddTable<TTable>(ISchemaModel schema);

        ITableBuilder<TTableDefinition> AddTable<TTableDefinition>(string schema, string name, Expression<Func<TTableDefinition>> definition);

        ISchemaBuilder AddSchema(string name);
    }
}
