using System;
using System.Linq.Expressions;

namespace Lightmap.Modeling
{
    public class EntityMappedTableCharacteristics<TTable> : IDirectEntityMappedTableCharacteristics<TTable>
    {
        private readonly IDatabaseModeler databaseModeler;

        private readonly ITableManager tableManager;

        public EntityMappedTableCharacteristics(IDatabaseModeler database, ITableManager tableManager)
        {
            this.databaseModeler = database;
            this.tableManager = tableManager;
        }

        public string DatabaseName => this.databaseModeler.DatabaseName;

        public IEntityBuilder Create()
        {
            return this.databaseModeler.Create();
        }

        public IDirectEntityMappedTableCharacteristics<TTable> IgnoreColumn<TColumn>(Expression<Func<TTable, TColumn>> columnSelector)
        {
            //tableManager.GetTable<TTable>().RemoveColumn()
            return this;
        }

        public IDirectEntityMappedTableCharacteristics<TTable> UseForeignKey<TForeignKey>(string relatedTable, Expression<Func<TTable, TForeignKey>> constraint)
        {
            return this;
        }

        public IDirectEntityMappedTableCharacteristics<TTable> UsePrimaryKey<TColumn>(Expression<Func<TTable, TColumn>> columnSelector)
        {
            return this;
        }
    }
}
