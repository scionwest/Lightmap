using System;
using System.Collections.Generic;

namespace Lightmap.Modeling
{
    public interface IColumnBuilder
    {
        ITableBuilder TableBuilder { get; }

        Dictionary<string, string> GetColumnDefinition();

        void TryAddColumnDefinition(string definitionKey, string definitionValue);

        string ColumnName { get; }

        Type ColumnDataType { get; }

        IColumnModel GetModel();
    }
}