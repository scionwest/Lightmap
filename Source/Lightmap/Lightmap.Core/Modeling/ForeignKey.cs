using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public class ForeignKey
    {
        ITableModeler Relationship { get; }

        IColumnCharacteristics Constraint { get; }

        string name { get; }
    }
}