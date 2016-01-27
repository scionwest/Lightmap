using System.Collections.Generic;

namespace Lightmap.Modeling
{
    public class DatabaseModeler : IDatabaseModeler
    {
        private List<IEntityBuilder> schema = new List<IEntityBuilder>();

        public IEntityBuilder Create()
        {
            var builder = new EntityBuilder(EntityState.Creating);
            schema.Add(builder);
            return builder;
        }

        public IEntityBuilder Alter()
        {
            var builder = new EntityBuilder(EntityState.Altering);
            schema.Add(builder);
            return builder;
        }

        public IEntityBuilder Drop()
        {
            var builder = new EntityBuilder(EntityState.Deleting);
            schema.Add(builder);
            return builder;
        }

    }
}
