using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public interface ITableModeler : IEntityModeler
    {
        IColumnCharacteristics WithColumn<TDataType>(string name);
    }
}
