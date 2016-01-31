using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Lightmap.Modeling
{
    public class EntityBuilder : IEntityBuilder
    {
        private List<TableModeler> tableSchema = new List<TableModeler>();

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

        public IColumnSelector<TColumns> Table<TColumns>(string name, Expression<Func<TColumns>> columnDefinitions)
        {
            var expression = (NewExpression)columnDefinitions.Body;
            var columns = expression.Members;
            var table = new TableModeler(name, this);

            foreach (PropertyInfo property in columns.OfType<PropertyInfo>())
            {
                table.WithColumn(property.PropertyType, property.Name);
            }

            this.tableSchema.Add(table);

            return new ColumnSelector<TColumns>();
        }

        public IColumnSelector<TColumns> Table<TTableName, TColumns>(Expression<Func<TTableName>> tableName, Expression<Func<TColumns>> columnDefinitions)
        {
            var expression = (NewExpression)tableName.Body;
            var name = expression.Members.First();
            return null;// this.Table(name.Name, columnDefinitions);
        }

        public IColumnSelector<TEntity> Table<TEntity>() where TEntity : class
        {
            var table = new TableModeler(typeof(TEntity).Name, this);
            IEnumerable<PropertyInfo> propertiesToDefineColumns = PropertyCache.GetPropertiesForType<TEntity>();
            foreach(PropertyInfo property in propertiesToDefineColumns)
            {
                table.WithColumn(property.PropertyType, property.Name);
            }

            return null; // table;
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
