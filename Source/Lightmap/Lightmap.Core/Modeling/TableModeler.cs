using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Lightmap.Modeling
{
    public class TableModeler : ITableModeler, ITableDefiniton
    {
        private readonly IEntityBuilder owner;

        private readonly IDatabaseModeler databaseModeler;

        private List<IColumnCharacteristics> characteristics = new List<IColumnCharacteristics>();

        public TableModeler(string tableName, IEntityBuilder owner, IDatabaseModeler database)
        {
            this.databaseModeler = database;
            this.owner = owner;
            this.Name = tableName;
        }
        
        public string Name { get; }

        public IColumnCharacteristics WithColumn<TDataType>(string name)
        {
            IColumnCharacteristics characteristic = new ColumnCharacteristics(name, typeof(TDataType), this, this.databaseModeler);
            this.characteristics.Add(characteristic);
            return characteristic;
        }

        public IColumnCharacteristics WithColumn(Type dataType, string columnName)
        {
            IColumnCharacteristics characteristic = new ColumnCharacteristics(columnName, dataType, this, this.databaseModeler);
            this.characteristics.Add(characteristic);
            return characteristic;
        }

        public IExpressionColumnCharacteristics<TColumns> WithColumns<TColumns>(System.Linq.Expressions.Expression<Func<TColumns>> columnDefinition)
        {
            var expression = (NewExpression)columnDefinition.Body;
            var columns = expression.Members;

            foreach (PropertyInfo property in columns.OfType<PropertyInfo>())
            {
                this.WithColumn(property.PropertyType, property.Name);
            }

            return new ExpressionColumnCharacteristics<TColumns>();
        }

        public IColumnCharacteristics GetColumn(string name)
        {
            return this.characteristics.FirstOrDefault(c => c.Name == name);
        }

        public void RemoveColumn(IColumnCharacteristics column)
        {
            this.characteristics.Remove(column);
        }
    }
}
