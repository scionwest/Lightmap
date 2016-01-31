using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public class EntityBuilder : IEntityBuilder
    {
        private static List<ITableModeler> tableSchema = new List<ITableModeler>();

        internal EntityBuilder(EntityState initialState)
        {
            this.State = initialState;
        }

        internal EntityState State { get; }

        public ITableModeler Table(string name)
        {
            var table = new TableModeler(name, this);
            tableSchema.Add(table);
            return table;
        }

        public ITableModeler Table<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public IEntityModeler View(string name)
        {
            throw new NotImplementedException();
        }

        public IEntityModeler View<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }
    }
}
