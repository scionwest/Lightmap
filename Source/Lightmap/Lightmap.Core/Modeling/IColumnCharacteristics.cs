using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public interface IColumnCharacteristics : ITableModeler
    {
        IColumnCharacteristics AsPrimaryKey();

        IColumnCharacteristics AsForeignKey(ITableModeler constraint, string name);

        IColumnCharacteristics WithIndex();

        IColumnCharacteristics WithIndex(string name);

        IColumnCharacteristics NotNull();

        IColumnCharacteristics IsUnique();

        //IColumnCharacteristics WithTrigger(string name);
    }

    public interface IColumnCharacteristics<TDataType> : IColumnCharacteristics
    {
        IColumnCharacteristics WithDefaultValue(TDataType value);
    }
}
