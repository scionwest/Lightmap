using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Lightmap.Modeling
{
    public class Table
    {
        private DatabaseModeler databaseModeler;

        private Dictionary<string, Column> columns;

        public Table(DatabaseModeler modeler)
        {
            this.columns = new Dictionary<string, Column>();
        }

        public string Name { get; private set; }

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
        public Table(DatabaseModeler modeler) : base(modeler) { }

        public TableExpressionDefinitonOptions<TTableDefiniton> GetColumn<TColumn>(Expression<Func<TTableDefiniton, TColumn>> columnSelector)
        {
            return new TableExpressionDefinitonOptions<TTableDefiniton>(this);
        }
    }
}