using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public class DatabaseModeler
    {
        public DatabaseModeler(string databaseName)
        {
            this.DatabaseName = databaseName;
        }

        public string DatabaseName { get; }

        public DatabaseModelingOptions Create()
        {
            return new DatabaseModelingOptions(this);
        }
    }
}
