using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Lightmap.Modeling
{
    public class ColumnSelector<TTableData> : IColumnSelector<TTableData>
    {
        public IColumnCharacteristics ModifyColumn(Func<TTableData, IEnumerable<IColumnCharacteristics>, IColumnCharacteristics> selector)
        {
            return default(IColumnCharacteristics);
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

        public string DefaultValue { get; private set; }

        public IColumnCharacteristics WithColumn<TDataType>(string name)
        {
            return this.Owner.WithColumn<TDataType>(name);
        }

        public IColumnCharacteristics WithDefaultValue<TDataType>(TDataType value)
        {
            this.DefaultValue = value.ToString();
            return this;
        }

        public IColumnCharacteristics AsForeignKey(ITableModeler constraint, string name)
        {
            throw new NotImplementedException();
        }

        public IColumnCharacteristics NotNull()
        {
            throw new NotImplementedException();
        }

        public IColumnCharacteristics AsPrimaryKey()
        {
            throw new NotImplementedException();
        }

        public IColumnCharacteristics IsUnique()
        {
            throw new NotImplementedException();
        }

        public IColumnCharacteristics WithIndex(string name)
        {
            throw new NotImplementedException();
        }

        public IColumnCharacteristics WithDefaultValue(object value)
        {
            throw new NotImplementedException();
        }

        public IColumnCharacteristics WithIndex()
        {
            string indexName = $"{Owner.Name}_{this.Name}_IX";

            throw new NotImplementedException();
        }
    }
}
