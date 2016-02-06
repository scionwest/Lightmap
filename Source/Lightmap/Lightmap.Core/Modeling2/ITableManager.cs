using System;
using Lightmap.Modeling2;

namespace Lightmap.Modeling2
{
    public interface ITableManager
    {
        ITableModeler GetTable(string name);

        ITableEditor EditTable<TTable>();

        ITableModeler GetTable(Func<ITableModeler, bool> predicate);
    }
}
