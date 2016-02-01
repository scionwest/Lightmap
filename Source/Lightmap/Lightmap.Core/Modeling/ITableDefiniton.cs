using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public interface ITableDefiniton
    {
        string Name { get; }

        IColumnCharacteristics GetColumn(string name);

        void RemoveColumn(IColumnCharacteristics column);
    }
}
