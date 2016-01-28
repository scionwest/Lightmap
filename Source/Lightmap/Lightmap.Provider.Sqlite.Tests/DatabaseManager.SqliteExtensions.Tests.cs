using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lightmap.Provider.Sqlite.Tests.Mocks;
using Xunit;

namespace Lightmap.Provider.Sqlite.Tests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class DatabaseManagerSqliteExtensionsTests : IDisposable
    {
        private string databaseName;

        public DatabaseManagerSqliteExtensionsTests()
        {
            this.databaseName = $"UnitTestDb-{Guid.NewGuid()}.sqlite";
        }

        [Fact]
        public void New_connection_is_not_opened_by_default()
        {
            // Arrange
            var databaseName = $"UnitTestDb-{Guid.NewGuid()}.sqlite";
            var manager = new DatabaseManager(databaseName, Enumerable.Empty<IMigration>());

            // Act
            DbConnection connection = manager.CreateSqliteConnection();

            // Assert
            Assert.True(connection.State == System.Data.ConnectionState.Closed);
            Assert.False(File.Exists(databaseName), "Database was created");
        }

        [Fact]
        public async Task Open_connection_provides_opened_connection_to_new_database()
        {
            // Arrange
            var manager = new DatabaseManager(databaseName, Enumerable.Empty<IMigration>());

            // Act
            ConnectionState? state = null;
            using (DbConnection connection = await manager.OpenSqliteConnectionAsync())
            {
                state = connection.State;
            }

            // Assert
            Assert.True(state.Value == System.Data.ConnectionState.Open);
            Assert.True(File.Exists(databaseName), "Database was not created");
        }

        [Fact]
        public async Task Upgrade_database_configures_migrations()
        {
            // Act
            bool configured = false;
            var completionSource = new TaskCompletionSource<bool>();
            IMigration initialMigration = new InitialDatabaseMigrationMock(() => configured = true, completionSource);
            var manager = new DatabaseManager(databaseName, new IMigration[] { initialMigration });

            // Arrange
            Task upgradeTask = manager.UpgradeDatabase();
            await completionSource.Task;
            await upgradeTask;

            // Assert
            Assert.True(configured);
        }

        public void Dispose()
        {
            if (File.Exists(this.databaseName))
            {
                File.Delete(this.databaseName);
            }
        }
    }
}
