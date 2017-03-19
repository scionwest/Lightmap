using System;
using System.Collections.Generic;
using System.Text;

namespace Lightmap.Migration
{
    public interface IUntypedColumnBuilder : ITableColumnBuilder
    {
        ITableBuilder GetOwner();

        ITableColumnBuilder AsPrimaryKey();

        ITableColumnBuilder WithForeignKey(ITableColumn referenceColumn);

        ITableColumnBuilder IsUniquenessRequired();

        ITableColumnBuilder IsNullable();
    }
}
