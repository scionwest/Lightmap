using System;
using System.Linq.Expressions;

namespace Lightmap.Modeling
{
    public interface IColumnBuilderStronglyTyped<TTableType> : IColumnBuilder
    {
        ITableBuilder<TTableType> GetOwner();

        IColumnBuilderStronglyTyped<TTableType> IsPrimaryKey();

        IColumnBuilderStronglyTyped<TTableType> WithForeignKey(IColumnBuilder column);
        IColumnBuilderStronglyTyped<TTableType> WithForeignKey<TReferenceTable>(Expression<Func<TTableType, TReferenceTable, bool>> columnSelector, ISchemaModel schema = null);
        IColumnBuilderStronglyTyped<TTableType> WithForeignKey<TReferenceTable>(Expression<Func<TTableType, TReferenceTable, bool>> constraint, ITableBuilder<TReferenceTable> referenceTable, ISchemaModel schema = null);

        IColumnBuilderStronglyTyped<TTableType> Unique();

        IColumnBuilderStronglyTyped<TTableType> IsNullable();
    }
}
