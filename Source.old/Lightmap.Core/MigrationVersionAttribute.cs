using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lightmap
{
    public class MigrationVersionAttribute : Attribute
    {
        public MigrationVersionAttribute(int version)
        {
            this.MigrationVersion = version;
        }

        public int MigrationVersion { get; }
    }
}
