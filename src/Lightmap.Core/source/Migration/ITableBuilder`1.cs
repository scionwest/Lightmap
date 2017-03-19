using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Lightmap.Migration
{
    public interface ITableBuilder<TTableType> : ITableBuilder
    {
        IColumnBuilderStronglyTyped<TTableType> AlterColumn<TColumn>(Expression<Func<TTableType, TColumn>> columnSelect);
    }
}
