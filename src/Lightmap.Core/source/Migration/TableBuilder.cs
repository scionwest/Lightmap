using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lightmap.Migration
{
    internal class TableBuilder : ITableBuilder
    {
        private IDataModel dataModel;

        private ISchemaBuilder schemaBuilder;
        private List<ITableColumnBuilder> columnBuilders;
        private string tableName;

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

            this.columnBuilders = new List<ITableColumnBuilder>();
            this.dataModel = currentDataModel;
            this.tableName = tableName;

            ISchemaBuilder matchedSchema = this.dataModel.GetSchemas().FirstOrDefault(schemaModel => schemaModel.Name == schema);
            if (matchedSchema == null)
            {
                // TODO: Make this configurable so we throw an exception instead of auto-creating, if that's the desired behavior.
                // if (!someConfig.AutoCreateMissingSchema) throw new InvalidOperationException($"The schema specified for the {tableName} does not exist.");
                matchedSchema = this.dataModel.AddSchema(schema);
            }

            this.schemaBuilder = matchedSchema;
        }

        public ITableModel GetTableModel()
        {
            ITableColumn[] columns = this.GetColumns().Select(columnBuilder => columnBuilder.GetModel()).ToArray();
            var tableModel = new TableModel(this.schemaBuilder.GetSchemaModel(), this.tableName, columns);
            return tableModel;
        }

        public IUntypedColumnBuilder AddColumn(Type dataType, string columnName)
        {
            throw new NotImplementedException();
        }

        public ITableColumnBuilder[] GetColumns()
        {
            throw new NotImplementedException();
        }
    }
}
