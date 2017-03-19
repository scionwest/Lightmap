using System;
using System.Collections.Generic;
using System.Text;

namespace Lightmap.Migration
{
    public interface IColumnBuilderUntyped : IColumnBuilder
    {
        ITableBuilder GetOwner();

        IColumnBuilder IsPrimaryKey();

        IColumnBuilder WithForeignKey(IColumnModel referenceColumn);

        IColumnBuilder IsUniquenessRequired();

        IColumnBuilder IsNullable();
    }
}
