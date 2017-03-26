using System;
using System.Linq.Expressions;

namespace Lightmap.Modeling
{
    public interface IDataModel
    {
        ITableBuilder[] GetTables();

        ITableBuilder GetTable(string name, ISchemaModel schema = null);

        ITableBuilder<TTable> GetTable<TTable>(ISchemaModel schema = null);

        ISchemaBuilder[] GetSchemas();

        ISchemaBuilder GetSchema(string name);

        ISchemaBuilder AddSchema(string name);

        ITableBuilder AddTable(string tableName, ISchemaModel schema = null);

        ITableBuilder<TTable> AddTable<TTable>(string tableName = null, ISchemaModel schema = null) where TTable : class;

        ITableBuilder<TTableDefinition> AddTable<TTableDefinition>(Expression<Func<TTableDefinition>> definition, string tableName = null, ISchemaModel schema = null);

        //void DropTable(string tableName, ISchemaModel schema = null);
    }
}
