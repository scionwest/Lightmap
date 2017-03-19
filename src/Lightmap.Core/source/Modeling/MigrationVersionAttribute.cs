using System;

namespace Lightmap.Modeling
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
