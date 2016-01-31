using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public interface IDirectEntityMappedTableCharacteristics<TTable> : IDatabaseModeler
    {
        IDirectEntityMappedTableCharacteristics<TTable> UsePrimaryKey<TColumn>(System.Linq.Expressions.Expression<Func<TTable, TColumn>> columnSelector);

        IDirectEntityMappedTableCharacteristics<TTable> UseForeignKey<TForeignKey>(string relatedTable, System.Linq.Expressions.Expression<Func<TTable, TForeignKey>> constraint);

        IDirectEntityMappedTableCharacteristics<TTable> IgnoreColumn<TColumn>(Expression<Func<TTable, TColumn>> columnSelector);
    }
}