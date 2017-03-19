﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Lightmap.Migration
{
    public class DataModel : IDataModel
    {
        private List<ISchemaBuilder> schemas;
        private List<ITableBuilder> tables;

        public DataModel(IDatabaseMigrator migrator)
        {
            if (migrator == null)
            {
                throw new ArgumentNullException(nameof(migrator), "You can not provide the data model with a null migrator.");
            }

            this.DataModelMigration = migrator;

            this.schemas = new List<ISchemaBuilder>();
            this.tables = new List<ITableBuilder>();
        }

        public IDatabaseMigrator DataModelMigration { get; }

        public ITableBuilder[] GetTables() => this.tables.ToArray();

        public ISchemaBuilder[] GetSchemas() => this.schemas.ToArray();

        public ISchemaBuilder AddSchema(string schemaName)
        {
            var builder = new SchemaBuilder { Name = schemaName };
            this.schemas.Add(builder);
            return builder;
        }

        public ITableBuilder AddTable(string schemaName, string tableName)
        {
            if (string.IsNullOrEmpty(schemaName))
            {
                throw new ArgumentException("You must specify the schema that owns the table being added.", nameof(schemaName));
            }

            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentException("You must specify the name of the table you want to add.", nameof(tableName));
            }

            var tableBuilder = new TableBuilder(schemaName, tableName, this);
            this.tables.Add(tableBuilder);

            return tableBuilder;
        }

        public ITableBuilder AddTable(ISchemaModel schema, string tableName) => this.AddTable(schema?.Name, tableName);

        public ITableBuilder<TTable> AddTable<TTable>(string schemaName)
        {
            if (string.IsNullOrEmpty(schemaName))
            {
                throw new ArgumentException(nameof(schemaName), "You must specify the name of hte schema that the new Table belongs to.");
            }

            var builder = new TableBuilder<TTable>(schemaName, typeof(TTable).Name, this);
            this.tables.Add(builder);
            return builder;
        }

        public ITableBuilder<TTable> AddTable<TTable>(ISchemaModel schema) => this.AddTable<TTable>(schema?.Name);

        public ITableBuilder<TTableDefinition> AddTable<TTableDefinition>(string schemaName, string name, Expression<Func<TTableDefinition>> definition)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(nameof(name), "You must specify the name of your table when constructing it without a Type.");
            }

            if (definition == null)
            {
                throw new ArgumentException(nameof(definition), "You must provide a table definition.");
            }

            var columnExpression = definition.Body as NewExpression;
            if (columnExpression == null)
            {
                throw new NotSupportedException($"The {definition.Body.NodeType.GetType().Name} expression used in the definition is not supported. You must create and return an anonymous Type.");
            }

            var builder = new TableBuilder<TTableDefinition>(schemaName, name, this);
            foreach(var columnData in columnExpression.Members.OfType<PropertyInfo>())
            {
                builder.AddColumn(columnData.PropertyType, columnData.Name);
            }

            this.tables.Add(builder);
            return builder;
        }
    }
}
