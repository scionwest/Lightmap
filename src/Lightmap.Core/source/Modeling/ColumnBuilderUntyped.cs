using System;

namespace Lightmap.Modeling
{
    internal sealed class ColumnBuilderUntyped : ColumnBuilderBase, IColumnBuilderUntyped
    {
        internal ColumnBuilderUntyped(string columnName, Type dataType, TableBuilder owner) : base(columnName, dataType, owner)
        {
            base.AddColumnDefinition(ColumnDefinitions.NotNull, columnName);
        }

        public ITableBuilder GetOwner() => base.TableBuilder;

        public IColumnBuilderUntyped IsPrimaryKey()
        {
            base.AddColumnDefinition(ColumnDefinitions.PrimaryKey, this.ColumnName);
            return this;
        }
        
        public IColumnBuilderUntyped WithForeignKey(string table, string columnName, ISchemaModel schema = null)
        {
            base.AddColumnDefinition(ColumnDefinitions.ForeignKey, this.ColumnName);
            base.AddColumnDefinition(ColumnDefinitions.ReferencesColumn, columnName);
            base.AddColumnDefinition(ColumnDefinitions.ReferencesTable, $"{table}");
            if (schema != null)
            {
                base.AddColumnDefinition(ColumnDefinitions.ReferencesSchema, schema.Name);
            }

            return this;
        }

        public IColumnBuilderUntyped WithForeignKey(IColumnModel referenceColumn)
        {
            ITableModel referenceTable = referenceColumn.GetOwningTable();
            this.WithForeignKey(referenceTable.Name, referenceColumn.Name, referenceTable.Schema);
            return this;
        }

        public IColumnBuilderUntyped IsNullable()
        {
            base.GetColumnDefinition().Remove(ColumnDefinitions.NotNull);
            return this;
        }

        public IColumnBuilderUntyped Unique()
        {
            base.AddColumnDefinition(ColumnDefinitions.Unique, this.ColumnName);
            return this;
        }
    }
}
