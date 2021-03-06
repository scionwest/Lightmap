﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public interface ITableModeler
    {
        void AddDefinition(string statementKey, string statementValue);

        Dictionary<string, string> GetDefinitions();

        string GetDefinition(string statementKey);

        void RemoveDefinition(string statementKey);

        IColumn[] GetColumns();

        IColumn GetColumn(string name);

        StandardTableOptions WithColumn<TDataType>(string name);

        StandardTableOptions WithColumn(Type dataType, string columnName);
    }
}
