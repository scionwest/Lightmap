using System;
using System.Collections.Generic;
using System.Text;

namespace Lightmap.Migration
{
    internal class TableColumnBuilder : ITableColumnBuilder
    {
        private Dictionary<string, string> columnDefinitions;
        private ITableBuilder owningTable;

        internal TableColumnBuilder(ITableBuilder owner)
        {
            this.owningTable = owner;
            this.columnDefinitions = new Dictionary<string, string>();
        }

        public ITableColumnBuilder AsPrimaryKey()
        {
            throw new NotImplementedException();
        }

        public ITableColumn GetModel()
        {
            throw new NotImplementedException();
        }

        public ITableBuilder GetOwner() => this.owningTable;

        public Dictionary<string, string> GetTableDefinition() => this.columnDefinitions;

        public ITableColumnBuilder IsNullable()
        {
            throw new NotImplementedException();
        }

        public ITableColumnBuilder IsUniquenessRequired()
        {
            throw new NotImplementedException();
        }

        public ITableColumnBuilder WithForeignKey(ITableColumn referenceColumn)
        {
            throw new NotImplementedException();
        }
    }
}
