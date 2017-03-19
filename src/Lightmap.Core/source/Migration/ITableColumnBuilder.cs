using System.Collections.Generic;

namespace Lightmap.Migration
{
    public interface ITableColumnBuilder
    {
        Dictionary<string, string> GetTableDefinition();

        ITableColumn GetModel();
    }
}