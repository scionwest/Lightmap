using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Lightmap.Modeling
{
    public interface IColumnSelector<TTableData>
    {
        IColumnCharacteristics ModifyColumn(Func<TTableData, IEnumerable<IColumnCharacteristics>, IColumnCharacteristics> selector);
    }

    public interface IColumnCharacteristics : ITableModeler
    {
        Type DataType { get; }

        IColumnCharacteristics AsPrimaryKey();

        IColumnCharacteristics AsForeignKey(ITableModeler constraint, string name);

        IColumnCharacteristics WithIndex();

        IColumnCharacteristics WithIndex(string name);

        IColumnCharacteristics NotNull();

        IColumnCharacteristics IsUnique();
        IColumnCharacteristics WithDefaultValue(object value);

        IColumnCharacteristics WithDefaultValue<TDataType>(TDataType value);

        //IColumnCharacteristics WithTrigger(string name);
    }
}
