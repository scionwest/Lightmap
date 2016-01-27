using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public interface IEntityBuilder
    {
        ITableModeler Table(string name);

        ITableModeler Table<TEntity>();

        IEntityModeler View(string name);

        IEntityModeler View<TEntity>();
    }
}
