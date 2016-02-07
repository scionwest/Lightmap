using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lightmap.Modeling;

namespace Lightmap
{
    public interface IMigration
    {
        void Configure(DatabaseModeler modeler);

        Task Apply(IDataProvider provider);

        Task Rollback(IDataProvider provider);
    }
}
