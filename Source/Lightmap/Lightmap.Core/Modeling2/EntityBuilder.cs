using System;
using System.Collections.Generic;
using System.Reflection;

namespace Lightmap.Modeling2
{
    public class EntityBuilder : IEntityBuilder
    {
        private TableManager tableManager;

        private readonly IDatabaseModeler owner;

        internal EntityBuilder(EntityState initialState, TableManager manager, IDatabaseModeler owner)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager), "In order to build an entity, you must provide a table manager.");
            }

            if (owner == null)
            {
                throw new ArgumentNullException(nameof(owner), "In order to build an entity, you must provide the database model that the entity will belong to.");
            }

            this.State = initialState;
            this.owner = owner;
            this.tableManager = manager;
        }

        internal EntityState State { get; }

        public ITableModeler Table(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "The table name can not be blank.");
            }

            var table = new TableModeler(name, this, this.owner);
            tableManager.AddTable(table);
            return table;
        }

        //public IColumnSelector<TColumns> Table<TColumns>(string name, Expression<Func<TColumns>> columnDefinitions)
        //{
        //}

        //public IColumnSelector<TColumns> Table<TTable, TColumns>(Expression<Func<TColumns>> columnDefinitions)
        //    where TTable : class
        //{
        //    return this.Table(typeof(TTable).Name, columnDefinitions);
        //}

        public IDirectEntityMappedTableCharacteristics<TTable> Table<TTable>() where TTable : class
        {
            var table = new TableModeler(typeof(TTable).Name, this, this.owner);
            IEnumerable<PropertyInfo> propertiesToDefineColumns = PropertyCache.GetPropertiesForType<TTable>();
            foreach (PropertyInfo property in propertiesToDefineColumns)
            {
                table.WithColumn(property.PropertyType, property.Name);
            }

            this.tableManager.AddTable(table);

            return new EntityMappedTableCharacteristics<TTable>(this.owner, this.tableManager);
        }

        //public IEntityModeler View(string name)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEntityModeler View<TEntity>()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
