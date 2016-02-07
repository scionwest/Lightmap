using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lightmap.Provider.Sqlite;

namespace Lightmap
{
    public class SqliteDataProviderFactory : IDataProviderFactory
    {
        public IDataProvider CreateProvider(DatabaseManager manager)
        {
            return new SqliteProvider(manager);
        }
    }
}
