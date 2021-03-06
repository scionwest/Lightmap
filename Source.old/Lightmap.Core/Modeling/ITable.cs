﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public interface ITable
    {
        string Name { get; }

        ITableModeler GetTableModeler();

        IDatabaseModelBrowser GetDatabaseModeler();

        StandardTableOptions WithColumn<TDataType>(string name);

        StandardTableOptions WithColumn(Type dataType, string columnName);
    }

    public interface ITable<TTableDefinition> : ITable
    {
        StronglyTypedTableOptions<TTableDefinition> GetColumn<TColumn>(Expression<Func<TTableDefinition, TColumn>> columnSelector);
    }
}
