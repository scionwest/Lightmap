using System;
using System.Collections.Generic;

namespace Lightmap.Modeling
{
    public interface ITableBuilder
    {
        IDataModel CurrentDataModel { get; }

        string TableName { get; }

        ISchemaModel Schema { get; }

        Dictionary<string, string> GetTableDefinition();

        void AddDefinition(string definitionKey, string definitionValue);

        ITableModel GetTableModel();

        IColumnBuilder[] GetColumns();

        IColumnBuilderUntyped AddColumn(Type dataType, string columnName);
    }
}