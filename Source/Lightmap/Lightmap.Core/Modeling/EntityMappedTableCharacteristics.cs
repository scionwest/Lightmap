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
            if (tableManager == null)
            {
                throw new ArgumentNullException(nameof(tableManager), "In order to modify the table's columns, you must provide a table manager.");
            }

            if (database == null)
            {
                throw new ArgumentNullException(nameof(database), "In order to modify the table's columns, you must provide the database model that the table will belong to.");
            }

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
            if (columnSelector == null)
            {
                throw new ArgumentNullException(nameof(columnSelector), "The column selector expression can't be null. In order to ignore a column, you must choose a column via the lambda.");
            }

            MemberExpression expression;

            try
            {
                expression = (MemberExpression)columnSelector.Body;
                tableManager.GetTable<TTable>().RemoveColumn(expression.Member.Name);
            }
            catch (InvalidCastException e)
            {
                var exception = new NotSupportedException("The expression provided is not supported. You must use the expression to select a member off of the table object provided to you.");
                throw new AggregateException(exception, e);
            }

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
