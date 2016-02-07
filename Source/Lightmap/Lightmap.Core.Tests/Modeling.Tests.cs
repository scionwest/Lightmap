using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        public async Task DatabaseModeler_provides_table_modeler()
        {
            // Arrange
            int count = 1000;
            var time = new List<double>();


            // Act
            for (int c = 0; c < count; c++)
            {
                File.Delete("Foo.Bar.sqlite");
                var watch = new Stopwatch();

                watch.Start();
                var model = new StronglyTypedMigration();
                var databaseManager = new DatabaseManager("Foo.Bar.sqlite", new List<IMigration> { model });
                databaseManager.UseSqliteProvider();
                await databaseManager.UpgradeDatabase();
                watch.Stop();

                model = null;
                databaseManager = null;
                time.Add(watch.ElapsedTicks);
                watch.Reset();
            }

            double average = time.Skip(10).Average();
        }
    }
}