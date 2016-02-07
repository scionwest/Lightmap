using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public class DatabaseModelingOptions
    {
        public DatabaseModelingOptions(DatabaseModeler modeler)
        {
            if (modeler == null)
            {
                throw new ArgumentNullException(nameof(modeler), "You must provide a database modeler when creating schema objects for the database.");
            }

            this.DatabaseModeler = modeler;
        }

        public DatabaseModeler DatabaseModeler { get; }

        public Table Table(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "You can not create a table without providing it a name.");
            }

            var table = new Table(this.DatabaseModeler, name);
            this.DatabaseModeler.AddTable(table);
            return table;
        }

        public StronglyTypedTableOptions<TTable> Table<TTable>() where TTable : class
        {
            var table = new Table<TTable>(this.DatabaseModeler, typeof(TTable).Name);
            var tableOptions = new StronglyTypedTableOptions<TTable>(table);
            this.DatabaseModeler.AddTable(table);

            return tableOptions;
        }

        public TableExpressionDefinitonOptions<TTableDefinition> Table<TTableDefinition>(string name, Expression<Func<TTableDefinition>> definition)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "You can create a table without providing it a name.");
            }

            if (definition == null)
            {
                throw new ArgumentNullException(nameof(name), $"You must define the columns you want for the {name} table by returning an anonymous object as part of the expression. The anonymous object must contain properties that map to the columns you want created.");
            }

            // Create the column objects that the definition provides us.
            NewExpression columnExpression = definition.Body as NewExpression;
            if (columnExpression == null)
            {
                throw new NotSupportedException($"The {definition.Body.NodeType.GetType().Name} used in the definiton is not supported. You must create and return an anonymous object.");
            }

            var table = new Table<TTableDefinition>(this.DatabaseModeler, name);
            foreach (var columnData in columnExpression.Members.OfType<PropertyInfo>())
            {
                table.WithColumn(columnData.PropertyType, columnData.Name);
            }

            this.DatabaseModeler.AddTable(table);
            return new TableExpressionDefinitonOptions<TTableDefinition>(table);
        }
    }
}
