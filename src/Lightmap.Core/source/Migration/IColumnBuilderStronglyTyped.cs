using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Lightmap.Migration
{
    public interface IColumnBuilderStronglyTyped<TTableType>
    {
        ITableBuilder<TTableType> GetOwner();

        IColumnBuilderStronglyTyped<TTableType> IsPrimaryKey();

        IColumnBuilderStronglyTyped<TTableType> WithForeignKey<TReferenceTable, TConstraint>(ITableBuilder<TReferenceTable> referenceTable, Expression<Func<TTableType, TReferenceTable, TConstraint>> constraint);

        IColumnBuilderStronglyTyped<TTableType> IsUniquenessRequired();

        IColumnBuilderStronglyTyped<TTableType> IsNullable();
    }
}
