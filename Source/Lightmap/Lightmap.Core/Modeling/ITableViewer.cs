using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public interface ITableViewer
    {
        Dictionary<string, string> GetDefinition();

        Column[] GetColumns();

        Column GetColumn(string name);

        StandardTableOptions WithColumn<TDataType>(string name);

        StandardTableOptions WithColumn(Type dataType, string columnName);
    }
}
