using System;
using System.Linq.Expressions;

namespace Lightmap.Modeling
{
    public interface IColumnBuilderStronglyTyped<TTableType>
    {
        ITableBuilder<TTableType> GetOwner();

        IColumnBuilderStronglyTyped<TTableType> IsPrimaryKey();

        IColumnBuilderStronglyTyped<TTableType> WithForeignKey<TReferenceTable, TConstraint>(ITableBuilder<TReferenceTable> referenceTable, Expression<Func<TTableType, TReferenceTable, TConstraint>> constraint);

        IColumnBuilderStronglyTyped<TTableType> Unique();

        IColumnBuilderStronglyTyped<TTableType> IsNullable();
    }
}
