using System;
using System.Collections.Generic;
using System.Linq;

namespace Lightmap.Modeling
{
    internal class TableBuilder : ITableBuilder
    {
        private readonly List<IColumnBuilder> columnBuilders;
        private readonly Dictionary<string, string> tableDefinition;

        public TableBuilder(ISchemaModel schema, string tableName, IDataModel currentDataModel)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentException("You must specify the name of the table you want to add.", nameof(tableName));
            }

            this.tableDefinition = new Dictionary<string, string>();
            this.columnBuilders = new List<IColumnBuilder>();

            this.CurrentDataModel = currentDataModel;
            this.Schema = schema;
            this.TableName = tableName;
        }

        public IDataModel CurrentDataModel { get; }

        public string TableName { get; }

        public ISchemaModel Schema { get; }

        public Dictionary<string, string> GetTableDefinition() => this.tableDefinition;

        public void AddDefinition(string definitionKey, string definitionValue)
        {
            if (string.IsNullOrEmpty(definitionKey) || string.IsNullOrEmpty(definitionValue))
            {
                return;
            }

            if (this.tableDefinition.TryGetValue(definitionKey, out var value))
            {
                this.tableDefinition[definitionKey] = definitionValue;
                return;
            }

            this.tableDefinition.Add(definitionKey, definitionValue);
        }

        public ITableModel GetTableModel()
        {
            var tableModel = new TableModel(this.Schema, this.TableName, this);
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
