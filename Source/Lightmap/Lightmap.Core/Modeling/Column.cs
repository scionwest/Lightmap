using System;
using System.Collections.Generic;

namespace Lightmap.Modeling
{
    public class Column
    {
        private Dictionary<string, List<string>> definition;

        public Column(Table owner, string name, Type dataType)
        {
            this.Owner = owner;
            this.Name = name;
            this.DataType = dataType;
            this.definition = new Dictionary<string, List<string>>();
        }

        public string Name { get; set; }

        public Table Owner { get; set; }

        public Type DataType { get; set; }

        public void AddDefiniton(string statementKey, string statementValue)
        {
            List<string> existingDefinitions = null;
            if (!this.definition.TryGetValue(statementKey, out existingDefinitions))
            {
                this.definition.Add(statementKey, new List<string>() { statementValue });
                return;
            }

            existingDefinitions.Add(statementValue);
        }
    }
}