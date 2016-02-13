using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Lightmap.Modeling
{
    public class Table : ITable, ITableModeler
    {
        private IDatabaseModelBrowser databaseModeler;

        private Dictionary<string, Column> columns;

        private Dictionary<string, string> definition;

        public Table(IDatabaseModelBrowser modeler, string name)
        {
            this.Name = name;
            this.databaseModeler = modeler;
            this.definition = new Dictionary<string, string>();
            this.columns = new Dictionary<string, Column>();
        }

        public string Name { get; private set; }

        public ITableModeler GetTableModeler()
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

        public Dictionary<string, string> GetDefinitions()
        {
            return this.definition.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public string GetDefinition(string statementKey)
        {
            return this.definition[statementKey];
        }

        public void RemoveDefinition(string statementKey)
        {
            this.definition.Remove(statementKey);
        }

        public IColumn[] GetColumns()
        {
            return this.columns.Select(kvp => kvp.Value).ToArray();
        }

        public IDatabaseModelBrowser GetDatabaseModeler()
        {
            return this.databaseModeler;
        }

        public IColumn GetColumn(string name)
        {
            Column column = null;
            this.columns.TryGetValue(name, out column);
            return column;
        }

        public StandardTableOptions WithColumn<TDataType>(string name)
        {
            var column = new Column(this, name, typeof(TDataType));
            this.columns.Add(name, column);

            return new StandardTableOptions(column);
        }

        public StandardTableOptions WithColumn(Type dataType, string columnName)
        {
            var column = new Column(this, columnName, dataType);
            this.columns.Add(columnName, column);
            return new StandardTableOptions(column);
        }
    }

    public class Table<TTableDefinition> : Table, ITable<TTableDefinition>
    {
        public Table(IDatabaseModelBrowser modeler, string name) : base(modeler, name) { }

        public StronglyTypedTableOptions<TTableDefinition> GetColumn<TColumn>(Expression<Func<TTableDefinition, TColumn>> columnSelector)
        {
            return new StronglyTypedTableOptions<TTableDefinition>(this);
        }
    }
}