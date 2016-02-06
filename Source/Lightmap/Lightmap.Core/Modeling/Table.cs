using System;
using System.Linq.Expressions;

namespace Lightmap.Modeling
{
    public class Table
    {
        private DatabaseModeler databaseModeler;

        public Table(DatabaseModeler modeler)
        {
            this.databaseModeler = modeler;
        }

        public string Name { get; private set; }

        public DatabaseModeler GetDatabaseModeler()
        {
            return this.databaseModeler;
        }

        public Column GetColumn(string name)
        {
            return new Column();
        }

        public Column WithColumn<TDataType>(string name)
        {
            return new Column();
        }
        
        public Column WithColumn(Type dataType, string columnName)
        {
            return new Column();
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