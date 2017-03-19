using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lightmap
{
    public interface IDataProviderFactory
    {
        IDataProvider CreateProvider(DatabaseManager manager);
    }
}
