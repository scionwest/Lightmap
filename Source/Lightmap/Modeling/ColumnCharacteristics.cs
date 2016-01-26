using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public class ColumnCharacteristics<TDataType> : IColumnCharacteristics<TDataType>
    {
        public ColumnCharacteristics(string name, ITableModeler owner)
        {
            this.ColumnName = name;
            this.Owner = owner;
        }

        internal string ColumnName { get; }

        internal ITableModeler Owner { get; }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
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

        public IColumnCharacteristics WithDefaultValue(TDataType value)
        {
            throw new NotImplementedException();
        }

        public IColumnCharacteristics WithIndex(string name)
        {
            throw new NotImplementedException();
        }

        public IColumnCharacteristics WithIndex()
        {
            string indexName = $"{Owner.Name}_{this.ColumnName}_IX";

            throw new NotImplementedException();
        }

        public IColumnCharacteristics<T> WithColumn<T>(string name)
        {
            return this.Owner.WithColumn<T>(name);
        }
    }
}
