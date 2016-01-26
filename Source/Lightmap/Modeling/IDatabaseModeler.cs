﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public interface IDatabaseModeler
    {
        IEntityBuilder Create();

        IEntityBuilder Alter();

        IEntityBuilder Drop();
    }
}
