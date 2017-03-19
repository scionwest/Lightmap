using System;

namespace Lightmap.Migration
{
    public interface ITableBuilder
    {
        ITableModel GetTableModel();

        ITableColumnBuilder[] GetColumns();

        IUntypedColumnBuilder AddColumn(Type dataType, string columnName);
    }
}