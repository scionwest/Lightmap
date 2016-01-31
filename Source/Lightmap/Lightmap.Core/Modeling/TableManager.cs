using System;
using System.Collections.Generic;
using System.Linq;

namespace Lightmap.Modeling
{
    public class TableManager : ITableManager, ITableEditor
    {
        private List<ITableModeler> schema = new List<ITableModeler>();

        public ITableModeler GetTable(string name)
        {
            return schema.OfType<TableModeler>().FirstOrDefault(modeler => modeler.Name == name);
        }

        public ITableEditor GetTable<TTable>()
        {
            ITableModeler tableModeler = schema.OfType<TableModeler>().FirstOrDefault(modeler => modeler.Name == typeof(TTable).Name);
            return this;
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

        public ITableEditor RemoveColumn(string name)
        {
            return this;
        }
    }
}
