using System;

namespace Lightmap.Modeling
{
    internal class ColumnBuilderUntyped : ColumnBuilderBase, IColumnBuilderUntyped
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

        [IsNotDeadCode]
        public IColumnBuilderUntyped WithForeignKey(string schema, string table, string columnName)
        {
            this.TableBuilder.AddDefinition(ColumnDefinitions.ForeignKey, this.ColumnName);
            this.TableBuilder.AddDefinition(ColumnDefinitions.ReferencesTable, $"{schema}.{table}");
            this.TableBuilder.AddDefinition(ColumnDefinitions.ReferencesColumn, columnName);

            return this;
        }

        public IColumnBuilderUntyped WithForeignKey(IColumnModel referenceColumn)
        {
            this.TableBuilder.AddDefinition(ColumnDefinitions.ForeignKey, this.ColumnName);
            this.TableBuilder.AddDefinition(ColumnDefinitions.ReferencesSchema, referenceColumn.GetOwningTable().Schema?.Name);
            this.TableBuilder.AddDefinition(ColumnDefinitions.ReferencesTable, referenceColumn.GetOwningTable().Name);
            this.TableBuilder.AddDefinition(ColumnDefinitions.ReferencesColumn, referenceColumn.Name);

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
