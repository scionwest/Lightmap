﻿using System;
using System.Linq.Expressions;

namespace Lightmap.Modeling
{
    public interface IDataModel
    {
        ITableBuilder[] GetTables();

        ISchemaBuilder[] GetSchemas();

        ITableBuilder AddTable(string schemaName, string tableName);

        ITableBuilder AddTable(ISchemaModel schema, string tableName);

        ITableBuilder<TTable> AddTable<TTable>(string schemaName) where TTable : class;

        ITableBuilder<TTable> AddTable<TTable>(ISchemaModel schema) where TTable : class;

        ITableBuilder<TTableDefinition> AddTable<TTableDefinition>(string schema, string name, Expression<Func<TTableDefinition>> definition);

        ISchemaBuilder AddSchema(string name);

        void DropTable(string schemaName, string tableName);
    }
}
