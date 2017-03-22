﻿using System;
using System.Linq.Expressions;

namespace Lightmap.Modeling
{
    internal class ColumnBuilderStronglyTyped<TTableType> : ColumnBuilder, IColumnBuilderStronglyTyped<TTableType>
    {
        private TableBuilder<TTableType> tableBuilder;

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

        public IColumnBuilderStronglyTyped<TTableType> WithForeignKey<TReferenceTable, TConstraint>(ITableBuilder<TReferenceTable> referenceTable, Expression<Func<TTableType, TReferenceTable, TConstraint>> constraint)
        {
            //base.TryAddColumnDefinition(ColumnDefinitions.ForeignKey, this.ColumnName);
            //base.TryAddColumnDefinition(ColumnDefinitions.ReferencesTable, referenceColumn.GetOwningTable().FullyQualifiedName);
            //base.TryAddColumnDefinition(ColumnDefinitions.ReferencesColumn, referenceColumn.Name);

            throw new NotImplementedException();
        }
    }
}
