using System;
using System.Linq.Expressions;

namespace Lightmap.Modeling
{
    public class ColumnCharacteristics : IColumnDefinitionResult
    {
        private IDatabaseModeler databaseModeler;

        public ColumnCharacteristics(string name, Type dataType, TableModeler owner, IDatabaseModeler database)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), $"The {dataType.Name} column on the {owner.Name} table can not have a null or empty name.");
            }

            if (dataType == null)
            {
                throw new ArgumentNullException(nameof(dataType), $"The {name} column on the {owner.Name} table can not have a null data type.");
            }

            if (owner == null)
            {
                throw new ArgumentNullException(nameof(owner), $"The {name} column must have an owning table assigned to it.");
            }

            if (database == null)
            {
                throw new ArgumentNullException(nameof(database), $"The {name} column must be associated to a database model.");
            }

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
            if (string.IsNullOrEmpty(this.Owner.Name))
            {
                throw new InvalidOperationException("The owning table does not have a name and can't be used by this column.");
            }

            if (string.IsNullOrEmpty(this.Name))
            {
                throw new InvalidOperationException("This column does not have a name assigned to it. Indexes can not be created when the column name is unknown.");
            }

            this.IndexName = string.Concat(Owner.Name, "_", this.Name, "_IX");
            return this;
        }

        public IColumnDefinitionResult WithIndex(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "The given index name can not be used without a valid name being provided.");
            }

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
