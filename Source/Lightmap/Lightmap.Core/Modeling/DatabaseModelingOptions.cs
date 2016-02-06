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
            return new Table(this.DatabaseModeler);
        }

        public Table<TTable> Table<TTable>() where TTable : class
        {
            return new Table<TTable>(this.DatabaseModeler);
        }

        public TableExpressionDefinitonOptions<TTableDefinition> Table<TTableDefinition>(string name, Expression<Func<TTableDefinition>> definition)
        {
            return new TableExpressionDefinitonOptions<TTableDefinition>();
        }
    }
}
