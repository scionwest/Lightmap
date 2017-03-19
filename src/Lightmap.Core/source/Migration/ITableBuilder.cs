using System;

namespace Lightmap.Migration
{
    public interface ITableBuilder
    {
        IDataModel CurrentDataModel { get; }

        string TableName { get; }

        string Schema { get; }

        ITableModel GetTableModel();

        IColumnBuilder[] GetColumns();

        IColumnBuilderUntyped AddColumn(Type dataType, string columnName);
    }
}