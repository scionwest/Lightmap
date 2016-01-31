// using System;
// using System.Collections.Generic;
// using System.Data;
// using System.Data.Common;
// using System.IO;
// using System.Linq;
// using System.Linq.Expressions;
// using System.Threading.Tasks;
// using Lightmap.Modeling;
// using Lightmap.Provider.Sqlite.Tests.Mocks;
// using Xunit;

// namespace Lightmap.Provider.Sqlite.Tests
// {
//     // This project can output the Class library as a NuGet Package.
//     // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
//     public class DatabaseManagerSqliteExtensionsTests
//     {
//         [Fact]
//         public void DatabaseModeler_provides_table_modeler()
//         {
//             // Arrange
//             var modeler = new DatabaseModeler();
//             ITableModeler tableModeler = modeler.Create().Table("User");
//             tableModeler.WithColumn<int>("Id").AsPrimaryKey().WithIndex().IsUnique();
//         }
//     }
// }