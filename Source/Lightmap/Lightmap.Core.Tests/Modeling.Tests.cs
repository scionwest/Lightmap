using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DatabaseManagerSqliteExtensionsTests;
using Lightmap.Core.Tests;
using Lightmap.Core.Tests.Migrations;
using Lightmap.Modeling;
using Xunit;

namespace Lightmap.Provider.Sqlite.Tests
{
    public class DatabaseManagerSqliteExtensionsTests
    {
        [Fact]
        public void DatabaseModeler_provides_table_modeler()
        {
            // Arrange
            int count = 100000;
            var time = new List<double>();

            // Act
            for (int c = 0; c < count; c++)
            {
                var watch = new Stopwatch();
                var model = new StronglyTypedMigration();
                watch.Start();
                model.Configure(new DatabaseModeler("Foo.sql"));
                watch.Stop();
                time.Add(watch.ElapsedTicks);
                watch.Reset();
            }

            double average = time.Skip(10).Average();
        }
    }
}