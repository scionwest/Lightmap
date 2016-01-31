using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

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

        public ITableModeler GetTable(string name)
        {
            return schema.OfType<ITableModeler>().FirstOrDefault(modeler => modeler.Name == name);
        }

        public ITableModeler GetTable<T>()
        {
            return schema.OfType<ITableModeler>().FirstOrDefault(modeler => modeler.Name == typeof(T).Name);
        }

        public ITableModeler GetTable<TTable>(Expression<Func<TTable>> tableDefinition)
        {
            return schema.OfType<ITableModeler>().FirstOrDefault(modeler =>
            {
                NewExpression expression = (NewExpression)tableDefinition.Body;
                return expression.Members.FirstOrDefault(member => member.Name == modeler.Name) != null;
            }); 
        }
    }
}
