using System;
using System.Linq.Expressions;

namespace Lightmap.Modeling
{
    public interface ITableBuilder<TTableType> : ITableBuilder
    {
        IColumnBuilderStronglyTyped<TTableType> AlterColumn<TColumn>(Expression<Func<TTableType, TColumn>> columnSelect);
    }
}
