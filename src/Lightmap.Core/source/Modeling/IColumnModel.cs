using System;

namespace Lightmap.Modeling
{
    public interface IColumnModel
    {
        string Name { get; }

        Type DataType { get; }

        ITableModel GetOwningTable();
    }
}