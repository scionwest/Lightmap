using System;
using System.Collections.Generic;

namespace Lightmap.Modeling
{
    public interface IColumnBuilder
    {
        Dictionary<string, string> GetTableDefinition();

        string ColumnName { get; }

        Type ColumnDataType { get; }

        IColumnModel GetModel();
    }
}