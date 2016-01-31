using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Lightmap.Modeling
{
    public interface IColumnSelector<TTableData>
    {
        IColumnSelectorResult<TTableData> ModifyColumn(Func<TTableData, IColumnCharacteristics, bool> predicate);
    }

    public interface IColumnSelectorResult<TTableData> : IColumnSelector<TTableData>
    {
        IColumnSelectorResult<TTableData> AsPrimaryKey();

        IColumnSelectorResult<TTableData> AsForeignKey<TForeignKey>(ITableModeler constraint, Expression<Func<TTableData, TForeignKey>> definition);

        IColumnSelectorResult<TTableData> WithIndex();

        IColumnSelectorResult<TTableData> WithIndex(string name);

        IColumnSelectorResult<TTableData> NotNull();

        IColumnSelectorResult<TTableData> IsUnique();
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
