using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lightmap
{
    public class DatabaseExistsException : Exception
    {
        public DatabaseExistsException(string message, string database) : base(message)
        {
            this.Database = database;
        }

        public string Database { get; }
    }
}
