using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public class TableModeler : IEntityModeler, ITableModeler
    {
        private readonly IEntityBuilder owner;

        private List<IColumnCharacteristics> characteristics = new List<IColumnCharacteristics>();

        public TableModeler(string columnName, IEntityBuilder owner)
        {
            this.owner = owner;
            this.Name = columnName;
        }

        public string Name { get; }

        public Type DataType { get; }

        public IColumnCharacteristics<TDataType> WithColumn<TDataType>(string name)
        {
            IColumnCharacteristics<TDataType> characteristic = new ColumnCharacteristics<TDataType>(name, this);
            this.characteristics.Add(characteristic);
            return characteristic;
        }
    }
}
