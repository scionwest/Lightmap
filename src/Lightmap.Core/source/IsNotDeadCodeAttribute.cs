using System;
using System.Collections.Generic;
using System.Text;

namespace Lightmap
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class IsNotDeadCodeAttribute : Attribute
    {
    }
}
