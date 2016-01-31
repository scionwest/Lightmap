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

        private TableManager tableManager;

        private readonly IDatabaseModeler owner;

        internal EntityBuilder(EntityState initialState, TableManager manager, IDatabaseModeler owner)
        {
            this.State = initialState;
            this.owner = owner;
            this.tableManager = manager;
        }

        internal EntityState State { get; }

        public ITableModeler Table(string name)
        {
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

            return null; // table;
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
