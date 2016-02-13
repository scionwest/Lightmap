using System.Collections.Generic;

namespace Lightmap.Modeling
{
    public interface IDatabaseModelBrowser
    {
        void AddTable(ITable table);

        IEnumerable<ITable> GetTables();

        bool HasTable(string name);
    }
}
