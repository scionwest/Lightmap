using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public interface IEntityBuilder
    {
        ITableModeler Table(string name);

        IColumnSelector<TColumns> Table<TColumns>(string name, Expression<Func<TColumns>> columnDefinitions);

        IColumnSelector<TColumns> Table<TTableName, TColumns>(Expression<Func<TTableName>> tableName, Expression<Func<TColumns>> columnDefinitions);

        IColumnSelector<TEntity> Table<TEntity>() where TEntity : class;

        IEntityModeler View(string name);

        IEntityModeler View<TEntity>();
    }
}
