using System;
using Lightmap.Modeling;

namespace Lightmap.Modeling
{
    public interface ITableManager
    {
        ITableModeler GetTable(string name);

        ITableModeler GetTable<TTable>();

        ITableModeler GetTable(Func<ITableModeler, bool> predicate);
    }
}
