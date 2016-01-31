﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Lightmap.Modeling
{
    public class TableModeler : ITableModeler, ITableDefiniton
    {
        private readonly IEntityBuilder owner;

        private List<ColumnCharacteristics> characteristics = new List<ColumnCharacteristics>();

        public TableModeler(string tableName, IEntityBuilder owner)
        {
            this.owner = owner;
            this.Name = tableName;
        }
        
        public string Name { get; }

        public IColumnCharacteristics WithColumn<TDataType>(string name)
        {
            ColumnCharacteristics characteristic = new ColumnCharacteristics(name, typeof(TDataType), this);
            this.characteristics.Add(characteristic);
            return characteristic;
        }

        public IColumnCharacteristics WithColumn(Type dataType, string columnName)
        {
            ColumnCharacteristics characteristic = new ColumnCharacteristics(columnName, dataType, this);
            this.characteristics.Add(characteristic);
            return characteristic;
        }

        public IExpressionColumnCharacteristics<TColumns> WithColumns<TColumns>(Expression<Func<TColumns>> columnDefinition)
        {
            var expression = (NewExpression)columnDefinition.Body;
            var columns = expression.Members;

            foreach (PropertyInfo property in columns.OfType<PropertyInfo>())
            {
                this.WithColumn(property.PropertyType, property.Name);
            }

            return new ExpressionColumnCharacteristics<TColumns>();
        }
    }
}
