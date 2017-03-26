using System;
using System.Linq.Expressions;

namespace Lightmap.Modeling
{
    internal class ColumnBuilderStronglyTyped<TTableType> : ColumnBuilderBase, IColumnBuilderStronglyTyped<TTableType>
    {
        private readonly TableBuilder<TTableType> tableBuilder;

        public ColumnBuilderStronglyTyped(string columnName, Type dataType, TableBuilder<TTableType> tableBuilder)
            : base(columnName, dataType, tableBuilder)
        {
            this.tableBuilder = tableBuilder;
            base.TryAddColumnDefinition(ColumnDefinitions.NotNull, columnName);
        }

        public ITableBuilder<TTableType> GetOwner() => this.tableBuilder;

        public IColumnBuilderStronglyTyped<TTableType> IsNullable()
        {
            base.GetColumnDefinition().Remove(ColumnDefinitions.NotNull);
            return this;
        }

        public IColumnBuilderStronglyTyped<TTableType> IsPrimaryKey()
        {
            base.TryAddColumnDefinition(ColumnDefinitions.PrimaryKey, this.ColumnName);
            return this;
        }

        public IColumnBuilderStronglyTyped<TTableType> Unique()
        {
            base.TryAddColumnDefinition(ColumnDefinitions.Unique, this.ColumnName);
            return this;
        }

        public IColumnBuilderStronglyTyped<TTableType> WithForeignKey<TColumn>(IColumnBuilder column, Expression<Func<TTableType, TColumn>> constraint)
        {
            //base.TryAddColumnDefinition(ColumnDefinitions.ForeignKey, this.ColumnName);
            //base.TryAddColumnDefinition(ColumnDefinitions.ReferencesTable, referenceColumn.GetOwningTable().FullyQualifiedName);
            //base.TryAddColumnDefinition(ColumnDefinitions.ReferencesColumn, referenceColumn.Name);

            throw new NotImplementedException();
        }

        public IColumnBuilderStronglyTyped<TTableType> WithForeignKey<TReferenceTable>(Expression<Func<TTableType, TReferenceTable, bool>> constraint)
        {
            throw new NotImplementedException();
        }

        public IColumnBuilderStronglyTyped<TTableType> WithForeignKey<TReferenceTable, TColumn>(ITableBuilder<TReferenceTable> referenceTable, Expression<Func<TTableType, TReferenceTable, TColumn>> constraint)
        {
            throw new NotImplementedException();
        }
    }
}
