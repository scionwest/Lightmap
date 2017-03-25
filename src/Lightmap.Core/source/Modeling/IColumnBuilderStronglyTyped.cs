using System;
using System.Linq.Expressions;

namespace Lightmap.Modeling
{
    public interface IColumnBuilderStronglyTyped<TTableType> : IColumnBuilder
    {
        ITableBuilder<TTableType> GetOwner();

        IColumnBuilderStronglyTyped<TTableType> IsPrimaryKey();

        IColumnBuilderStronglyTyped<TTableType> WithForeignKey<TColumn>(IColumnBuilder column, Expression<Func<TTableType, TColumn>> constraint);

        IColumnBuilderStronglyTyped<TTableType> WithForeignKey<TReferenceTable>(Expression<Func<TTableType, TReferenceTable, bool>> constraint);
        IColumnBuilderStronglyTyped<TTableType> WithForeignKey<TReferenceTable, TColumn>(ITableBuilder<TReferenceTable> referenceTable, Expression<Func<TTableType, TReferenceTable, TColumn>> constraint);

        IColumnBuilderStronglyTyped<TTableType> Unique();

        IColumnBuilderStronglyTyped<TTableType> IsNullable();
    }
}
