using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public interface IColumnModeler
    {
        void AddDefinition(string statementKey, string statementValue);

        string GetDefinition(string statementKey);

        Dictionary<string, string> GetDefinitions();

        void RemoveDefinition(string statementKey);
    }
}
