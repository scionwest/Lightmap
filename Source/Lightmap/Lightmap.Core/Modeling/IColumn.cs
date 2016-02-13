using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public interface IColumn
    {
        string Name { get; }

        ITable Owner { get; }

        Type DataType { get; }

        IColumnModeler GetColumnModeler();
    }
}
