using System;

namespace Lightmap.Modeling
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class MigrationVersionAttribute : Attribute
    {
        public MigrationVersionAttribute(int version)
        {
            this.MigrationVersion = version;
        }

        public int MigrationVersion { get; }
    }
}
