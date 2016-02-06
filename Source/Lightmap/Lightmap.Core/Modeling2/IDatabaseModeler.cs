using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lightmap.Modeling2
{
    public interface IDatabaseModeler : IObjectDefinition
    {
        string DatabaseName { get; }
    }
}
