using System;
using System.Collections.Generic;
using System.Linq;

namespace Lightmap.Modeling2
{
    public class TableManager : ITableManager
    {
        private List<ITableModeler> schema = new List<ITableModeler>();

        public ITableModeler GetTable(string name)
        {
            return schema.OfType<TableModeler>().FirstOrDefault(modeler => modeler.Name == name);
        }

        public ITableModeler GetTable<TTable>()
        {
            ITableModeler tableModeler = schema.OfType<ITableModeler>().FirstOrDefault(modeler => modeler is TTable);

            if (tableModeler == null)
            {
                throw new InvalidOperationException($"The {typeof(TTable).Name} table has not been defined as part of this database model.");
            }

            return tableModeler;
        }

        public ITableEditor EditTable<TTable>()
        {
            ITableDefiniton tableModeler = schema.OfType<ITableDefiniton>().FirstOrDefault(modeler => modeler.Name == typeof(TTable).Name);

            if (tableModeler == null)
            {
                throw new InvalidOperationException($"The {typeof(TTable).Name} table has not been defined as part of this database model.");
            }

            return new TableEditor(tableModeler);
        }

        public ITableModeler GetTable(Func<ITableModeler, bool> predicate)
        {
            return this.schema.OfType<ITableModeler>().FirstOrDefault(predicate);
        }

        internal void AddTable(ITableModeler modeler)
        {
            // TODO: Check if the modeler already exists.
            this.schema.Add(modeler);
        }
    }
}
