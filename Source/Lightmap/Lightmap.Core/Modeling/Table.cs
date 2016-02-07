using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Lightmap.Modeling
{
    public class Table
    {
        private DatabaseModeler databaseModeler;

        private Dictionary<string, Column> columns;

        private Dictionary<string, string> definition;

        public Table(DatabaseModeler modeler, string name)
        {
            this.Name = name;
            this.databaseModeler = modeler;
            this.definition = new Dictionary<string, string>();
            this.columns = new Dictionary<string, Column>();
        }

        public string Name { get; private set; }

        public void AddDefiniton(string key, string value)
        {
            string existingDefinition = null;
            if (!this.definition.TryGetValue(key, out existingDefinition))
            {
                this.definition.Add(key, value);
                return;
            }

            existingDefinition = value;
        }

        public Dictionary<string, string> GetDefinition()
        {
            return this.definition.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public Column[] GetColumns()
        {
            return this.columns.Select(kvp => kvp.Value).ToArray();
        }

        public DatabaseModeler GetDatabaseModeler()
        {
            return this.databaseModeler;
        }

        public Column GetColumn(string name)
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

    public class Table<TTableDefiniton> : Table
    {
        public Table(DatabaseModeler modeler, string name) : base(modeler, name) { }

        public TableExpressionDefinitonOptions<TTableDefiniton> GetColumn<TColumn>(Expression<Func<TTableDefiniton, TColumn>> columnSelector)
        {
            return new TableExpressionDefinitonOptions<TTableDefiniton>(this);
        }
    }
}