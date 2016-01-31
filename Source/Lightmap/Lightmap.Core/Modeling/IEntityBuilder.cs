using System;
using System.Linq.Expressions;

namespace Lightmap.Modeling
{
    public interface IEntityBuilder
    {
        ITableModeler Table(string name);

        IDirectEntityMappedTableCharacteristics<TTable> Table<TTable>() where TTable : class;

        //IColumnSelector<TColumns> Table<TTable, TColumns>(Expression<Func<TColumns>> columnDefinitions)
        //    where TTable : class;

        //IColumnSelector<TColumns> Table<TColumns>(string name, Expression<Func<TColumns>> columnDefinitions);
    }
}
