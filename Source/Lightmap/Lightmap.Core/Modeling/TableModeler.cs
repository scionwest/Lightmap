using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public class TableModeler : IEntityModeler, ITableModeler
    {
        private readonly IEntityBuilder owner;

        private List<ColumnCharacteristics> characteristics = new List<ColumnCharacteristics>();

        public TableModeler(string tableName, IEntityBuilder owner)
        {
            this.owner = owner;
            this.Name = tableName;
            tableName.Split(',');
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
    }
}
