using System;
using System.Collections.Generic;
using System.Text;

namespace Lightmap.Migration
{
    internal class TableBuilder<TTableType> : TableBuilder, ITableBuilder<TTableType>
    {
        public TableBuilder(string schema, string tableName, IDataModel currentDataModel) 
            : base(schema, tableName, currentDataModel)
        {
        }

        public IStronglyTypedColumnBuilder<TTableType> AlterColumn<TColumn>(System.Linq.Expressions.Expression<Func<TTableType, TColumn>> columnSelect)
        {
            throw new NotImplementedException();
        }
    }
}
