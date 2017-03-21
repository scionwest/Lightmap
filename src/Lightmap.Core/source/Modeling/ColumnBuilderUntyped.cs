using System;
using System.Collections.Generic;

namespace Lightmap.Modeling
{
    internal class ColumnBuilderUntyped : IColumnBuilderUntyped
    {
        private Dictionary<string, string> columnDefinitions;
        private ITableBuilder owningTable;

        internal ColumnBuilderUntyped(string columnName, Type dataType, ITableBuilder owner)
        {
            this.owningTable = owner;

            // Default all columns to not allowing nulls.
            this.columnDefinitions = new Dictionary<string, string> { { ColumnDefinitions.NotNull, columnName } };
            this.ColumnName = columnName;
            this.ColumnDataType = dataType;
        }

        public string ColumnName { get; }

        public Type ColumnDataType { get; }

        public ITableBuilder GetOwner() => this.owningTable;

        public Dictionary<string, string> GetColumnDefinition() => this.columnDefinitions;

        public IColumnModel GetModel()
        {
            return new ColumnModel(this.ColumnName, this.ColumnDataType, this.columnDefinitions, this.owningTable.GetTableModel());
        }

        public IColumnBuilderUntyped IsPrimaryKey()
        {
            this.TryAddColumnDefinition(ColumnDefinitions.PrimaryKey, this.ColumnName);
            return this;
        }

        public IColumnBuilderUntyped WithForeignKey(IColumnModel referenceColumn)
        {
            this.TryAddColumnDefinition(ColumnDefinitions.ForeignKey, this.ColumnName);
            this.TryAddColumnDefinition(ColumnDefinitions.ReferencesTable, referenceColumn.GetOwningTable().FullyQualifiedName);
            this.TryAddColumnDefinition(ColumnDefinitions.ReferencesColumn, referenceColumn.Name);

            return this;
        }

        public IColumnBuilderUntyped IsNullable()
        {
            this.columnDefinitions.Remove(ColumnDefinitions.NotNull);
            return this;
        }

        public IColumnBuilderUntyped Unique()
        {
            this.TryAddColumnDefinition(ColumnDefinitions.Unique, this.ColumnName);
            return this;
        }

        public void TryAddColumnDefinition(string definitionKey, string definitionValue)
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
