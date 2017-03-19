using System;
using System.Collections.Generic;

namespace Lightmap.Migration
{
    public interface ITableColumnBuilder
    {
        Dictionary<string, string> GetTableDefinition();

        string ColumnName { get; }

        Type ColumnDataType { get; }

        ITableColumn GetModel();
    }
}