using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Lightmap.Modeling
{
    public class ExpressionColumnCharacteristics<TTableData> : IExpressionColumnCharacteristics<TTableData>
    {
        public ITableModeler Table(string name)
        {
            throw new NotImplementedException();
        }

        public IExpressionColumnCharacteristics<TTableData> UseForeignKey<TForeignKey>(string relatedTable, Expression<Func<TTableData, TForeignKey>> constraint)
        {
            throw new NotImplementedException();
        }

        public IExpressionColumnCharacteristics<TTableData> UsePrimaryKey<TColumn>(Expression<Func<TTableData, TColumn>> columnSelector)
        {
            throw new NotImplementedException();
        }
    }

    public class ColumnCharacteristics : IColumnCharacteristics
    {
        public ColumnCharacteristics(string name, Type dataType, ITableModeler owner)
        {
            this.Name = name;
            this.Owner = owner;
            this.DataType = dataType;
        }

        internal ITableModeler Owner { get; }

        public Type DataType { get; }

        public string Name { get; }

        public IColumnDefinitionResult AsPrimaryKey()
        {
            throw new NotImplementedException();
        }

        public IColumnDefinitionResult WithIndex()
        {
            throw new NotImplementedException();
        }

        public IColumnDefinitionResult WithIndex(string name)
        {
            throw new NotImplementedException();
        }

        public IColumnDefinitionResult NotNull()
        {
            throw new NotImplementedException();
        }

        public IColumnDefinitionResult IsUnique()
        {
            throw new NotImplementedException();
        }

        public IColumnDefinitionResult WithDefaultValue(object value)
        {
            throw new NotImplementedException();
        }

        public IColumnDefinitionResult WithDefaultValue<TDataType>(TDataType value)
        {
            throw new NotImplementedException();
        }

        public IColumnCharacteristics WithColumn<TDataType>(string name)
        {
            throw new NotImplementedException();
        }

        public IColumnCharacteristics WithColumn(Type dataType, string columnName)
        {
            throw new NotImplementedException();
        }

        public IExpressionColumnCharacteristics<TColumns> WithColumns<TColumns>(Expression<Func<TColumns>> columnDefinition)
        {
            throw new NotImplementedException();
        }
    }
}
