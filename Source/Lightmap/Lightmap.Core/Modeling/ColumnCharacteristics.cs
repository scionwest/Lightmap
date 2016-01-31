using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Lightmap.Modeling
{
    public class ColumnCharacteristics : IColumnDefinitionResult
    {
        private IDatabaseModeler databaseModeler;

        public ColumnCharacteristics(string name, Type dataType, TableModeler owner, IDatabaseModeler database)
        {
            this.databaseModeler = database;
            this.Name = name;
            this.Owner = owner;
            this.DataType = dataType;
        }

        internal TableModeler Owner { get; }

        public Type DataType { get; }

        public string Name { get; }

        public bool IsPrimaryKey { get; private set; }

        public string DatabaseName => this.databaseModeler.DatabaseName;

        public string IndexName { get; private set; }

        public bool RequiresUniqueness { get; private set; }

        public bool IsNullable { get; private set; }

        public object DefaultValue { get; private set; }

        public IColumnDefinitionResult AsPrimaryKey()
        {
            this.IsPrimaryKey = true;
            return this;
        }

        public IColumnDefinitionResult WithIndex()
        {
            this.IndexName = string.Concat(Owner.Name, "_", this.Name, "_IX");
            return this;
        }

        public IColumnDefinitionResult WithIndex(string name)
        {
            this.IndexName = name;
            return this;
        }

        public IColumnDefinitionResult NotNull()
        {
            this.IsNullable = false;
            return this;
        }

        public IColumnDefinitionResult IsUnique()
        {
            this.RequiresUniqueness = true;
            return this;
        }

        public IColumnDefinitionResult WithDefaultValue(object value)
        {
            this.DefaultValue = value;
            return this;
        }

        public IColumnDefinitionResult WithDefaultValue<TDataType>(TDataType value)
        {
            return this.WithDefaultValue(value);
        }

        public IColumnCharacteristics WithColumn<TDataType>(string name)
        {
            return this.Owner.WithColumn<TDataType>(name);
        }

        public IColumnCharacteristics WithColumn(Type dataType, string columnName)
        {
            return this.Owner.WithColumn(dataType, columnName);
        }

        public IExpressionColumnCharacteristics<TColumns> WithColumns<TColumns>(Expression<Func<TColumns>> columnDefinition)
        {
            return this.Owner.WithColumns(columnDefinition);
        }

        public IEntityBuilder Create()
        {
            return this.databaseModeler.Create();
        }
    }
}
