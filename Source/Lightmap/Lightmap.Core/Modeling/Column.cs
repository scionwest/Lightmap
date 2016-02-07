using System;
using System.Collections.Generic;

namespace Lightmap.Modeling
{
    public class Column
    {
        private Dictionary<string, string> definition;

        public Column(Table owner, string name, Type dataType)
        {
            this.Owner = owner;
            this.Name = name;
            this.DataType = dataType;
            this.definition = new Dictionary<string, string>();
        }

        public string Name { get; set; }

        public Table Owner { get; set; }

        public Type DataType { get; set; }

        public void AddDefinition(string statementKey, string statementValue)
        {
            string existingDefinition = null;
            if (!this.definition.TryGetValue(statementKey, out existingDefinition))
            {
                this.definition.Add(statementKey, statementValue);
                return;
            }

            existingDefinition = statementValue;
        }

        public string GetDefinition(string statementKey)
        {
            string existingDefiniton = null;
            this.definition.TryGetValue(statementKey, out existingDefiniton);
            return existingDefiniton;
        }

        public void RemoveDefinition(string statementKey)
        {
            this.definition.Remove(statementKey);
        }
    }
}