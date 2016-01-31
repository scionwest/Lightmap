using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public interface ITableModeler
    {
        IColumnCharacteristics WithColumn<TDataType>(string name);

        IColumnCharacteristics WithColumn(Type dataType, string columnName);

        IExpressionColumnCharacteristics<TColumns> WithColumns<TColumns>(Expression<Func<TColumns>> columnDefinition);
    }
}
