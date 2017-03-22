using System;
using System.Collections.Generic;
using System.Text;

namespace Lightmap.Modeling
{
    public abstract class ColumnBuilder : IColumnBuilder
    {
        private Dictionary<string, string> columnDefinitions;

        public ColumnBuilder(string columnName, Type dataType, ITableBuilder owningTable)
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

        public virtual Dictionary<string, string> GetColumnDefinition() => this.columnDefinitions;

        public virtual void TryAddColumnDefinition(string definitionKey, string definitionValue)
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
