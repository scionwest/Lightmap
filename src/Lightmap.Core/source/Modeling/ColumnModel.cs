using System;
using System.Collections.Generic;

namespace Lightmap.Modeling
{
    internal class ColumnModel : IColumnModel
    {
        private readonly Dictionary<string, string> columnDefinitions;
        private readonly ITableModel owner;

        public ColumnModel(string columnName, Type columnDataType, Dictionary<string, string> columnDefinitions, ITableModel owner)
        {
            this.Name = columnName;
            this.DataType = columnDataType;
            this.columnDefinitions = columnDefinitions;
            this.owner = owner;
        }

        public string Name { get; }

        public Type DataType { get; }

        public ITableModel GetOwningTable() => this.owner;
    }
}