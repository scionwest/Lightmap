using System;

namespace Lightmap.Modeling
{
    internal class ColumnBuilderUntyped : ColumnBuilderBase, IColumnBuilderUntyped
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
            this.TableBuilder.TryAddDefinition(ColumnDefinitions.ForeignKey, this.ColumnName);
            this.TableBuilder.TryAddDefinition(ColumnDefinitions.ReferencesTable, $"{schema}.{table}");
            this.TableBuilder.TryAddDefinition(ColumnDefinitions.ReferencesColumn, columnName);

            return this;
        }

        public IColumnBuilderUntyped WithForeignKey(IColumnModel referenceColumn)
        {
            this.TableBuilder.TryAddDefinition(ColumnDefinitions.ForeignKey, this.ColumnName);
            this.TableBuilder.TryAddDefinition(ColumnDefinitions.ReferencesSchema, referenceColumn.GetOwningTable().Schema?.Name);
            this.TableBuilder.TryAddDefinition(ColumnDefinitions.ReferencesTable, referenceColumn.GetOwningTable().Name);
            this.TableBuilder.TryAddDefinition(ColumnDefinitions.ReferencesColumn, referenceColumn.Name);

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
