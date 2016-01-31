using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public interface IDatabaseModeler
    {
        IEntityBuilder Create();

        IEntityBuilder Alter();

        IEntityBuilder Drop();

        ITableModeler GetTable(string name);

        ITableModeler GetTable<T>();

        ITableModeler GetTable<TTable>(Expression<Func<TTable>> tableDefinition);
    }
}
