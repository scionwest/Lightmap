﻿using System;
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
            // Act
            var modeler = new DatabaseModeler();

            modeler.Create()
                .Table("User", () => new
                {
                    UserId = default(int),
                    FirstName = default(string),
                    LastName = default(string),
                    MiddleInitial = default(string),
                })
                .ModifyColumn((table, columns) => columns.First(c => c.Name == nameof(table.FirstName))).NotNull();
        }
    }
}