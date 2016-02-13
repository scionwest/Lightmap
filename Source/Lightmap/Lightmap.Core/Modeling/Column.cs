using System;
using System.Collections.Generic;

namespace Lightmap.Modeling
{
    public class Column : IColumn, IColumnModeler
    {
        private Dictionary<string, string> definition;

        private Table owner;

        public Column(Table owner, string name, Type dataType)
        {
            this.owner = owner;
            this.Name = name;
            this.DataType = dataType;
            this.definition = new Dictionary<string, string>();
        }

        public string Name { get; }

        public ITable Owner => this.owner;

        public Type DataType { get; }

        public IColumnModeler GetColumnModeler()
        {
            return this;
        }

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

        public Dictionary<string, string> GetDefinitions()
        {
            return this.definition;
        }

        public void RemoveDefinition(string statementKey)
        {
            this.definition.Remove(statementKey);
        }
    }
}