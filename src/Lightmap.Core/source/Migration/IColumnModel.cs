using System;

namespace Lightmap.Migration
{
    public interface IColumnModel
    {
        string Name { get; }

        Type DataType { get; }

        ITableModel GetOwningTable();
    }
}