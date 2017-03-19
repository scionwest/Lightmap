using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public class SeedModel
    {
        private List<SeedData> data;

        public SeedModel()
        {
            this.data = new List<SeedData>();
        }

        public SeedModel(params SeedData[] data) : this()
        {
            this.data.AddRange(data);
        }

        public IEnumerable<SeedData> Data => data;

        public void AddData(string column, object value)
        {
            this.data.Add(new SeedData(column, value));
        }

        public void AddData<TDataType>(string column, TDataType value)
        {
            this.data.Add(new SeedData<TDataType>(column, value));
        }
    }
    public class SeedData
    {
        public SeedData(string column, object value)
        {
            this.Column = column;
            this.Value = value;
        }

        public string Column { get; }

        public virtual object Value { get; }
    }

    public class SeedData<TDataType> : SeedData
    {
        public SeedData(string column, TDataType value) : base(column, value)
        {
        }

        public TDataType Data => (TDataType)base.Value;
    }
}
