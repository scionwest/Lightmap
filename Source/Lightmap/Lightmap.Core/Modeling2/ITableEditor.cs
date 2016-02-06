using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lightmap.Modeling2
{
    public interface ITableEditor
    {
        ITableEditor RemoveColumn(string name);
    }
}
