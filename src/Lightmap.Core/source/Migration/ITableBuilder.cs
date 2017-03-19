using System;

namespace Lightmap.Migration
{
    public interface ITableBuilder
    {
        string TableName { get; }

        string Schema { get; }

        ITableModel GetTableModel();

        ITableColumnBuilder[] GetColumns();

        IUntypedColumnBuilder AddColumn(Type dataType, string columnName);
    }
}