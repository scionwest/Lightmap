using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public class ExpressionColumnCharacteristics<TTableData> : IExpressionColumnCharacteristics<TTableData>
    {
        public ITableModeler Table(string name)
        {
            throw new NotImplementedException();
        }

        public IDirectEntityMappedTableCharacteristics<TTable> Table<TTable>() where TTable : class
        {
            throw new NotImplementedException();
        }

        public IExpressionColumnCharacteristics<TTableData> UseForeignKey<TForeignKey>(string relatedTable, Expression<Func<TTableData, TForeignKey>> constraint)
        {
            return this;
        }

        public IExpressionColumnCharacteristics<TTableData> UsePrimaryKey<TColumn>(Expression<Func<TTableData, TColumn>> columnSelector)
        {
            return this;
        }
    }
}
