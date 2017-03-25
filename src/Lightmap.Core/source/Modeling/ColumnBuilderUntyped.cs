using System;

namespace Lightmap.Modeling
{
    internal class ColumnBuilderUntyped : ColumnBuilder, IColumnBuilderUntyped
    {
        internal ColumnBuilderUntyped(string columnName, Type dataType, TableBuilder owner) : base(columnName, dataType, owner)
        {
            base.TryAddColumnDefinition(ColumnDefinitions.NotNull, columnName);
        }

        public ITableBuilder GetOwner() => base.TableBuilder;

        public IColumnBuilderUntyped IsPrimaryKey()
        {
            base.TryAddColumnDefinition(ColumnDefinitions.PrimaryKey, this.ColumnName);
            return this;
        }

        public IColumnBuilderUntyped WithForeignKey(string schema, string table, string columnName)
        {
            base.TryAddColumnDefinition(ColumnDefinitions.ForeignKey, this.ColumnName);
            base.TryAddColumnDefinition(ColumnDefinitions.ReferencesTable, $"{schema}.{table}");
            base.TryAddColumnDefinition(ColumnDefinitions.ReferencesColumn, columnName);

            return this;
        }

        public IColumnBuilderUntyped WithForeignKey(IColumnModel referenceColumn)
        {
            base.TryAddColumnDefinition(ColumnDefinitions.ForeignKey, this.ColumnName);
            base.TryAddColumnDefinition(ColumnDefinitions.ReferencesSchema, referenceColumn.GetOwningTable().Schema?.Name);
            base.TryAddColumnDefinition(ColumnDefinitions.ReferencesTable, referenceColumn.GetOwningTable().Name);
            base.TryAddColumnDefinition(ColumnDefinitions.ReferencesColumn, referenceColumn.Name);

            return this;
        }

        public IColumnBuilderUntyped IsNullable()
        {
            base.GetColumnDefinition().Remove(ColumnDefinitions.NotNull);
            return this;
        }

        public IColumnBuilderUntyped Unique()
        {
            base.TryAddColumnDefinition(ColumnDefinitions.Unique, this.ColumnName);
            return this;
        }
    }
}
