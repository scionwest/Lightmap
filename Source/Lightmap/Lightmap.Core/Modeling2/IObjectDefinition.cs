using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lightmap.Modeling2;

namespace Lightmap.Modeling2
{
    public interface IObjectDefinition
    {
        IEntityBuilder Create();
    }
}
