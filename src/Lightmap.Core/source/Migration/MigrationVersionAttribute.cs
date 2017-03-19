using System;

namespace Lightmap.Migration
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
