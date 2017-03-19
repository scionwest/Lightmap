using Lightmap.Core.Tests.Models;
using Lightmap.Querying;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Xunit;

namespace Lightmap.Provider.Sqlite.Tests
{
    public class DatabaseManagerSqliteExtensionsTests
    {
        [Fact]
        public void DatabaseModeler_provides_table_modeler()
        {
            var q = new LightmapQuery<AspNetRoles>(new SqliteProvider2());
            var q2 = q.Where(role => role.Name == "Admin");
            var result = q2.ToList();
        }
    }
}