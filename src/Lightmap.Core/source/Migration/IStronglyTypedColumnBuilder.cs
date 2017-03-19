using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Lightmap.Migration
{
    public interface IStronglyTypedColumnBuilder<TTableType>
    {
        ITableBuilder<TTableType> GetOwner();

        IStronglyTypedColumnBuilder<TTableType> AsPrimaryKey();

        IStronglyTypedColumnBuilder<TTableType> WithForeignKey<TReferenceTable, TConstraint>(ITableBuilder<TReferenceTable> referenceTable, Expression<Func<TTableType, TReferenceTable, TConstraint>> constraint);

        IStronglyTypedColumnBuilder<TTableType> IsUniquenessRequired();

        IStronglyTypedColumnBuilder<TTableType> IsNullable();
    }
}
