using System;
using System.Collections.Generic;
using System.Text;

namespace Lightmap.Modeling
{
    internal class ColumnBuilderStronglyTyped<TTableType> : IColumnBuilderStronglyTyped<TTableType>
    {
        private string columnName;
        private Type dataType;
        private TableBuilder<TTableType> tableBuilder;

        public ColumnBuilderStronglyTyped(string columnName, Type dataType, TableBuilder<TTableType> tableBuilder)
        {
            this.columnName = columnName;
            this.dataType = dataType;
            this.tableBuilder = tableBuilder;
        }

        public ITableBuilder<TTableType> GetOwner()
        {
            throw new NotImplementedException();
        }

        public IColumnBuilderStronglyTyped<TTableType> IsNullable()
        {
            throw new NotImplementedException();
        }

        public IColumnBuilderStronglyTyped<TTableType> IsPrimaryKey()
        {
            throw new NotImplementedException();
        }

        public IColumnBuilderStronglyTyped<TTableType> IsUniquenessRequired()
        {
            throw new NotImplementedException();
        }

        public IColumnBuilderStronglyTyped<TTableType> WithForeignKey<TReferenceTable, TConstraint>(ITableBuilder<TReferenceTable> referenceTable, System.Linq.Expressions.Expression<Func<TTableType, TReferenceTable, TConstraint>> constraint)
        {
            throw new NotImplementedException();
        }
    }
}
