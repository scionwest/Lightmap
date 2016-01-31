using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lightmap.Modeling;

namespace Lightmap.Modeling
{
    public interface IObjectDefinition
    {
        IEntityBuilder Create();
    }
}
