using System;
using System.Collections.Generic;
using System.Linq;

namespace Lightmap.Modeling
{
    internal class TableBuilder : ITableBuilder
    {
        private ISchemaBuilder schemaBuilder;
        private List<IColumnBuilder> columnBuilders;
        private Dictionary<string, string> tableDefinition;

        public TableBuilder(string schema, string tableName, IDataModel currentDataModel)
        {
            if (string.IsNullOrEmpty(schema))
            {
                throw new ArgumentException("You must specify the schema that owns the table being added.", nameof(schema));
            }

            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentException("You must specify the name of the table you want to add.", nameof(tableName));
            }

            this.tableDefinition = new Dictionary<string, string>();
            this.columnBuilders = new List<IColumnBuilder>();
            this.CurrentDataModel = currentDataModel;

            ISchemaBuilder matchedSchema = this.CurrentDataModel.GetSchemas().FirstOrDefault(schemaModel => schemaModel.Name == schema);
            if (matchedSchema == null)
            {
                // TODO: Make this configurable so we throw an exception instead of auto-creating, if that's the desired behavior.
                // if (!someConfig.AutoCreateMissingSchema) throw new InvalidOperationException($"The schema specified for the {tableName} does not exist.");
                matchedSchema = this.CurrentDataModel.AddSchema(schema);
            }

            this.schemaBuilder = matchedSchema;
            this.TableName = tableName;
            this.Schema = schema;
        }

        public IDataModel CurrentDataModel { get; }

        public string TableName { get; }

        public string Schema { get; }

        public Dictionary<string, string> GetTableDefinition() => this.tableDefinition;

        public void TryAddDefinition(string definitionKey, string definitionValue)
        {
            if (this.tableDefinition.TryGetValue(definitionKey, out var value))
            {
                this.tableDefinition[definitionKey] = definitionValue;
                return;
            }

            this.tableDefinition.Add(definitionKey, definitionValue);
        }

        public ITableModel GetTableModel()
        {
            var tableModel = new TableModel(this.schemaBuilder.GetSchemaModel(), this.TableName, this);
            return tableModel;
        }

        public IColumnBuilderUntyped AddColumn(Type dataType, string columnName)
        {
            if (dataType == null)
            {
                throw new ArgumentNullException(nameof(dataType), "You must provide the data type that this column represents. The data type may only be primitive value types");
            }

            if (string.IsNullOrEmpty(columnName))
            {
                throw new ArgumentNullException(nameof(columnName), "You can not add a nameless column.");
            }

            var builder = new ColumnBuilderUntyped(columnName, dataType, this);
            this.columnBuilders.Add(builder);
            return builder;
        }

        public IColumnBuilderUntyped AlterColumn(Func<IColumnModel, bool> columnSelector)
        {
            return this.columnBuilders.OfType<IColumnBuilderUntyped>()
                .FirstOrDefault(builder => columnSelector(builder.GetModel()));
        }

        public IColumnBuilder[] GetColumns() => this.columnBuilders.ToArray();
    }
}
