using System;
using System.Linq;
using System.Linq.Expressions;

namespace Lightmap.Modeling
{
    internal sealed class TableBuilder<TTableType> : TableBuilder, ITableBuilder<TTableType>
    {
        public TableBuilder(ISchemaModel schema, string tableName, IDataModel currentDataModel) 
            : base(schema, tableName, currentDataModel)
        {
        }

        public IColumnBuilderStronglyTyped<TTableType> AlterColumn<TColumn>(Expression<Func<TTableType, TColumn>> columnSelect)
        {
            var memberExpression = columnSelect.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new NotSupportedException("The selector provided is not supported. You must return a property that represents a column from the table.");
            }

            string columnName = memberExpression.Member.Name;
            Type dataType = memberExpression.Member.DeclaringType;

            var columnToAlter = base.GetColumns()
                .FirstOrDefault(column => column.ColumnName == columnName) as IColumnBuilderStronglyTyped<TTableType>;

            // We have never altered the column - so just create it.
            if (columnToAlter == null)
            {
                columnToAlter = new ColumnBuilderStronglyTyped<TTableType>(columnName, dataType, this);
            }

            return columnToAlter;
        }
    }
}
