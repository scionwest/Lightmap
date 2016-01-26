using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public class EntityBuilder : IEntityBuilder
    {
        private List<IEntityModeler> schema = new List<IEntityModeler>();

        internal EntityBuilder(EntityState initialState)
        {
            this.State = initialState;
        }

        internal EntityState State { get; }

        public ITableModeler Table(string name)
        {
            var table = new TableModeler(name, this);
            schema.Add(table);
            return table;
        }

        public ITableModeler Table<TEntity>()
        {
            throw new NotImplementedException();
        }

        public IEntityModeler View(string name)
        {
            throw new NotImplementedException();
        }

        public IEntityModeler View<TEntity>()
        {
            throw new NotImplementedException();
        }
    }
}
