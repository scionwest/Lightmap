using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Lightmap.Modeling
{
    public class DataModel : IDataModel
    {
        private readonly List<ISchemaBuilder> schemas;
        private readonly List<ITableBuilder> tables;

        public DataModel()
        {
            this.schemas = new List<ISchemaBuilder>();
            this.tables = new List<ITableBuilder>();
        }

        public ITableBuilder[] GetTables() => this.tables.ToArray();

        public ITableBuilder GetTable(string name, ISchemaModel schema = null)
        {
            if (schema == null)
            {
                return this.tables.FirstOrDefault(table => table.TableName == name);
            }

            return this.tables.FirstOrDefault(table => table.TableName == name && table.Schema.Name == schema.Name);
        }

        public ITableBuilder<TTable> GetTable<TTable>(ISchemaModel schema = null)
        {
            if (schema == null)
            {
                return this.tables.FirstOrDefault(table => table.TableName == typeof(TTable).Name) as ITableBuilder<TTable>;
            }

            return this.tables.FirstOrDefault(table => table.TableName == typeof(TTable).Name && table.Schema?.Name == schema.Name) as ITableBuilder<TTable>;
        }

        public ISchemaBuilder[] GetSchemas() => this.schemas.ToArray();

        public ISchemaBuilder GetSchema(string name)
            => this.schemas.FirstOrDefault(schema => schema.Name == name);

        public ISchemaBuilder AddSchema(string schemaName)
        {
            var builder = new SchemaBuilder(this) { Name = schemaName };
            this.schemas.Add(builder);
            return builder;
        }

        public ITableBuilder AddTable(string tableName, ISchemaModel schema = null)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentException("You must specify the name of the table you want to add.", nameof(tableName));
            }

            var tableBuilder = new TableBuilder(schema, tableName, this);
            this.tables.Add(tableBuilder);

            return tableBuilder;
        }

        public ITableBuilder<TTable> AddTable<TTable>(string tableName = null, ISchemaModel schema = null) where TTable : class
        {
            Type tableType = typeof(TTable);
            string correctTableName = string.IsNullOrEmpty(tableName) ? tableType.Name : tableName;
            var builder = new TableBuilder<TTable>(schema, correctTableName, this);
            bool decoratedWithInclude = AttributeCache.GetAttribute<IncludeColumnOnTableAttribute>(tableType) != null;
            bool decoratedWithExclude = AttributeCache.GetAttribute<ExcludeColumnOnTableAttribute>(tableType) != null;

            // You can omit all Attributes and the API will just use all properties as columns,
            // or you can specify which properties to use as columns, or specify which properties to exclude.
            // You can't opt properties in and out at the same time. An exception will be thrown.
            if (decoratedWithInclude && decoratedWithExclude)
            {
                // TODO: Update tests to cover this use-case.
                throw new InvalidOperationException($"You can not have a model decorated with both {typeof(IncludeColumnOnTableAttribute).Name} and {typeof(ExcludeColumnOnTableAttribute).Name} attributes. If no attributes are specified, all Properties are turned into Columns. If you specify columns to include, then all columns not specified are automatically excluded. If you mark columns as being excluded, then all remaining columns are automatically included. You can not mix the two attributes.");
            }

            if (decoratedWithInclude)
            {
                foreach (PropertyInfo property in PropertyCache.GetPropertiesForType(tableType, property => AttributeCache.GetAttribute<IncludeColumnOnTableAttribute>(tableType, property) != null))
                {
                    builder.AddColumn(property.PropertyType, property.Name);
                }
            }
            else if (decoratedWithExclude)
            {
                foreach (PropertyInfo property in PropertyCache.GetPropertiesForType(tableType, property => AttributeCache.GetAttribute<ExcludeColumnOnTableAttribute>(tableType, property) == null))
                {
                    builder.AddColumn(property.PropertyType, property.Name);
                }
            }
            else
            {
                foreach (PropertyInfo property in PropertyCache.GetPropertiesForType(tableType))
                {
                    builder.AddColumn(property.PropertyType, property.Name);
                }
            }

            this.tables.Add(builder);
            return builder;
        }

        public ITableBuilder<TTableDefinition> AddTable<TTableDefinition>(Expression<Func<TTableDefinition>> definition, string tableName = null, ISchemaModel schema = null)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentException(nameof(tableName), "You must specify the name of your table when constructing it without a Type.");
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

            var builder = new TableBuilder<TTableDefinition>(schema, tableName, this);
            foreach (var columnData in columnExpression.Members.OfType<PropertyInfo>())
            {
                builder.AddColumn(columnData.PropertyType, columnData.Name);
            }

            this.tables.Add(builder);
            return builder;
        }
    }
}
