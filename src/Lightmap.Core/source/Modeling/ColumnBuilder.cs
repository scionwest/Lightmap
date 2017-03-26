using System;
using System.Collections.Generic;
using System.Text;

namespace Lightmap.Modeling
{
    public abstract class ColumnBuilderBase : IColumnBuilder
    {
        private readonly Dictionary<string, string> columnDefinitions;

        public ColumnBuilderBase(string columnName, Type dataType, ITableBuilder owningTable)
        {
            this.ColumnName = columnName;
            this.ColumnDataType = dataType;
            this.columnDefinitions = new Dictionary<string, string>();
            this.TableBuilder = owningTable;
        }

        public ITableBuilder TableBuilder { get; protected set; }

        public string ColumnName { get; }

        public Type ColumnDataType { get; }

        public virtual IColumnModel GetModel()
            => new ColumnModel(this.ColumnName, this.ColumnDataType, this.GetColumnDefinition(), this.TableBuilder.GetTableModel());

        public Dictionary<string, string> GetColumnDefinition() => this.columnDefinitions;

        public void AddColumnDefinition(string definitionKey, string definitionValue)
        {
            if (this.columnDefinitions.TryGetValue(definitionKey, out var result))
            {
                this.columnDefinitions[definitionKey] = definitionValue;
                return;
            }

            this.columnDefinitions.Add(definitionKey, definitionValue);
        }
    }
}
