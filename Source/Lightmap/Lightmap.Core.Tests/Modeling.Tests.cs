using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Lightmap.Modeling;
using Xunit;

namespace Lightmap.Provider.Sqlite.Tests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class DatabaseManagerSqliteExtensionsTests
    {
        public void test(IPAddress ip, int port)
        {

        }

        [Fact]
        public void DatabaseModeler_provides_table_modeler()
        {
            // Arrange
            int maxCount = 100000;
            var elapsed = new List<double>();

            // Act
            var modeler = new DatabaseModeler();

            for (int count = 0; count < maxCount; count++)
            {
                var watch = new Stopwatch();
                watch.Start();
                modeler.Create()
                    .Table("User", (c) => new
                    {
                        UserId = c.NewColumn<int>().AsPrimaryKey(),
                        FirstName = c.NewColumn<string>().NotNull(),
                        LastName = c.NewColumn<string>().NotNull(),
                        MiddleInitial = c.NewColumn<string>()
                    });

                //.Table(
                //    "Account",
                //    () => new { AccountId = default(int), Number = default(double), UserId = default(int) });
                watch.Stop();
                elapsed.Add(watch.Elapsed.TotalMilliseconds);
            }

            double average = elapsed.Average();
            Debug.WriteLine($"Average is {average}");
        }


    }
}