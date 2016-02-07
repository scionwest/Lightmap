using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public class DatabaseModelingOptions
    {
        public DatabaseModelingOptions(DatabaseModeler modeler)
        {
            this.DatabaseModeler = modeler;
        }

        public DatabaseModeler DatabaseModeler { get; }

        public Table Table(string name)
        {
            var table = new Table(this.DatabaseModeler);
            this.DatabaseModeler.AddTable(table);
            return table;
        }

        public StronglyTypedTableOptions<TTable> Table<TTable>() where TTable : class
        {
            var table = new Table<TTable>(this.DatabaseModeler);
            var tableOptions = new StronglyTypedTableOptions<TTable>(table);
            this.DatabaseModeler.AddTable(table);

            return tableOptions;
        }

        public TableExpressionDefinitonOptions<TTableDefinition> Table<TTableDefinition>(string name, Expression<Func<TTableDefinition>> definition)
        {
            var table = new Table<TTableDefinition>(this.DatabaseModeler);
            this.DatabaseModeler.AddTable(table);
            return new TableExpressionDefinitonOptions<TTableDefinition>(table);
        }
    }
}
