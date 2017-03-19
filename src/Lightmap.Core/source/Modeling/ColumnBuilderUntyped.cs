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

        public Dictionary<string, string> GetTableDefinition() => this.columnDefinitions;

        public IColumnModel GetModel()
        {
            return new ColumnModel(this.ColumnName, this.ColumnDataType, this.columnDefinitions, this.owningTable.GetTableModel());
        }

        public IColumnBuilder IsPrimaryKey()
        {
            this.TryAddDefinition(ColumnDefinitions.PrimaryKey, this.ColumnName);
            return this;
        }

        public IColumnBuilder WithForeignKey(IColumnModel referenceColumn)
        {
            this.TryAddDefinition(ColumnDefinitions.ForeignKey, this.ColumnName);
            this.TryAddDefinition(ColumnDefinitions.ReferencesTable, referenceColumn.GetOwningTable().FullyQualifiedName);
            this.TryAddDefinition(ColumnDefinitions.ReferencesColumn, referenceColumn.Name);

            return this;
        }

        public IColumnBuilder IsNullable()
        {
            this.columnDefinitions.Remove(ColumnDefinitions.NotNull);
            return this;
        }

        public IColumnBuilder IsUniquenessRequired()
        {
            this.TryAddDefinition(ColumnDefinitions.Unique, this.ColumnName);
            return this;
        }

        private void TryAddDefinition(string key, string value)
        {
            if (this.columnDefinitions.TryGetValue(key, out var result))
            {
                this.columnDefinitions[key] = value;
                return;
            }

            this.columnDefinitions.Add(key, value);
        }
    }
}
