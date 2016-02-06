using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lightmap.Modeling2
{
    public class TableEditor : ITableEditor
    {
        private ITableDefiniton tableDefinition;

        public TableEditor(ITableDefiniton definition)
        {
            this.tableDefinition = definition;
        }

        public ITableEditor RemoveColumn(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), $"In order to remove a column from the {this.tableDefinition.Name} table, you must specify the column name.");
            }

            IColumnCharacteristics column = this.tableDefinition.GetColumn(name);

            if (column == null)
            {
                throw new InvalidOperationException($"The {name} column specified has not been defined as part of the {this.tableDefinition.Name} table.");
            }

            this.tableDefinition.RemoveColumn(column);
            return this;
        }
    }
}
